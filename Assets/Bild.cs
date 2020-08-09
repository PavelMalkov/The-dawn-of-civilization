using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Bild : MonoBehaviour
{
    public Image BildImage;
    public Image BildMain;
    public Text Name;
    public Text Timer;
    public Text txtCost;
    public Text txtMoney;
    public Scrollbar Progress;
    public Button Bay;
    public Button LevelUp;

    // вот эти данные должны храниться
    public float money;
    public float time;
    public float CostBay;

    private bool FactBay = false;
    // Start is called before the first frame update
    void Start()
    {
        FactBay = false; // нужно будет в дальнейшем сохранять
        if (FactBay == false)
        {
            Timer.gameObject.SetActive(false);
            Progress.gameObject.SetActive(false);
            LevelUp.gameObject.SetActive(false);
            BildMain.gameObject.SetActive(false);
            txtMoney.gameObject.SetActive(false);

            txtCost.text = "Стоимость покупки " + CostBay + "$";
            //txtCost.gameObject.SetActive(false);
        }        
    }

    /*public void ClickBay(Text text)
    {
        switch (text.text.ToString())
        {
            case "Дом":
                {
                    // здесь добавить появление дома на игровом поле
                    FactBay = true;
                    Bay.gameObject.SetActive(false);
                    LevelUp.gameObject.SetActive(true);
                    Timer.gameObject.SetActive(true);
                    Progress.gameObject.SetActive(true);
                    BildMain.gameObject.SetActive(true);
                    //StartCoroutine(SumMoney());
                    StartCoroutine(Time());
                    break;
                }

            default:
                break;
        }
    }*/

    // добавить защиту от повторного нажатия +
    public void ClickBay()
    {
        if (Data.count >= CostBay)
        {
            Data.count -= CostBay;
            // здесь добавить появление дома на игровом поле +
            FactBay = true;
            Bay.gameObject.SetActive(false);
            LevelUp.gameObject.SetActive(true);
            Timer.gameObject.SetActive(true);
            Progress.gameObject.SetActive(true);
            BildMain.gameObject.SetActive(true);
            txtMoney.gameObject.SetActive(true);
            //txtCost.gameObject.SetActive(false);
            txtMoney.text = money + "$";
            //StartCoroutine(SumMoney());
            StartCoroutine(Time());
        }
    }

    IEnumerator Time()
    {
        float timelocal = time;
        int sec, min, hour;
        int secAll, minAll, hourAll;
        //hour = (int)TimeSpan.FromSeconds(time).TotalHours; // 0,0166666666666667
        
        while (true)
        {
            // установить таймер
            minAll = (int)TimeSpan.FromSeconds(time).TotalMinutes; // 1
            secAll = (int)TimeSpan.FromSeconds(time - (minAll * 60)).TotalSeconds; // 60

            min = (int)TimeSpan.FromSeconds(timelocal).TotalMinutes; // 1
            sec = (int)TimeSpan.FromSeconds(timelocal - (min * 60)).TotalSeconds; // 60
            timelocal -= 1;

            string res = String.Format("{0:d2}:{1:d2}/({2:d2}:{3:d2})", min,sec,minAll,secAll);
            
            Timer.text = res;

            // установить прогресс
            Progress.size = (time-timelocal-1)/time; // тут всегда 0
            yield return new WaitForSeconds(1);
            if (timelocal == 0) 
            {
                Data.count += money;
                timelocal = time;
                
            }; 
        }
    }
}
