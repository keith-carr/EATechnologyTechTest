using MyApp.Models;

namespace MyApp.Services
{
    public interface IForestCustomerAllocator
    {
        NodeForest Allocate(NodeForest forest, List<CustomerAllocations> allocations);
        void AllocateNode(NodeModel node);
    }
}