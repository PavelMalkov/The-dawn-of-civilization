﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsMove : MonoBehaviour
{
    private Vector2 Move = new Vector2(1,0);
    public float speed;
    private float div = 1000;
    float RandomDiv;

    private void Start()
    {
        RandomDiv = Random.Range(0.9f * div, div);
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        transform.Translate(Move.normalized * speed / RandomDiv);
        if (this.transform.position.x > 4)
        {
            Destroy(gameObject);
        }
    }
}
