using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public int IdPanel; 


    // Start is called before the first frame update
    void Start()
    {
        RectTransform Panel = GetComponent<RectTransform>(); ;
        X = Data.X;
        Y = Data.Y;

        RectTransformExtensions.SetTop(Panel,Y / 2);
        print(X + " " + Y);

        Closed = new Vector2(0,-Y);
        Open = new Vector2(0, -Y/2);
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
        //OpenControl.Open(IdPanel);
    }

    public void ClouseOpen()
    {
        StateOpen = false;
        //OpenControl.Open(IdPanel);
    }
}
