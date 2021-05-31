using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResheachView : MonoBehaviour
{
    // Это объекты которые изменяются и привязываются в самом префабе поэтому они скрыты
    [SerializeField]
    private Image BildImage;
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private TextMeshProUGUI TxtBonus;
    [SerializeField]
    private TextMeshProUGUI txtCost;
    [SerializeField]
    private GameObject Bay;

    [SerializeField]
    private GameObject Video, Al; // просмотр видео покупка за алмазы 

    [SerializeField]
    private Image Open1, Open2, Open3;
    [SerializeField]
    private Image Open1Bg, Open2Bg, Open3Bg;

    [SerializeField]
    private Scrollbar Progress;
    [SerializeField]
    private TextMeshProUGUI Timer;

    // вот эти данные должны храниться
    public int IdBild;
    public int IdResheach; // индекс в массиве
    public int IdBoosts; // индекс в массиве
    public int State; // состояние (его номер)

    public float bonus; // начальный бонус
    public float bonusdiv; // шаг получаемого бонуса

    // эти данные находятся
    float CostBay;

    Bild ThisBild; // Здание
    Resheach Resheach;


    void Start()
    {
        //print(Id);
        // изменить значение для наших блоков
        RectTransform a = this.GetComponent<RectTransform>();
        float SizeX = Currency.X * 0.98f;
        //float SizeY = SizeX / 5.7857f;
        float SizeY = SizeX / 4.5f;
        a.sizeDelta = new Vector2(SizeX, SizeY); // задаем размер наших блоков зданий

        ThisBild = SaveControl.GetBildSetting(IdBild); // получение данных о здании
        Resheach = SaveControl.GetReseachSetting(IdResheach); // получение данных о ускорении

        // отображение изображений
        BildImage.sprite = Resheach.ResheachImage;

        // отображение улучшаемого дома и новых возможностей
        if (Open1.sprite != null) Open1.sprite = Resheach.Open1;
        if (Open2.sprite != null) Open2.sprite = Resheach.Open2;
        if (Open3.sprite != null) Open3.sprite = Resheach.Open3;

        // вставка текста
        Name.text = Resheach.NameResheach; // текст

        // получение значения бонуса и его шаг изменения
        bonus = Resheach.bonus;
        bonusdiv = Resheach.bonusdiv;

        //получение возможности ускорения
        IdBoosts = Resheach.IdBoosts;

        CostBay = Resheach.CostBay;

        State = Resheach.State;
        if (State == 1) SaveControl.SetBildSetting(IdBild, true); // устанавливаем воможность покупки зданий

        if (Resheach.BayNow) // идет ли исследование
        {
            // не идет исследование, можно исследовать
            ResheachNow();

        }
        else // идет исследование, нельзя исследовать
        {
            NoResheachNow();

            txtCost.text = "" + TextControl.ConvertTxt(CostBay);
        }
        MyProperty = 0;
        Bay.GetComponent<ButtonControl>().SetButton();
    }

    private void ShowImages()
    {
        if (State == 0)
        {
            TxtBonus.gameObject.SetActive(false);

            Open1.gameObject.SetActive(Resheach.Open1);
            Open2.gameObject.SetActive(Resheach.Open2);
            Open3.gameObject.SetActive(Resheach.Open3);
            Open1Bg.gameObject.SetActive(Resheach.Open1);
            Open2Bg.gameObject.SetActive(Resheach.Open2);
            Open3Bg.gameObject.SetActive(Resheach.Open3);
        }
        else
        {
            TxtBonus.gameObject.SetActive(true);

            Open1.gameObject.SetActive(false);
            Open2.gameObject.SetActive(false);
            Open3.gameObject.SetActive(false);
            Open1Bg.gameObject.SetActive(false);
            Open2Bg.gameObject.SetActive(false);
            Open3Bg.gameObject.SetActive(false);
        }

    }

    private void ResheachNow()
    {
        ShowImages();
        Bay.gameObject.SetActive(false);
        Video.gameObject.SetActive(true);
        Al.gameObject.SetActive(true); // просмотр видео покупка за алмазы 

        Progress.gameObject.SetActive(true);
        Timer.gameObject.SetActive(true);
    }

    private void NoResheachNow()
    {
        ShowImages();
        Bay.gameObject.SetActive(true);
        Video.gameObject.SetActive(false);
        Al.gameObject.SetActive(false); // просмотр видео покупка за алмазы 

        Progress.gameObject.SetActive(false);
        Timer.gameObject.SetActive(false);
    }

    // работа кнопки
    private int myPropVar;

    public int MyProperty
    {
        get { return myPropVar; }
        set
        {
            myPropVar = value;
            Bay.GetComponent<ButtonControl>().SetData(myPropVar);
        }
    }

    private void check()
    {
        if (Currency.Science >= CostBay)
        {
            if (myPropVar != 0) MyProperty = 0;
        }
        else if (myPropVar != 2) MyProperty = 2;
    }

    private void Update()
    {
        check();
    }

    // клик по покупки
    public void ClickBay()
    {
        // необходимо переделать ускорение и бонусы.
        if (Currency.Science >= CostBay && Resheach.BayNow == false)
        {
            Currency.Science -= CostBay;
            ResheachNow();
            StartCoroutine(Time()); // запускаем корутину прогресса
            Resheach.BayNow = true;
        }
    }

    IEnumerator Time()
    {
        if (Resheach.timelocal <= 0) Resheach.timelocal = Resheach.time;
        int sec, min; //hour;
        int secAll, minAll;// hourAll;
                           //hour = (int)TimeSpan.FromSeconds(time).TotalHours; 

        while (Resheach.timelocal >= 0)
        {
            txtCost.text = TextControl.ConvertTxt(Resheach.CostBay);
            // установить таймер
            minAll = (int)TimeSpan.FromSeconds(Resheach.time).TotalMinutes; // 1
            secAll = (int)TimeSpan.FromSeconds(Resheach.time - (minAll * 60)).TotalSeconds; // 60

            min = (int)TimeSpan.FromSeconds(Resheach.timelocal).TotalMinutes; // 1
            sec = (int)TimeSpan.FromSeconds(Resheach.timelocal - (min * 60)).TotalSeconds; // 60
            Resheach.timelocal -= 0.05f;

            string res = String.Format("{0:d2}:{1:d2}/({2:d2}:{3:d2})", min, sec, minAll, secAll);

            Timer.text = res;

            // установить прогресс
            Progress.size = (Resheach.time - Resheach.timelocal) / Resheach.time; // тут всегда 0
            yield return new WaitForSeconds(0.05f);
        }

        // таймер закончился
        if (State == 0)
        {
            State = 1; // меняем статус на бонус здания
            SaveControl.SetBildSetting(IdBild, true); // устанавливаем воможность покупки зданий
            SaveControl.SetBoostSetting(IdBoosts, true); // устанавливаем воможность покупки зданий
        }
        else
        {
            ThisBild.Money *= bonus / (bonus - bonusdiv);
            bonus += bonusdiv;
        }
        Resheach.BayNow = false;
        Bay.GetComponent<ButtonControl>().SetButton();
        TxtBonus.text = "х " + bonus.ToString();
        NoResheachNow();
    }
}