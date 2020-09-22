using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BildView : MonoBehaviour
{
    // Это объекты которые изменяются и привязываются в самом префабе поэтому они скрыты
    [HideInInspector]
    public Image BildImage;
    [HideInInspector]
    public Text Name;
    [HideInInspector]
    public Text Timer;
    [HideInInspector]
    public Text txtCost;
    [HideInInspector]
    public Text txtUp;
    [HideInInspector]
    public Text txtMoney;
    [HideInInspector]
    public Scrollbar Progress;
    [HideInInspector]
    public Button Bay;
    [HideInInspector]
    public Button LevelUp;

    // вот эти данные должны храниться
    public int Id; // номер здания
    public Image BildMain; // Изображение дома на игровом поле

    Bild ThisBild;


    void Start()
    {
        print(Id);
        ThisBild = BildControl.GetBildSetting(Id);

        RectTransform a = this.GetComponent<RectTransform>();
        float SizeX = Data.X * 0.9f;
        float SizeY = SizeX / 5.7857f;
        a.sizeDelta = new Vector2(SizeX, SizeY); // задаем размер наших блоков зданий

        BildImage.sprite = ThisBild.bildimage;

        if (ThisBild.FactBay == false) // здание еще не куплено
        {
            NotBayView();
            Name.text = ThisBild.homeName;
            BildImage.sprite = ThisBild.bildimage;
            txtCost.text = "Стоимость покупки " + Data.ConvertTxt(ThisBild.CostBay) + "$";
        }
        else // здание уже куплено
        {
            bayView();
            Name.text = ThisBild.homeName + "";
            txtUp.text = "Стоимость повышения уровня " + Data.ConvertTxt(ThisBild.CostUp) + "$";
            txtMoney.text = Data.ConvertTxt(ThisBild.Money) + "$";
            StartCoroutine(Time());
        }
    }

    public void ClickBay()
    {
        if (Data.CountMoney >= ThisBild.CostBay)
        {
            Data.CountMoney -= ThisBild.CostBay;
            ThisBild.FactBay = true;

            bayView();
            // прогресс строительства (количество домов)
            Data.BildCount++;
            // повышение уровня
            txtUp.text = "Стоимость повышения уровня " + Data.ConvertTxt(ThisBild.CostUp) + "$";
            txtMoney.text = Data.ConvertTxt(ThisBild.Money) + "$";
            StartCoroutine(Time());
        }
        // можно добавить что денег не достаточно
    }

    private void bayView()
    {
        Bay.gameObject.SetActive(false);
        LevelUp.gameObject.SetActive(true);
        Timer.gameObject.SetActive(true);
        Progress.gameObject.SetActive(true);
        BildMain.gameObject.SetActive(true);
        txtMoney.gameObject.SetActive(true);
        txtUp.gameObject.SetActive(true);
        Bay.gameObject.SetActive(false);
    }

    private void NotBayView()
    {
        Timer.gameObject.SetActive(false);
        Progress.gameObject.SetActive(false);
        LevelUp.gameObject.SetActive(false);
        BildMain.gameObject.SetActive(false);
        txtMoney.gameObject.SetActive(false);
        txtUp.gameObject.SetActive(false);
    }
    
    // добавить защиту от повторного нажатия +
    public void ClickUp()
    {
        if (Data.CountMoney >= ThisBild.CostUp)
        {
            Data.CountMoney -= ThisBild.CostUp;

            ThisBild.countUp++; // повышение уровня

            ThisBild.Money *= ThisBild.coefficientUp;
            ThisBild.CostUp *= ThisBild.coefficientMoney;
            txtMoney.text = Data.ConvertTxt(ThisBild.Money) + "$";
            txtUp.text = "Стоимость повышения уровня " + Data.ConvertTxt(ThisBild.CostUp) + "$";
        }
    }


    IEnumerator Time()
    {
        if (ThisBild.timelocal == 0) ThisBild.timelocal = ThisBild.time;
        int sec, min; //hour;
        int secAll, minAll;// hourAll;
        //hour = (int)TimeSpan.FromSeconds(time).TotalHours; 
        
        while (true)
        {
            // установить таймер
            minAll = (int)TimeSpan.FromSeconds(ThisBild.time).TotalMinutes; // 1
            secAll = (int)TimeSpan.FromSeconds(ThisBild.time - (minAll * 60)).TotalSeconds; // 60

            min = (int)TimeSpan.FromSeconds(ThisBild.timelocal).TotalMinutes; // 1
            sec = (int)TimeSpan.FromSeconds(ThisBild.timelocal - (min * 60)).TotalSeconds; // 60
            ThisBild.timelocal -= 1;

            string res = String.Format("{0:d2}:{1:d2}/({2:d2}:{3:d2})", min,sec,minAll,secAll);
            
            Timer.text = res;

            // установить прогресс
            Progress.size = (ThisBild.time - ThisBild.timelocal -1)/ ThisBild.time; // тут всегда 0
            yield return new WaitForSeconds(1);
            if (ThisBild.timelocal == 0) 
            {
                Data.CountMoney += ThisBild.Money;
                ThisBild.timelocal = ThisBild.time;
            }; 
        }
    }
}
