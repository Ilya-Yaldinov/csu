using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{

    public static Map main;
    [SerializeField] Tilemap firstFloor;
    [SerializeField] Tilemap firstFloorPaths;
    // Start is called before the first frame update
    void Start()
    {
        if (main == null) main = this; else throw new System.Exception("Карта может быть только одной");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetMovementCost(Vector3Int tilePos)
    {
        return firstFloor.GetTile<topologyTile>(tilePos).baseMovementCost;
    }

    [ExecuteAlways]

    public topologyTile GetTopologyTile(Vector3Int tilePos)
    {
        return firstFloor.GetTile<topologyTile>(tilePos);
    }

    public float GetManhattanDistance(Vector3Int A, Vector3Int B)
    {
        return Mathf.Abs(A.x - B.x) + Mathf.Abs(A.y + B.y);
    }

    public Dictionary<Vector3Int, float> GetNeighboursAndCosts(Vector3Int pos)
    {
        Dictionary<Vector3Int,float> result = new Dictionary<Vector3Int,float>();
        foreach(Vector3Int neighbour in pos.neighbours())
        {
            result.Add(neighbour, GetMovementCost(neighbour));
        }
        return result;
    }
}
