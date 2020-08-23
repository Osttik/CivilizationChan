using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, ITile
{
    public TileType Type { get; set; } = TileType.EmptyTile;
    public void SetTile(ITile tile)
    {
        Type = tile.Type;
    }
}