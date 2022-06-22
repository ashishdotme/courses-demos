using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RoomBooking.Core;
using RoomBooking.Core.DataServices;
using RoomBooking.Persistence;
using RoomBooking.Persistence.Repositories;

namespace RoomBooking.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "RoomBookingApp.API", Version = "v1" });
      });

      var connString = "DataSource=:memory:";
      var conn = new SqliteConnection(connString);
      conn.Open();

      services.AddDbContext<RoomBookingAppDbContext>(opt => opt.UseSqlite(conn));

      EnsureDatabaseCreate(conn);

      services.AddScoped<IRoomBookingService, RoomBookingService>();
      services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();
    }

    private void EnsureDatabaseCreate(SqliteConnection conn)
    {
      var builder = new DbContextOptionsBuilder<RoomBookingAppDbContext>();
      builder.UseSqlite(conn);

      using var context = new RoomBookingAppDbContext(builder.Options);
      context.Database.EnsureCreated();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}

