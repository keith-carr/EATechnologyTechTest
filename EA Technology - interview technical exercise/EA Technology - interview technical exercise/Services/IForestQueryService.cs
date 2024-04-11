using MyApp.Models;

namespace MyApp.Services
{
    public interface IForestQueryService
    {
        CustomerCountResult GetCustomerCountResult(CustomerCountForNetworkModel model);
    }
}