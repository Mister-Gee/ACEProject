using ACE.Domain.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ACEdatabaseAPI.Data;
using Microsoft.AspNetCore.Identity;
using ACEdatabaseAPI.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IO;
using ACE.Domain.Concrete;
using ACE.Domain.Abstract;
using System.Reflection;
using System.Runtime.InteropServices;
using ACE.Domain.Concrete.EFControlledRepo;
using ACE.Domain.Abstract.IControlledRepo;
using ACEdatabaseAPI.Helpers.FileUploadService;

namespace ACE
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
            services.AddAutoMapper(typeof(Startup));
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            //services.AddSession();
            services.AddControllers();

            //services.AddCors(
            //    c =>
            //    {
            //        c.AddPolicy("AllowOrigin", options => options.SetIsOriginAllowedToAllowWildcardSubdomains()
            //            .WithOrigins("http://localhost:57578", "http://localhost:8080", "https://localhost:8080",
            //               "http://localhost:3000")
            //            .AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //        );
            //    }
            //);
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ACE", Version = "v1" });
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ACE API",
                    Description = "Adekunle College of Education Database",
                    //TermsOfService = new Uri("https://app.epump.com/terms"),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Service Guy",
                    //    Email = string.Empty,
                    //    Url = new Uri("https://app.epump.com/contact"),
                    //}
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                                  \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<ACEContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("ACEContext")
                ));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("ACEContext")
                ));
            services.AddIdentity<ApplicationUser, ApplicationRole>(option => 
                    option.SignIn.RequireConfirmedAccount = false )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                // Default User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            services.AddAuthorization();

            services.Configure<PasswordHasherOptions>(options =>
                options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);
            services.Configure<JwtAuth>(Configuration.GetSection("JwtAuthentication"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddHttpClient();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            //Scopes
            services.AddScoped<IvUserRoleRepo, EFvUserRoleRepo>();
            services.AddScoped<IDeviceRepo, EFDeviceRepo>();
            services.AddScoped<IProgrammeRepo, EFProgrammeRepo>();
            services.AddScoped<IReligionRepo, EFReligionRepo>();
            services.AddScoped<ISchoolRepo, EFSchoolRepo>();
            services.AddScoped<IDepartmentRepo, EFDepartmentRepo>();
            services.AddScoped<IGenderRepo, EFGenderRepo>();
            services.AddScoped<ILevelRepo, EFLevelRepo>();
            services.AddScoped<IMaritalStatusRepo, EFMaritalStatusRepo>();
            services.AddScoped<IStudentCategoryRepo, EFStudentCategoryRepo>();

            services.AddScoped<IFileUploadService, FileUploadService>();



        }

        private class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
        {
            private readonly IOptions<JwtAuth> _jwtAuthentication;

            public ConfigureJwtBearerOptions(IOptions<JwtAuth> jwtAuthentication)
            {
                _jwtAuthentication = jwtAuthentication ?? throw new System.ArgumentNullException(nameof(jwtAuthentication));
            }

            public void PostConfigure(string name, JwtBearerOptions options)
            {
                var jwtAuthentication = _jwtAuthentication.Value;

                options.ClaimsIssuer = jwtAuthentication.ValidIssuer;
                options.IncludeErrorDetails = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtAuthentication.ValidIssuer,
                    ValidAudience = jwtAuthentication.ValidAudience,
                    IssuerSigningKey = jwtAuthentication.SymmetricSecurityKey,
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ClockSkew = TimeSpan.Zero
                };
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseSession();
            var path = Directory.GetCurrentDirectory();
            var date = DateTime.Today.ToString("ddMMyyyy");
            //loggerFactory.AddFile($"{path}\\Logs\\{date}-Log.txt");

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ACE v1"));
            //}

            app.UseSwagger(
                    c =>
                    {
                        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            c.PreSerializeFilters.Add((doc, req) => doc.Servers.Clear());
                        }
                    }
                    );
            // app.UseDeveloperExceptionPage();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/myApi/swagger/v1/swagger.json", "V1 Docs");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ACE Service");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
