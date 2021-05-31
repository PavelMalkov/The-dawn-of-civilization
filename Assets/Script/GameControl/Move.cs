using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector2 Move1 = new Vector2(1, 0);
    public float speed;
    private float div = 1000;
    void FixedUpdate()
    {
        transform.Translate(Move1.normalized * speed / div);
        if (this.transform.position.x > 4)
        {
            Move1 = new Vector2(-1, 0);
        }
        if (this.transform.position.x < -4)
        {
            Move1 = new Vector2(1, 0);
        }
    }
}
