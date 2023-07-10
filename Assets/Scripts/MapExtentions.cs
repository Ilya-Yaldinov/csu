using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapExtentions
{
    public static Vector3Int north(this Vector3Int vector) { return vector + Vector3Int.right; }
    public static Vector3Int south(this Vector3Int vector) { return vector + Vector3Int.left; }
    public static Vector3Int east(this Vector3Int vector) { return vector + Vector3Int.up; }
    public static Vector3Int west(this Vector3Int vector) { return vector + Vector3Int.down; }


    public static Vector3Int[] neighbours(this Vector3Int vector)
    {
        return new Vector3Int[]
        {
            vector.north(), vector.south(), vector.east(), vector.west()
        };
    }
}
