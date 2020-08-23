using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    TileType Type { get; set; }
    void SetTile(ITile tile);
}
