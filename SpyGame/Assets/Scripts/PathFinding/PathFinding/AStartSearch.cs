using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStartSearch : MonoBehaviour {

    public static List<Vector2> Search(Grid grid, Vector3 origin, Vector3 destiny) {
        if (!GridUtils.isWorldPositionInGrid(grid, origin) || !GridUtils.isWorldPositionInGrid(grid, destiny))
            return new List<Vector2>();

        return Search(grid, GridUtils.nodeFromWorldPoint(grid, origin), GridUtils.nodeFromWorldPoint(grid, destiny));
    } // Search

    /// <summary>
    /// Pure PathFinding
    /// </summary>
    /// <param name="grid">grid where try to find</param>
    /// <param name="origin">origin cell</param>
    /// <param name="destiny">destiny cell</param>
    /// <returns>the if find path, return a orderer list with the cells, otherwise a clear list</returns>
    public static List<Vector2> Search(Grid grid, Vector2 origin, Vector2 destiny)
    {
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
        Dictionary<Vector2, double> costSoFar = new Dictionary<Vector2, double>();

        var frontier = new Queue<Vector2>();
        frontier.Enqueue(origin);

        cameFrom[origin] = origin;
        costSoFar[origin] = PathFindingUtils.HeuristicRealDistanceSqr(grid, origin, destiny);

        int security = 10 * (int)( grid.getGridSize().x * grid.getGridSize().y );

        Vector2 currentNode;
        double newCost;
        //double priority;

        while (security-- > 0 && frontier.Count > 0) {
            currentNode = frontier.Dequeue();
            if (currentNode.x == destiny.x && currentNode.y == destiny.y) {
                break;
            }

            foreach (Vector2 next in GridUtils.getNeighbors(grid, currentNode)) {
                newCost = costSoFar[currentNode] +
                    PathFindingUtils.HeuristicRealDistanceSqr(grid, currentNode, next);

                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next]) {
                    costSoFar[next] = newCost;

                    //priority = newCost + PathFindingUtils.HeuristicRealDistanceSqr(grid, next, destiny);
                    frontier.Enqueue(next);
                    cameFrom[next] = currentNode;
                }
            }
        }

        if (security <= 0)
            Debug.LogError("AStartSearch: More steps than allowed");

        List<Vector2> path = new List<Vector2>();
        Vector2 node = destiny;
        //*
        path.Add(node);
        for (int i = cameFrom.Count - 1; i >= 0 && node != origin; --i)
        {
            if(cameFrom.TryGetValue(node, out node))
            {
                path.Add(node);
            } else
            {
                path.Clear();
                break;
            }
        }
        return path;
    } // Search


    // !!!TODO: working in A* improvements
    public static List<Vector2> Search2(Grid grid, Vector2 origin, Vector2 destiny)
    {
        List<Vector2> result = new List<Vector2>();





        return result;
    } // Search2

}
