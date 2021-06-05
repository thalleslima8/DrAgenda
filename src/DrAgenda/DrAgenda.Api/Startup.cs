using DrAgenda.Data.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Codout.Framework.DAL;
using DrAgenda.Api.Helpers;
using DrAgenda.Core;
using Newtonsoft.Json.Serialization;

namespace DrAgenda.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddNHibernate(Configuration.GetConnectionString("ConnectionString"));
            services.AddSingleton(new ApiSettings(Configuration["ApiKey"]));
            services.AddSingleton<ApiKeyAuthorizeFilter>();

            services.AddDirectoryBrowser();

            services.AddCors();

            services.AddControllers(o =>
            {
                o.Conventions.Add(new ControllerDocumentationConvention());
            });

            services
                .AddMvc()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrAgenda.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IUnitOfWorkFactory<IDrAgendaUnitOfWork> unitOfWorkFactory, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DrAgenda.Api v1"));
            }

            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );
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
