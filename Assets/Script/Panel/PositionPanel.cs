using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PositionPanel : MonoBehaviour
{
    int Begin, End;
    int X, Y;
    public float progress;
    public float step;
    private Vector2 Move = new Vector2(0, 1);
    string[] States = {"Open", "Close", "Stay"};
    public bool StateOpen = false;
    public bool StateCloused = false;

    public Vector2 Open;
    public Vector2 Closed;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Camera = GameObject.Find("Main Camera");
        Resolution resolution = Camera.GetComponent<Resolution>();
        X = resolution.X;
        Y = resolution.Y;
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
                //StateOpen = false;
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
                //StateCloused = false;
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
}
