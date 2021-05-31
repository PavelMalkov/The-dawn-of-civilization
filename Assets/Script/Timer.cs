using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    private float timer = 0;

    public static event  Action OnSecondTickEvent;

    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer > 10f)
        {
            timer = timer - Mathf.FloorToInt(timer);
            OnSecondTickEvent?.Invoke();
        }
    }
}
