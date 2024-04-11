using System.Text.Json.Serialization;

namespace MyApp.Models
{
    public class CustomerCountForNetworkModel
    {
        public  int SelectedNode { get; set; }
        public int CustomerCount { get; set; }
        public NetworkModel Network { get; set; }
    }

    public class NetworkModel
    {
        public List<BranchModel> Branches { get; set; } = new List<BranchModel>();
        [JsonPropertyName("Customers")]
        public List<CustomerAllocations> CustomersAllocations { get; set; } = new List<CustomerAllocations>();
    }

    public class BranchModel
    {
        public int StartNode { get; set; }
        public int EndNode { get; set; }
    }

    public class CustomerAllocations
    {
        public int Node { get; set; }
        public int NumberOfCustomers { get; set; }
    }
}
