using MyApp.Models;

namespace MyApp.Services
{
    public interface INodeForestBuilderService
    {
        NodeForest BuildForest(NetworkModel nodeNetwork);
    }
}