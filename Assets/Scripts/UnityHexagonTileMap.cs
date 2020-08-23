using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityHexagonTileMap
{
    private Dictionary<Vector3, GameObject> _tiles;

    public GameObject this[Vector3 position]
    {
        get { return _tiles[position]; }
        set { _tiles[position] = value; }
    }

    public void Add(GameObject tile, Vector3 position)
    {
        _tiles.Add(position, tile);
    }
}
