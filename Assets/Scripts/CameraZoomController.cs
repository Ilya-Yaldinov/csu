using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    Vector3 touchStart;
    [SerializeField]
    private float zoomSpeed;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomOutMin;
    [SerializeField]
    private float zoomOutMax;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.005f);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment * zoomSpeed, zoomOutMin, zoomOutMax);
    }
}
