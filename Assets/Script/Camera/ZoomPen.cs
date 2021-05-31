using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomPen : MonoBehaviour
{
    Vector3 touch;

    bool ClickUI = false;
    bool ClickNotButContinueUI = false;

    public float zoomMin = 1;
    public float zoomMax = 5;

    void Update()
    {
        if (IsPointerOverUIObject() || !ClickUI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ClickUI = false;    // клик не по UI
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

                zoom(difference * 0.01f);

            }
            else if (Input.GetMouseButton(0))
            {
                if (!ClickUI)
                {
                    Vector3 direction = touch - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Camera.main.transform.position += direction;
                }
            }
        }
        else ClickUI = true; // Клик по UI
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomMin, zoomMax);
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
