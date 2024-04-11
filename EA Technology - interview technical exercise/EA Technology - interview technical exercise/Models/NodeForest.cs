namespace MyApp.Models
{
    public class NodeForest
    {
        public List<NodeModel> trees { get; set; } = new List<NodeModel>();
    }

    public class NodeModel
    {
        public int Id { get; set; }
        public List<NodeModel> SubNodes { get; set; } = new List<NodeModel>();
        public int CustomerCount { get; set; }
    }
}
