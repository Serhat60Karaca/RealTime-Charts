using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SignalR_SqlTableDependency.Repositories;

namespace SignalR_SqlTableDependency.Hubs
{
    public class DashboardHub : Hub
    {
        ProductRepository productRepository;
        public DashboardHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("NewConnection");
            productRepository = new ProductRepository(connectionString);
        }
        public async Task SendProducts()
        {
            var products = productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);

            var productsForGraph = productRepository.GetProductsForGraph();
            await Clients.All.SendAsync("ReceivedProductsForGraph", productsForGraph);
        }
    }
}
