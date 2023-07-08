
using SeaBirdProject.ProgramHelper;

namespace SeaBirdProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ProgramHelperClass.CrossOriginPolicy(builder);
            ProgramHelperClass.AdminPolicy(builder);
            ProgramHelperClass.AddingDbContextToContainer(builder);
            ProgramHelperClass.AddingContextAccessor(builder);
            ProgramHelperClass.RegisteringAndSortingDependencies(builder);
            ProgramHelperClass.AddingJWTConfigurationToContainer(builder);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            ProgramHelperClass.HttpPipelineConfiguration(app);
            app.Run();
        }
    }
}