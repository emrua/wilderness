using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isWalkable; // Whether this node is walkable
    public Vector3 worldPosition; // The position in the world
    public int gridX; // X coordinate in the grid
    public int gridY; // Y coordinate in the grid

    public int gCost; // Cost from the start node
    public int hCost; // Heuristic cost to the target node
    public Node parent; // Parent node for retracing the path

    public Node(bool walkable, Vector3 worldPos, int x = 0, int y = 0)
    {
        isWalkable = walkable;
        worldPosition = worldPos;
        gridX = x;
        gridY = y;
    }

    public int FCost
    {
        get { return gCost + hCost; }
    }
}

