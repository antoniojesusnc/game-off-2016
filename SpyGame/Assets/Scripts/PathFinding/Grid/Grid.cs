using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Grid
{    
    HashSet<Vector2> _blockNodes;

    Vector2 _gridSize;
    float _nodeSize;
    Vector3 _worldBottomLeft;



    public Grid(Vector2 gridSize, float nodeSize, Vector3 originPoint)
    {
        _gridSize = gridSize;
        _nodeSize = nodeSize;
        _worldBottomLeft = originPoint;
        _blockNodes = new HashSet<Vector2>();
    } // Grid

    public void addBlockNode(Vector2 nodeToBlock)
    {
        _blockNodes.Add(nodeToBlock);
    } // addBlockNode

    // gets & sets
    public Vector3 getOrigin()
    {
        return _worldBottomLeft;
    } // getOrigin

    public Vector2 getGridSize()
    {
        return _gridSize;
    } // getSize

    public float getNodeSize()
    {
        return _nodeSize;
    } // getNodeSize

    public HashSet<Vector2> getBlockNodes()
    {
        return _blockNodes;
    }
}