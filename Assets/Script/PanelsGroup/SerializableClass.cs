using System;
using UnityEngine;

[Serializable]
public class Bild
{
    // обычные данные
    public int Id; // номер здания
    public int IdBoost; // номер ускорения
    public string homeName; //название здания
    public float CostBay; // Стоимость покупки
    public Sprite bildimage; // его изображение
    public float coefficientUp; // коэффициент повышения уровня
    public float coefficientMoney; // коэффициент повышения уровня

    // сохраняемые данные
    public bool FactBay; // факт покупки
    //Возможность покупки
    public bool YouCanBay; // Это из научного исследования вытекает
    public bool Open; // Это для редакторв

    public int NumCurrent = 0; // номер валюты 0 - золото, 1 - наука

    public int countUp; // какой уровень

    public float CostUpLevel;
    public float MoneyLevel;

    public float CostUp; // стоимость повышения уровня 
    public float Money; // сколько денег здание приносит

    public float time; // сколько необходимо времени
    public float timelocal; // сколько времени прошло

    public Bild() {}

    // Надо нормально сделать get set

    public void getbildList(bool _FactBay, int _NumCurrent, int _countUp, float _CostUp, float _money, float _time, float _timelocal, string _homeName, float _CostBay, Sprite _bildimage,
        float _coefficientUp, float _coefficientMoney, bool[] _FactBayBonus)
    {
        _FactBay = FactBay; _NumCurrent = NumCurrent;  _countUp = countUp; _CostUp = CostUp; _money = Money; _time = time; _timelocal = timelocal; _homeName = homeName; _CostBay = CostBay;
        _bildimage = bildimage;  _coefficientUp = coefficientUp; _coefficientMoney = coefficientMoney;
    }

    public void getbildList(int _Id, bool _FactBay, int _NumCurrent, int _countUp, float _CostUp, float _money, float _time, float _timelocal, string _homeName, float _CostBay, Sprite _bildimage,
    float _coefficientUp, float _coefficientMoney, bool[] _FactBayBonus)
    {
        _Id = Id;
        _FactBay = FactBay; _NumCurrent = NumCurrent; _countUp = countUp; _CostUp = CostUp; _money = Money; _time = time; _timelocal = timelocal; _homeName = homeName; _CostBay = CostBay;
        _bildimage = bildimage; _coefficientUp = coefficientUp; _coefficientMoney = coefficientMoney;
    }

    public void setbildList(Bild bildList)
    {
        bildList.Id = Id;
        bildList.FactBay = FactBay; bildList.NumCurrent = NumCurrent; bildList.countUp = countUp; bildList.CostUp = CostUp; bildList.Money = Money; bildList.time = time; bildList.timelocal = timelocal;
        bildList.homeName = homeName; bildList.CostBay = CostBay; bildList.bildimage = bildimage; bildList.coefficientUp = coefficientUp; bildList.coefficientMoney = coefficientMoney;
    }
}

[Serializable]
public class Boost
{
    // обычные данные
    public int Id; // номер повышаемого количесвто денег здания
    public int IdBoost; // номер ускорения
    public string NameBoost; //название здания
    public bool CanYouBay; // Возможность купить
    public string AboutBoost; //название ускорения

    public Sprite BoostImage; // его изображение

    public float CostBay; // Стоимость повышений
    public float CoefBoost; // Коэфициент повышения
    public bool FactBayBonus; // факт покупки

    public bool Open; // Это для редакторв

    public Boost() { }

    public Boost(int IdBoost, string NameBoost, string AboutBoost, bool CanYouBay)
    {
        this.IdBoost = IdBoost;
        this.NameBoost = NameBoost;
        this.AboutBoost = AboutBoost;
        this.CanYouBay = CanYouBay;
    }
}

[Serializable]
public class Resheach
{
    // обычные данные
    public int Id; // номер повышаемого здания
    public int IdReseach; // номер повышаемого здания
    public int IdBoosts; // номер повышаемого здания
    public string NameResheach; //название здания
    //public string AboutResheach; //название ускорения
    
    public Sprite ResheachImage; // его изображение

    public Sprite Open1; // его изображение
    public Sprite Open2; // его изображение
    public Sprite Open3; // его изображение

    public float CostBay; // стоимость
    public bool Open; // Это для редакторв
    public bool BayNow; // Факт покупки исследования

    public float bonus = 1.2f; // начальный бонус
    public float bonusdiv = 0.2f; // шаг получаемого бонуса

    public int State;

    public float time; // сколько необходимо времени
    public float timelocal; // сколько времени прошло

    public Resheach() { }

    public Resheach(int id, string NameResheach)
    {
        Id = id;
        this.NameResheach = NameResheach;
    }
}