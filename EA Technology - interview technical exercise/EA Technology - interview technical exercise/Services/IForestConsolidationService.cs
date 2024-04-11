using MyApp.Models;

namespace MyApp.Services
{
    public interface IForestConsolidationService
    {
        NodeForest Consolidate(NodeForest forest);
    }
}