using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsMove : MonoBehaviour
{
    private Vector2 Move = new Vector2(1,0);
    public float speed;
    private float div = 1000;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float RandomDiv = Random.Range(0.9f * div, div);
        transform.Translate(Move.normalized * speed / RandomDiv);
    }
}
