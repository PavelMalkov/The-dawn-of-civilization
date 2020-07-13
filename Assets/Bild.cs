using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Bild : MonoBehaviour
{
    public Image bild;
    public Text names;
    public Scrollbar progress;
    public Text timer;
    public Button bay;

    public int money;
    public float time;


    private bool FactBay = false;
    // Start is called before the first frame update
    void Start()
    {
        if (FactBay == false) {timer.gameObject.SetActive(false);progress.gameObject.SetActive(false);}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // добавить защиту от повторного нажатия
    public void ClickBay()
    {
        string str = names.text.ToString();
        switch (str)
        {
            case "Дом":
                {
                    // здесь добавить появление дома на игровом поле
                    FactBay = true;
                    timer.gameObject.SetActive(true);
                    progress.gameObject.SetActive(true);
                    //StartCoroutine(SumMoney());
                    StartCoroutine(Time());
                    break;
                }

            default:
                break;
        }
    }
    /*
    IEnumerator SumMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

        }
    }
    */

    IEnumerator Time()
    {
        float timelocal = time;
        int sec, min, hour;
        //hour = (int)TimeSpan.FromSeconds(time).TotalHours; // 0,0166666666666667
        
        while (true)
        {
            // установить таймер
            min = (int)TimeSpan.FromSeconds(timelocal).TotalMinutes; // 1
            sec = (int)TimeSpan.FromSeconds(timelocal - (min * 60)).TotalSeconds; // 60
            timelocal -= 1;
            timer.text = min + ":" + sec;

            // установить прогресс
            progress.size = (time-timelocal-1)/time; // тут всегда 0
            yield return new WaitForSeconds(1);
            if (timelocal == -1)
            {
                timelocal = time;
            }; // сделать сложение money с монетами;
        }
    }
}
