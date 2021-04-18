using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskTech_Infrustructure.DataContext;
using Microsoft.EntityFrameworkCore;
using TaskTech_ApplicationCore.Entites;
using TaskTech_ApplicationCore.Interface;
using TaskTech_Infrustructure.ImplementInterface;

namespace TaskTech_UI
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
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddScoped<IStudentCourse<Course>, CourseSerivce>();
            services.AddScoped<IStudentCourse<Student>, StudentSerivce>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<TaskTechDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("sqlcon"), x => x.MigrationsAssembly("TaskTech-Infrustructure"));
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMvc(route => { route.MapRoute("default", "{controller=Course}/{action=Index}/{id?}"); });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
