using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeNetworkController : ControllerBase
    {
        private readonly ILogger<NodeNetworkController> _logger;
        private readonly IForestQueryService _forestQueryService;

        public NodeNetworkController(
            ILogger<NodeNetworkController> logger,
            IForestQueryService forestQueryService)
        {
            _logger = logger;
            _forestQueryService = forestQueryService;
        }

        [HttpPost]
        [Route("GetCustomers")]
        public CustomerCountResult Get(CustomerCountForNetworkModel model)
        {
            return _forestQueryService.GetCustomerCountResult(model);
        }
    }
}