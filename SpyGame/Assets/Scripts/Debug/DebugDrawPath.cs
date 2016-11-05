using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class DebugDrawPath : MonoBehaviour
{

    public Transform _origin;
    public Transform _destiny;
    public GridFactory _grid;

    private List<Vector2> _path;
    private bool canDoUpdateInEditorMode()
    {
        return _origin != null &&
                _destiny != null &&
                _grid != null &&
                _grid.getGrid() != null;
    }

    void Update()
    {
        if (!canDoUpdateInEditorMode())
            return;

        CalculatePath();
    } // OnUpdate

    public void CalculatePath()
    {
        _path = AStartSearch.Search(_grid.getGrid(), _origin.position, _destiny.position);
    } // CalculatePath

    public void OnDrawGizmos()
    {
        if (!canDoUpdateInEditorMode())
            return;

        if(_path != null)
        {
            Vector3 origin;
            Vector3 destiny;
            Gizmos.color = Color.green;
            for (int i = _path.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                    continue;

                destiny = GridUtils.worldFromPoint(_grid.getGrid(), _path[i]);
                origin = GridUtils.worldFromPoint(_grid.getGrid(), _path[i-1]);
                Gizmos.DrawLine(origin, destiny);
            }
        }
    } // OnDrawGizmos
}
