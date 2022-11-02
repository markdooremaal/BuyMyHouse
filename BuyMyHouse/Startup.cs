using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using AutoMapper;
using BuyMyHouse.Helpers;
using BuyMyHouse.Helpers.Abstract;
using BuyMyHouse.Services;
using BuyMyHouse.Services.Abstract;
using BuyMyHouse.Validators;
using FluentValidation;
using Microsoft.Extensions.Azure;
using Models;

namespace BuyMyHouse
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
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

            services.AddDbContext<BuyMyHouseContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("MySQL"), serverVersion));
            

            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<House>, HouseValidator>();
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHouseService, HouseService>();
            services.AddTransient<IMortgageService, MortgageService>();
            
            services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddBlobServiceClient(Configuration.GetConnectionString("AzureWebJobsStorage"));
            });

            services.AddTransient<IUploadImageHelper, UploadImageHelper>();

            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            // Automapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyMyHouse", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication5 v1"));
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