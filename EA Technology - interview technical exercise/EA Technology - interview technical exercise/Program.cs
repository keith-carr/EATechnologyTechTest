using MyApp.Services;
using MyApp.Services.Implementations;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            
            // Add services to the container.
            builder.Services.AddTransient<INodeForestBuilderService, NodeForestBuilderService>(); // Builds the forest of nodes
            builder.Services.AddTransient<IForestCustomerAllocator, ForestCustomerAllocator>(); // Allocates customers to nodes
            builder.Services.AddTransient<IForestConsolidationService, ForestConsolidationService>(); // Consolidates node trees
            builder.Services.AddTransient<IForestQueryService, ForestQueryService>(); // Queries forests for customers

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}