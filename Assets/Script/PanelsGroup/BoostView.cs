using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BoostView : MonoBehaviour
{
    // Это объекты которые изменяются и привязываются в самом префабе поэтому они скрыты
    [SerializeField]
    private Image BildImage;
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private TextMeshProUGUI About;
    [SerializeField]
    private TextMeshProUGUI txtCost;
    [SerializeField]
    private GameObject Bay;

    // вот эти данные должны храниться
    public int Id; // номер здания
    public int IdBoost; // номер ускорения
    public bool CanYouBay;

    // эти данные находятся
    float CostBay;

    Bild Bild; // Здание
    Boost Boost;

    bool parametrAllBild = false; // Все здания

    [SerializeField]
    List<GameObject> gameObjects;

    public bool Visible()
    {
        return (Boost.CanYouBay == true && Boost.FactBayBonus == false);
    }


    // обновление данных
    public void UpdateData()
    {
        if (Id >= 0) Bild = SaveControl.GetBildSetting(Id); // получение данных о здании
        else parametrAllBild = true;
        Boost = SaveControl.GetBoostSetting(IdBoost); // получение данных о ускорении
        //SaveControl.SetBoostSetting(IdBoost, CanYouBay); // устанавливаем значение которое хранит здание
    }

    void Start()
    {
        RectTransform a = this.GetComponent<RectTransform>();
        float SizeX = Currency.X * 0.98f;
        float SizeY = SizeX / 4.5f;
        a.sizeDelta = new Vector2(SizeX, SizeY); // задаем размер наших блоков зданий

        UpdateData();

        BildImage.sprite = Boost.BoostImage;
        Name.text = Boost.NameBoost; // текст
        About.text = Boost.AboutBoost; // текст об ускорении

        if (Boost.FactBayBonus) // возможны сдесь ошибки
        {
            if (gameObjects != null) // объекты появляемые на сцене
                foreach (var item in gameObjects)
                {
                    item.SetActive(true);
                }
            //  gameObject.SetActive(false);            
        }
        else
        {
            if (gameObjects != null)
                foreach (var item in gameObjects)
                {
                    item.SetActive(false);
                }
            gameObject.SetActive(true);
            CostBay = Boost.CostBay;
            txtCost.text = "" + TextControl.ConvertTxt(CostBay);
        }
        MyProperty = 0;
        Bay.GetComponent<ButtonControl>().SetButton();
    }

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
        if (Currency.Gold >= CostBay)
        {
            if (myPropVar != 0) MyProperty = 0;
        } else if (myPropVar != 2) MyProperty = 2;

    }

    private void Update()
    {
        check();
    }

    // добавить защиту от повторного нажатия +
    public void ClickBay()
    {
        // необходимо переделать ускорение и бонусы.
        if (Currency.Gold >= CostBay && Boost.FactBayBonus == false)
        {
            Currency.Gold -= CostBay;
            txtCost.text = "+";
            //Ускорение
            //само ускорение
            if (Bild != null)
            {
                Bild.time /= Boost.CoefBoost;
                Bild.timelocal /= Boost.CoefBoost;
                Bild.Money *= Boost.CoefBoost;
            }
            else Debug.Log("Up All bild");

            if (gameObjects != null)
                foreach (var item in gameObjects)
                {
                    item.SetActive(true);
                }

            Boost.FactBayBonus = true;
            gameObject.SetActive(false);
        }
    }
}
