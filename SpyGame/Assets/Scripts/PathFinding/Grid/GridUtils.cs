using UnityEngine;
using System.Collections;
using System;

public class GridUtils : MonoBehaviour {

    /// <summary>
    /// Static method to get Node by worldPoint
    /// </summary>
    /// <param name="grid"> the grid to check</param>
    /// <param name="worldPosition">the world position</param>
    /// <returns>Node point</returns>
    public static Vector2 nodeFromWorldPoint(Grid grid, Vector3 worldPosition)
    {

        if (!isWorldPositionInGrid(grid, worldPosition))
            return Vector2.one * -1;

        // move to the origin for easy conversion
        worldPosition = worldPosition - grid.getOrigin();
        Vector2 result = new Vector2(Mathf.RoundToInt(worldPosition.x / grid.getNodeSize()), Mathf.RoundToInt(worldPosition.y / grid.getNodeSize() ));
        if (result.x > grid.getGridSize().x || result.y > grid.getGridSize().y)
            return Vector2.one * -1;

        return result;
    } // nodeFromWorldPoint

    public static bool isWorldPositionInGrid(Grid grid, Vector3 worldPosition)
    {
        worldPosition = worldPosition - grid.getOrigin();
        return worldPosition.x >= 0 && worldPosition.y >= 0 &&
            worldPosition.x <= grid.getNodeSize() * grid.getGridSize().x && worldPosition.y <= grid.getNodeSize() * grid.getGridSize().y;
    } // isWorldPositionInGrid

    public static Vector3 worldFromPoint(Grid grid, Vector2 nodePos)
    {
        return worldFromPoint(grid, Mathf.RoundToInt(nodePos.x), Mathf.RoundToInt(nodePos.y));
    } // worldFromPoint

    public static Vector3 worldFromPoint(Grid grid, int x, int y)
    {
        // check if valid node position
        if(!isNodePositionInGrid(grid, x,y))
            return Vector3.one * -1;

        return grid.getOrigin() + new Vector3(x * grid.getNodeSize()+ grid.getNodeSize()* 0.5f, 0, 
                            y * grid.getNodeSize()+ grid.getNodeSize()* 0.5f);
    } // worldFromPoint

    private static bool isNodePositionInGrid(Grid grid, int x, int y)
    {
        return x >= 0 && y >= 0 && x < grid.getGridSize().x && y < grid.getGridSize().y;
    } // isNodePositionInGrid
}
