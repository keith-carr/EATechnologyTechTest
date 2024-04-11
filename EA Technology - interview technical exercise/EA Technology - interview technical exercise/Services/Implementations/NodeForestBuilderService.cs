using MyApp.Models;

namespace MyApp.Services.Implementations
{
    public class NodeForestBuilderService : INodeForestBuilderService
    {
        private readonly IForestConsolidationService _forestConsolidation;
        private NetworkModel _nodeNetwork;
        private NodeForest _forest;

        public NodeForestBuilderService(IForestConsolidationService forestConsolidation)
        {
            _forestConsolidation = forestConsolidation;
        }

        // Builds node networks from branch data
        public NodeForest BuildForest(NetworkModel nodeNetwork)
        {
            _nodeNetwork = nodeNetwork;
            _forest = new NodeForest();

            // Build node forest from scattered branches
            foreach (var branch in _nodeNetwork.Branches)
            {
                // Build node for current branch
                NodeModel node = new NodeModel();
                node.Id = branch.StartNode;
                node.SubNodes.Add(new NodeModel() { Id = branch.EndNode });

                BuildForestFor(node);
            }
            return _forestConsolidation.Consolidate(_forest);
        }

        // Build forest for current node
        private void BuildForestFor(NodeModel node)
        {
            // Have we started building the forest?
            if (!_forest.trees.Any())
            {
                // Add first tree
                _forest.trees.Add(node);
                return;
            }

            // Check if node already exists in forest
            if (!FindNodeInForest(node))
            {
                // Node not found, must be a new tree
                UpdateNodeForSubNodes(node, node);
                _forest.trees.Add(node);
            }
        }

        // Try to find node in the forest
        private bool FindNodeInForest(NodeModel nodeModel)
        {
            foreach (var tree in _forest.trees)
            {
                if (FindSubNode(tree, nodeModel)) return true;
            }

            return false;
        }

        // Recursively find subnode in tree
        private bool FindSubNode(NodeModel node, NodeModel nodeModel)
        {
            var found = false;

            foreach (var subNode in node.SubNodes)
            {
                if (subNode.Id == nodeModel.Id)
                {
                    UpdateNodeForSubNodes(subNode, nodeModel);
                    found = true;
                    break;
                }
                else
                {
                    found = FindSubNode(subNode, nodeModel);
                }
            }

            return found;
        }

        // Update node for subnodes
        private void UpdateNodeForSubNodes(NodeModel node, NodeModel nodeModel)
        {
            var missingSubNodes = nodeModel.SubNodes.Where(
                                        nm => !node.SubNodes.Any(
                                            cn => nm.Id == cn.Id));

            node.SubNodes.AddRange(missingSubNodes);
        }
    }
}
