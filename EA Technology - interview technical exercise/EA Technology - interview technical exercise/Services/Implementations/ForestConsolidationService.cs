using MyApp.Models;

namespace MyApp.Services.Implementations
{
    public class ForestConsolidationService : IForestConsolidationService
    {
        private NodeForest _forest;


        // Combines distributed nodes into tree networks
        public NodeForest Consolidate(NodeForest forest)
        {
            _forest = forest;

            CombineTreeSplits();

            var nodeRootIds = _forest.trees.Select(t => t.Id).ToList();
            foreach (var id in nodeRootIds)
            {
                TreeSearch(forest, id);
            }

            return forest;
        }

        // Combines subnodes of root trees sharing the same ID
        private void CombineTreeSplits()
        {
            // Duplicated root node IDs
            var nodeRootIds = _forest.trees.GroupBy(t => t.Id)
                                .Where(g => g.Count() > 1)
                                .Select(t => t.Key)
                                .ToList();

            
            foreach (var id in nodeRootIds)
            {
                var subNodes = _forest.trees.Where(t => t.Id == id).SelectMany(t => t.SubNodes).ToList(); // Combine subnodes
                var firstInstace = _forest.trees.First(t => t.Id == id); // Pick first instance of tree
                firstInstace.SubNodes.Clear();
                firstInstace.SubNodes = subNodes; // Reallocated all subnodes to first tree instance
                _forest.trees.RemoveAll(t => t.Id == id); // Remove all trees sharing ID
                _forest.trees.Add(firstInstace); // Re-add the first instance
            }
        }

        // Search for node ID in forest
        private void TreeSearch(NodeForest forest, int id)
        {
            var foundId = false;
            foreach (var node in forest.trees.Where(t => t.Id != id))
            {
                if (FindSubnodeFor(id, node))
                {
                    foundId = true;
                    break;
                }
            }

            if (foundId) _forest.trees.Remove(_forest.trees.First(t => t.Id == id));
        }

        // Search for node ID in subnodes, consolidate subnodes to parent tree
        private bool FindSubnodeFor(int id, NodeModel node)
        {
            if (node.Id == id)
            {
                node.SubNodes.AddRange(_forest.trees.First(t => t.Id == id).SubNodes);
                return true;
            }
            else
            {
                foreach (var subNode in node.SubNodes)
                {
                    if (FindSubnodeFor(id, subNode)) return true;
                }
            }

            return false;
        }
    }
}
