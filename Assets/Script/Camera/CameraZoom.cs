using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using System;

[AddComponentMenu("Camera-Control/CameraZoom")]
public class CameraZoom : MonoBehaviour
{

    Vector3 StartPos;
    Vector3 buf;

    public float Top;
    public float Down;

    public float Z = -20f;

    float speedzoom = 3;
    public float maxzoom = 5.4f;
    public float minzoom = 3;

    private float minX, maxX, minY, maxY;

    private float zoomSensitivity = 4.0f;
    private float zoom;

    private bool ClickOnUI = true;

    void Start()
    {
        zoom = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        if (Preferense.Play)
        {
            if (IsPointerOverUIObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    ClickOnUI = false;
                }

                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector3 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
                    Vector3 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

                    float disTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
                    float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

                    float difference = currentDistTouch - disTouch;

                    //zoom(difference * 0.01f);
                    zoom -= difference * 0.01f;
                    zoom = Mathf.Clamp(zoom, minzoom, maxzoom);
                }
                else // Scroll
                if (Input.GetMouseButton(0) && !ClickOnUI)
                {
                    float posX = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - StartPos.x) / 10;
                    float posY = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - StartPos.y) / 10;
                    CalcMinMax();
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x - posX, minX, maxX), Mathf.Clamp(transform.position.y - posY, minY, maxY), Z);
                }
            }
            else ClickOnUI = true;
            // Zoom
            zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            zoom = Mathf.Clamp(zoom, minzoom, maxzoom);
        }        
    }

    void LateUpdate()
    {
        CalcMinMax();
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, Time.deltaTime * speedzoom);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), Z);
    }

    void CalcMinMax()// здесс остановился
    {
        maxX = (maxzoom - zoom)  * 720 / 1280; //(maxzoom - Camera.main.orthographicSize);
        minX = -maxX;

        maxY = (Top + maxzoom - zoom);
        minY = (zoom - maxzoom - Down);
    }

    //When Touching UI
    // проверка активного места нажатия
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count == 0;
    }

    // проверка по координатам
    private bool IsPointerOverUIObject(float x, float y)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(x, y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count == 0;
    }
}
