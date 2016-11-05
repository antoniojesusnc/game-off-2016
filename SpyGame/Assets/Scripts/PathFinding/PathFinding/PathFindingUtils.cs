using UnityEngine;
using System.Collections;

public class PathFindingUtils : MonoBehaviour {

    public static double HeuristicRealDistanceSqr(Grid grid, Vector2 node1, Vector2 node2)
    {
        return ( GridUtils.worldFromPoint(grid, node2) - GridUtils.worldFromPoint(grid, node1) ).sqrMagnitude;
    } // HeuristicDistanceSqr

    public static double HeuristicRealDistance(Grid grid, Vector2 node1, Vector2 node2)
    {
        return ( GridUtils.worldFromPoint(grid, node2) - GridUtils.worldFromPoint(grid, node1) ).magnitude;
    } // HeuristicDistance
}
