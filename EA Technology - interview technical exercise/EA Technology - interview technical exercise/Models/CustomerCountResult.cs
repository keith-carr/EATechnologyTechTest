namespace MyApp.Models
{
    public class CustomerCountResult
    {
        public int CustomerCount { get; set; }
        public List<NodeCustomerCount> NodeCustomerCounts { get; set; } = new List<NodeCustomerCount>();
        public NodeModel Tree { get; set; }
    }

    public class NodeCustomerCount
    {
        public NodeCustomerCount() { }
        public NodeCustomerCount(NodeModel node) 
        {
            Id = node.Id;
            CustomerCount = node.CustomerCount;
        }

        public int Id { get; set; }
        public int CustomerCount { get; set; }
    }
}
