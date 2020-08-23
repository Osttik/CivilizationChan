using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _tiles = new List<GameObject>();
    public List<GameObject> Tiles { get { return _tiles; } set { _tiles = value; } }

    private void Start()
    {
        Vector3 bounds = Tiles[0].GetComponent<MeshRenderer>().bounds.size;
        HexagonalMapCoordinates c = new HexagonalMapCoordinates(Vector3.zero, 1f, bounds.x, bounds.z);

        for(int i = 0; i < 25; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                var coordinates = c[i, j];
                print($"({i}, {j})  ->  ({coordinates.Item1}, {coordinates.Item2})");
                Instantiate(Tiles[Random.Range(0, 20)], new Vector3(coordinates.Item1, coordinates.Item2, 0f), Quaternion.Euler(240, -90, 90), transform);
            }
        }
    }

    public ITile GetTileByType(TileType type)
    {
        ITile returnedTile = Tiles.Find(tile => tile.GetComponent<ITile>().Type == type).GetComponent<ITile>();

        return returnedTile;
    }

    public void SetTileByType(GameObject tile, TileType type)
    {
        int prevTileIndex = Tiles.FindIndex(tileInArray => tileInArray.GetComponent<ITile>().Type == type);

        if (prevTileIndex > -1)
        {
            Tiles[prevTileIndex] = tile;
            return;
        }

        Tiles.Add(tile);
    }
}
