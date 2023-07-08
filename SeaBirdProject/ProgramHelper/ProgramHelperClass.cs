using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SeaBirdProject.ApplicationAuthenticationFolder;
using SeaBirdProject.ApplicationContext;
using SeaBirdProject.GateWay.Email;
using SeaBirdProject.Repositories.Implementations;
using SeaBirdProject.Repositories.Interfaces;
using SeaBirdProject.Services.Implementations;
using SeaBirdProject.Services.Interfaces;
using System.Security.Claims;
using System.Text;

namespace SeaBirdProject.ProgramHelper
{
    public class ProgramHelperClass
    {
        public static void CrossOriginPolicy(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(a => a.AddPolicy("CorsPolicy", b =>
            {
                //b.WithOrigins("http://localhost:5000/")
                b.AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader();

            }));
        }
        public static void AdminPolicy(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(x =>
            x.AddPolicy("SuperAdminPolicy", policy => {
                policy.RequireRole("SuperAdmin");
                policy.RequireClaim(ClaimTypes.Email, new string[] { "tijaniadebayoabdllahi@gmail.com", "johnwilson5864@gmail.com" });

            }));
        }
        public static void AddingDbContextToContainer(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(
            builder.Configuration.GetConnectionString("SeaBirdConnectionString")
            ));

        }

        public static void AddingContextAccessor(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void RegisteringAndSortingDependencies(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
            builder.Services.AddScoped<ISuperAdminService, SuperAdminService>();

            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddScoped<IEmailSender, EmailSender>();
        }

        public static void AddingJWTConfigurationToContainer(WebApplicationBuilder builder)
        {

            var key = builder.Configuration.GetValue<string>("JWTConnectionKey:JWTKeyString");
            builder.Services.AddSingleton<IJWTAuthentication>(new JWTAuthentication(key));

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });

        }

        public static void HttpPipelineConfiguration(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeaBird Project v1"));
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            //cancellation token for long proccesses
            app.MapGet("/hello", async (CancellationToken token) => {
                app.Logger.LogInformation("Request started at: " +
                DateTime.Now.ToLongTimeString());
                await Task.Delay(TimeSpan.FromSeconds(5), token);
                app.Logger.LogInformation("Request completed at: " +
                DateTime.Now.ToLongTimeString());
                return "Success";

            });

        }
    }
}
