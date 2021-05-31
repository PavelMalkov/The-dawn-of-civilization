using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanelAnim : MonoBehaviour
{
    public float speed = 10;

    void Update()
    {
        gameObject.transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
