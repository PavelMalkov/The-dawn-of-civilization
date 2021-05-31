using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionPanel : MonoBehaviour
{
    int Begin, End;
    float X, Y;
    public float progress;
    public float step;
    private Vector2 Move = new Vector2(0, 1);
    bool StateOpen = false;

    Vector2 Open;
    Vector2 Closed;    

    void Start()
    {
        // Расчет размеров панели
        RectTransform Panel = GetComponent<RectTransform>(); ;
        X = Currency.X;
        Y = Currency.Y;
        RectTransformExtensions.SetTop(Panel,(Y / 2));

        print(X + " " + Y);

        Closed = new Vector2(0,-Y);
        Open = new Vector2(0, -Y / 2);
        transform.localPosition = Closed;
    }  

    // Update is called once per frame
    void Update()
    {
        if (StateOpen)
        {
            if (transform.localPosition.x == Open.x && transform.localPosition.y == Open.y)
            {
                progress = 0;
            }
            else
            {
                transform.localPosition = Vector2.Lerp(Closed, Open, progress / 1000);
                progress += step;
            }            
        }
        if (!StateOpen)
        {
            if (transform.localPosition.x == Closed.x && transform.localPosition.y == Closed.y)
            {
                progress = 0;
            }
            else
            {
                transform.localPosition = Vector2.Lerp(Open, Closed, progress / 1000);
                progress += step;
            }
        }
    }

    public void ChancheStateOpen()
    {
        StateOpen = StateOpen ? false : true;
    }

    public void ClouseOpen()
    {
        StateOpen = false;
    }
}
