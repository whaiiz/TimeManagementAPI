using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Repositories.MongoDb;

namespace TimeManagementAPI
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
            services.AddScoped(p =>
            {
                var client = new MongoClient(Configuration.GetValue<string>("TimeManagementDb:ConnectionString"));
                var database = client.GetDatabase(Configuration.GetValue<string>("TimeManagementDb:DatabaseName"));
                return database.GetCollection<TaskModel>("Tasks");
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddControllers();
            services.AddMediatR(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}