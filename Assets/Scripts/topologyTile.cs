using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class topologyTile : Tile
{
    public bool isLand;
    public bool isTraversable;
    public int baseMovementCost;
    public int baseDefeseBonus;
}
