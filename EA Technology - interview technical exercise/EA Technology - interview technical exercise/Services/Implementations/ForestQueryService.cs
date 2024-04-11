using MyApp.Models;

namespace MyApp.Services.Implementations
{
    public class ForestQueryService : IForestQueryService
    {
        private readonly INodeForestBuilderService _forestBuilder;
        private readonly IForestCustomerAllocator _forestAllocator;
        private CustomerCountResult _result;

        public ForestQueryService(
            INodeForestBuilderService forestBuilder,
            IForestCustomerAllocator forestAllocator)
        {
            _forestBuilder = forestBuilder;
            _forestAllocator = forestAllocator;
        }

        // Return downstream aggregate customer count and associated nodes for a node ID located
        // somewhere within a node forest
        public CustomerCountResult GetCustomerCountResult(CustomerCountForNetworkModel model)
        {
            _result = new CustomerCountResult();

            // Build and allocate forest nodes
            var forest = _forestBuilder.BuildForest(model.Network);
            forest = _forestAllocator.Allocate(forest, model.Network.CustomersAllocations);

            foreach (var tree in forest.trees)
            {
                if (FindSubNode(tree, model.SelectedNode)) break; // Node can only exist in one tree
            }

            // Gets a flattened list of nodes for easier parsing / reading
            if (_result.Tree != null)
            {
                GetNodeFlatList(_result.Tree);
            }

            return _result;
        }

        // Search all nodes in a tree
        private bool FindSubNode(NodeModel node, int selectedNode)
        {
            if (node.Id == selectedNode)
            {
                _result.Tree = node;
                SumCustomerForSubNodes(node);
                return true;
            }
            else
            {
                foreach (var subNode in node.SubNodes)
                {
                    if (FindSubNode(subNode, selectedNode)) return true;
                }
            }

            return false;
        }

        // Recursively sum customers in subnodes
        private void SumCustomerForSubNodes(NodeModel node)
        {
            _result.CustomerCount += node.CustomerCount;

            foreach (var subNode in node.SubNodes)
            {
                SumCustomerForSubNodes(subNode);
            }
        }

        private void GetNodeFlatList(NodeModel node)
        {
            _result.NodeCustomerCounts.Add(new NodeCustomerCount(node));

            foreach (var subNode in node.SubNodes)
            {
                GetNodeFlatList(subNode);
            }
        }
    }
}
