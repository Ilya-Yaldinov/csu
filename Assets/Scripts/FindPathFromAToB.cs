using Pathfinder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NewBehaviourScript : MonoBehaviour
{

    public Tilemap tilemap;
    public Vector3Int startCell;
    public Vector3Int endCell;
    bool bothEndsAssigned = false;
    List<Vector3Int> path;
    Pathfinder<Vector3Int> pathfinder;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = new Pathfinder<Vector3Int>();
        pathfinder.GetHeuristicDistance = (l,r) => Map.main.GetManhattanDistance(l,r);
        pathfinder.GetNeighboursAndStepCosts = (x) => Map.main.GetNeighboursAndCosts(x);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (bothEndsAssigned)
            {
                startCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                startCell.z = 0;
            }
            else
            {
                endCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                endCell.z = 0;
                if (pathfinder.GeneratePath(startCell, endCell, out path))
                    path.Insert(0, startCell);
            }
            bothEndsAssigned = !bothEndsAssigned;
        }
        if (bothEndsAssigned && path.Count > 0)
            DrawPathGizmo();
    }
    void DrawPathGizmo()
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(tilemap.CellToWorld(path[i]), tilemap.CellToWorld(path[i+1]));
        }
    }
}
