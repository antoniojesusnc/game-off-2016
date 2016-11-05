using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GridFactory : MonoBehaviour
{
    public LayerMask _unwalkableMask;
    public Vector2 _gridContainer;
    public float _nodeSize;
    [Header("Update Time")]
    public float _timeStampTimeToUpdate;

    Grid _grid;

    // because ExecuteInEditMode, Update is Call when something in the SceneChange
    public void Update()
    {
        // check non nullable vars
        if (!canUpdateInEditorMode())
            return;

        CreateGrid();
    } // Update

    // check that the Update can be done
    private bool canUpdateInEditorMode()
    {
        return _nodeSize > 0 &&
                //_gridContainer != null &&
                _gridContainer.x > 0 &&
                _gridContainer.y > 0 &&
                _timeStampTimeToUpdate >= 1;
    } // canUpdateInEditorMode

    void CreateGrid()
    {
        // the origin point is down left
        Vector3 originPoint = transform.position;
        Vector2 gridSize = new Vector2(_gridContainer.x / _nodeSize, _gridContainer.y / _nodeSize);
        _grid = new Grid(gridSize, _nodeSize, originPoint);

        Vector3 worldPoint;
        Vector2 temp = Vector2.zero;
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                temp.Set(x, y);
                worldPoint = GridUtils.worldFromPoint(_grid, temp);
                //worldPoint = originPoint+ Vector3.right * ( x * _nodeSize + _nodeSize * 0.5f ) + Vector3.forward * ( y * _nodeSize + _nodeSize * 0.5f );
                // is the node Colidede with the "unwalkable mask" choosen by inspector, mean is not a walkable node
                if (Physics.CheckSphere(worldPoint, _nodeSize*0.9f, _unwalkableMask))
                {       
                    _grid .addBlockNode(temp);
                }
            }
        }
    } // GenerateGrid

    /// <summary>
    /// Method for show the grid in the sceneMode
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.right*_gridContainer.x*0.5f + Vector3.forward*_gridContainer.y*0.5f, 
            new Vector3(_gridContainer.x, 1, _gridContainer.y));
        
        if(!canUpdateInEditorMode())
            return;

        if(_grid == null)
        {
            //Debug.Log("Grid Null, update some value in DebugGrid");
            return;
        }

        Vector2 nodeToCheck = Vector2.zero;
        Vector3 nodeSize = Vector3.one * _grid.getNodeSize()*0.5f;
        for (int x = 0; x < _grid.getGridSize().x; x++)
        {
            for (int y = 0; y < _grid.getGridSize().y; y++)
            {
                nodeToCheck.Set(x, y);
                if(!_grid.getBlockNodes().Contains(nodeToCheck))
                    //Gizmos.DrawCube(Vector3.up* transform.position.y +GridUtils.worldFromPoint(_grid, nodeToCheck), nodeSize);
                Gizmos.DrawCube(0*Vector3.up * transform.position.y + GridUtils.worldFromPoint(_grid, nodeToCheck), nodeSize);
            }
        }
    } // OnDrawGizmos
}
