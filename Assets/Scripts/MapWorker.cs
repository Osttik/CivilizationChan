using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapWorker : MonoBehaviour
{
    private UnityHexagonTileMap _map { get; set; } = new UnityHexagonTileMap();

    public GameObject GetTileByPosition(Vector3 position)
    {
        GameObject tile;
        try
        {
            tile = _map[position];
            
        }
        catch (Exception)
        {
            tile = null;
        }

        return tile;
    }

    public bool SetTileByPosition(GameObject tile)
    {
        return SetTileByPosition(tile, tile.transform.position);
    }

    public bool SetTileByPosition(GameObject tile, Vector3 position)
    {
        bool isCorrect = true;

        try
        {
            _map.Add(tile, position);
        }
        catch (Exception)
        {
            isCorrect = false;
        }

        return isCorrect;
    }
}