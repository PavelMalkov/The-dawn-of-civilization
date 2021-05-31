using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BildView : MonoBehaviour
{
    // Это объекты которые изменяются и привязываются в самом префабе поэтому они скрыты
    [SerializeField]
    private Image BildImage;
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private TextMeshProUGUI TimerBild;
    [SerializeField]
    private TextMeshProUGUI txtCost;
    [SerializeField]
    private TextMeshProUGUI txtUp;
    [SerializeField]
    private TextMeshProUGUI txtMoney;
    [SerializeField]
    private Scrollbar Progress;
    [SerializeField]
    private GameObject Bay;
    [SerializeField]
    private GameObject LevelUp;

    [SerializeField]
    private Image ImageCurrency;

    [SerializeField]
    private Sprite SpriteGold;
    [SerializeField]
    private Sprite SpriteScience;
    [SerializeField]
    private Sprite SpriteMaterial;
    [SerializeField]
    private Sprite SpriteFood;

    [SerializeField]
    private Material MaterialGold;    
    [SerializeField]
    private Material MaterialScience;    

    [SerializeField]
    private TextMeshProUGUI LevelView;
    /*
    //[HideInInspector]
    [SerializeField]
    private Text LevelBegin;
    ///[HideInInspector]
    [SerializeField]
    private Text LevelEnd;*/
    [SerializeField]
    private Scrollbar ProgressLevel;

    // вот эти данные должны храниться
    public int Id; // номер здания
    int IdBoost; // номер ускорения
    public GameObject BildMain; // Изображение дома на игровом поле

    private float step = 5;
    private float Level = 0;
    //private float LevelBeginInt = 0;
    //private float LevelEndInt = 5;

    Bild ThisBild;
    bool StartCorutin = false;

    GameObject Parent;

    public bool GetCanBay()
    {
        return ThisBild.YouCanBay;
    }

    void Start()
    {
        //print(Id);

        ThisBild = SaveControl.GetBildSetting(Id); // получение данных о здании получение здания по Id
        IdBoost = ThisBild.IdBoost;

        RectTransform a = this.GetComponent<RectTransform>();
        float SizeX = Currency.X * 0.98f;
        //float SizeY = SizeX / 5.7857f;
        float SizeY = SizeX / 4.5f;
        a.sizeDelta = new Vector2(SizeX, SizeY); // задаем размер наших блоков зданий

        // устанавдиваем изображение валюты
        if (ThisBild.NumCurrent == 0)
        {
            ImageCurrency.sprite = SpriteGold;
            txtMoney.fontMaterial = MaterialGold;
        }
        if (ThisBild.NumCurrent == 1) 
        {
            ImageCurrency.sprite = SpriteScience;
            txtMoney.fontMaterial = MaterialScience;
        }

        UpdateData();

        Timer.OnSecondTickEvent += Timer_OnSecondTickEvent;
    }
    int i = 0;
    private void Timer_OnSecondTickEvent()
    {
        i++;
        Debug.Log(i);
    }


    // обновление данных
    public void UpdateData()
    {
        ThisBild = SaveControl.GetBildSetting(Id); // получение данных о здании получение здания по Id
        IdBoost = ThisBild.IdBoost;

        BildImage.sprite = ThisBild.bildimage;
        Name.text = ThisBild.homeName;

        // если здание еще не изучено
        if (ThisBild.YouCanBay == false)
        {
            BildMain.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else if (ThisBild.FactBay == false) // здание еще не куплено
        {
            NotBayView();
            txtCost.text = TextControl.ConvertTxt(ThisBild.CostBay);
        }
        else // здание уже куплено
        {
            bayView();
            LevelSet();

            txtUp.text = TextControl.ConvertTxt(ThisBild.CostUp);
            txtMoney.text = TextControl.ConvertTxt(ThisBild.Money);

            if (!StartCorutin)
            {
                StartCoroutine(Time());
                StartCorutin = true;
            }
        }

        // включаем кнопки (нужно добавить проверку, хватает ли денег)
        check();
        MyProperty = 0;
        Bay.GetComponent<ButtonControl>().SetButton();
        LevelUp.GetComponent<ButtonControl>().SetButton();
    }    

    private void Update()
    {
        check(); // проверка можно ли купить
    }

    private int myPropVar;

    public int MyProperty
    {
        get { return myPropVar; }
        set { 
            myPropVar = value; 
            Bay.GetComponent<ButtonControl>().SetData(myPropVar);
            LevelUp.GetComponent<ButtonControl>().SetData(myPropVar); 
        }
    }

    // проверка можно ли совешить покупку
    private void check()
    {
        
        float thisCost;
        if (ThisBild.FactBay) thisCost = ThisBild.CostUp;
        else thisCost = ThisBild.CostBay;
        if (Currency.Gold >= thisCost)
        {
            if (myPropVar != 0) MyProperty = 0;
        }
        else
        {
            if (myPropVar != 2) MyProperty = 2;
        }
    }

    private void AnimationUpBay()
    {
        if (BildMain.GetComponent<HouseAnimControl>())
            BildMain.GetComponent<HouseAnimControl>().SetAnimBildOrUp(); // проигрывание анимации
    }

    public void ClickBay()
    {
        if (Currency.Gold >= ThisBild.CostBay)
        {
            Currency.Gold -= ThisBild.CostBay;
            ThisBild.FactBay = true;

            bayView();
            // прогресс строительства (количество домов)
            Currency.BildCount++;
            // повышение уровня
            txtUp.text = TextControl.ConvertTxt(ThisBild.CostUp);
            txtMoney.text = TextControl.ConvertTxt(ThisBild.Money);

            // обновление
            this.gameObject.GetComponentInParent<PanelCreate>().ChancheActive();

            ThisBild.countUp++;
            LevelSet(); // установка прогресса
            StartCoroutine(Time());
            // Установка покупки возможных ускорений
            SaveControl.SetBoostSetting(IdBoost, true);

            AnimationUpBay(); // проигрывание анимации
        }
        // можно добавить что денег не достаточно
    }

    private void LevelSet()
    {
        //LevelBegin.text = (LevelBeginInt + step * Math.Floor(ThisBild.countUp / step)).ToString();
        //LevelEnd.text = (LevelEndInt + step * Math.Floor(ThisBild.countUp / step)).ToString();
        ProgressLevel.size = (ThisBild.countUp  % step) / step;
        LevelView.text = ThisBild.countUp.ToString();
    }

    private void bayView()
    {
        Bay.gameObject.SetActive(false);
        LevelUp.gameObject.SetActive(true);
        TimerBild.gameObject.SetActive(true);
        Progress.gameObject.SetActive(true);
        
        BildMain.gameObject.SetActive(true);

        txtMoney.gameObject.SetActive(true);
        txtUp.gameObject.SetActive(true);
        Bay.gameObject.SetActive(false);
        ImageCurrency.gameObject.SetActive(true);
        ProgressLevel.gameObject.SetActive(true);
        LevelView.gameObject.SetActive(true);
    }

    private void NotBayView()
    {
        TimerBild.gameObject.SetActive(false);
        Progress.gameObject.SetActive(false);
        LevelUp.gameObject.SetActive(false);
        BildMain.gameObject.SetActive(false);
        txtMoney.gameObject.SetActive(false);
        txtUp.gameObject.SetActive(false);
        ImageCurrency.gameObject.SetActive(false);
        ProgressLevel.gameObject.SetActive(false);
        LevelView.gameObject.SetActive(false);
    }
    
    // добавить защиту от повторного нажатия +
    public void ClickUp()
    {
        if (Currency.Gold >= ThisBild.CostUp)
        {
            Currency.Gold -= ThisBild.CostUp;

            ThisBild.countUp++; // повышение уровня

            ThisBild.Money *= ThisBild.coefficientMoney;
            ThisBild.CostUp *= ThisBild.coefficientUp;
            txtMoney.text = TextControl.ConvertTxt(ThisBild.Money);
            txtUp.text = TextControl.ConvertTxt(ThisBild.CostUp);

            LevelSet(); // повышение уровня 
            AnimationUpBay(); // проигрывание анимаии
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
            txtMoney.text = TextControl.ConvertTxt(ThisBild.Money);
            // установить таймер
            minAll = (int)TimeSpan.FromSeconds(ThisBild.time).TotalMinutes; // 1
            secAll = (int)TimeSpan.FromSeconds(ThisBild.time - (minAll * 60)).TotalSeconds; // 60

            min = (int)TimeSpan.FromSeconds(ThisBild.timelocal).TotalMinutes; // 1
            sec = (int)TimeSpan.FromSeconds(ThisBild.timelocal - (min * 60)).TotalSeconds; // 60
            ThisBild.timelocal -= 0.05f;

            string res = String.Format("{0:d2}:{1:d2}/({2:d2}:{3:d2})", min,sec,minAll,secAll);
            
            TimerBild.text = res;

            // установить прогресс
            Progress.size = (ThisBild.time - ThisBild.timelocal)/ ThisBild.time; // тут всегда 0
            yield return new WaitForSeconds(0.05f);
            if (ThisBild.timelocal <= 0)
            {
                if (ThisBild.NumCurrent == 0) Currency.Gold += ThisBild.Money;
                if (ThisBild.NumCurrent == 1) Currency.Science += ThisBild.Money;
                ThisBild.timelocal = ThisBild.time;
            };
            
        }
    }
}
