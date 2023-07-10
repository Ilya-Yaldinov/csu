using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityLineRendererTest : MonoBehaviour
{
    private LineRenderer pathLine;
    private Vector3 previousPosition;

    [SerializeField]
    private GameObject walker;

    [SerializeField]
    private float minDistance = 0.1f;

    private void Start()
    {
        pathLine = GetComponent<LineRenderer>();

        pathLine.startColor = Color.yellow;
        pathLine.endColor = Color.yellow;
        pathLine.startWidth = 3f;
        pathLine.endWidth = 3f;

        previousPosition = walker.transform.position;

    }

    private void Update()
    {
        if(walker.transform.position != previousPosition) 
        {
            Vector3 currentPosition = walker.transform.position;
            currentPosition.z = 1f;

            if(Vector3.Distance(previousPosition, currentPosition) > minDistance) 
            {
                pathLine.positionCount++;
                pathLine.SetPosition(pathLine.positionCount - 1, currentPosition);
                previousPosition = currentPosition;

            }
        }

        if(Input.GetMouseButtonDown(0)) 
        {
            pathLine.positionCount = 0;
        }
    }
}