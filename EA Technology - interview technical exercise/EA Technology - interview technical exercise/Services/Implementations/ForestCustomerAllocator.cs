using MyApp.Models;

namespace MyApp.Services.Implementations
{
    public class ForestCustomerAllocator : IForestCustomerAllocator
    {
        private List<CustomerAllocations> _allocations;

        // Allocates customers to all nodes in the forest
        public NodeForest Allocate(NodeForest forest, List<CustomerAllocations> allocations)
        {
            _allocations = allocations;

            // Loop through all trees in forest and allocate customer
            foreach (var tree in forest.trees)
            {
                AllocateNode(tree);
            }

            return forest;
        }

        // Recurse through all nodes in the tree allocating customer counts
        public void AllocateNode(NodeModel node)
        {
            node.CustomerCount = _allocations
                    ?.Where(c => c.Node == node.Id)
                    ?.FirstOrDefault()
                    ?.NumberOfCustomers ?? 0;

            foreach (var subnode in node.SubNodes)
            {
                AllocateNode(subnode);
            }
        }
    }
}
