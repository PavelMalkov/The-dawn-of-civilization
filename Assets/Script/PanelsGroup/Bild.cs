using System;
using UnityEngine;

[Serializable]
public class Bild
{
    // обычные данные
    public int Id; // номер здания
    public string homeName; //название здания
    public float CostBay; // Стоимость покупки
    public Sprite bildimage; // его изображение
    public float coefficientUp; // коэффициент повышения уровня
    public float coefficientMoney; // коэффициент повышения уровня

    // сохраняемые данные
    public bool FactBay; // факт покупки

    public int countUp; // какой уровень

    public float CostUpLevel;
    public float MoneyLevel;

    public float CostUp; // стоимость повышения уровня 
    public float Money; // сколько денег здание приносит

    public float time; // сколько необходимо времени
    public float timelocal; // сколько времени прошло


    // Надо нормально сделать get set

    public void getbildList(bool _FactBay, int _countUp, float _CostUp, float _money, float _time, float _timelocal, string _homeName, float _CostBay, Sprite _bildimage,
        float _coefficientUp, float _coefficientMoney)
    {
        _FactBay = FactBay; _countUp = countUp; _CostUp = CostUp; _money = Money; _time = time; _timelocal = timelocal; _homeName = homeName; _CostBay = CostBay; _bildimage = bildimage;
        _coefficientUp = coefficientUp; _coefficientMoney = coefficientMoney;
    }

    public void getbildList(int _Id, bool _FactBay, int _countUp, float _CostUp, float _money, float _time, float _timelocal, string _homeName, float _CostBay, Sprite _bildimage,
    float _coefficientUp, float _coefficientMoney)
    {
        _Id = Id;
        _FactBay = FactBay; _countUp = countUp; _CostUp = CostUp; _money = Money; _time = time; _timelocal = timelocal; _homeName = homeName; _CostBay = CostBay;
        _bildimage = bildimage; _coefficientUp = coefficientUp; _coefficientMoney = coefficientMoney;
    }

    public void setbildList(Bild bildList)
    {
        bildList.Id = Id;
        bildList.FactBay = FactBay; bildList.countUp = countUp; bildList.CostUp = CostUp; bildList.Money = Money; bildList.time = time; bildList.timelocal = timelocal;
        bildList.homeName = homeName; bildList.CostBay = CostBay; bildList.bildimage = bildimage; bildList.coefficientUp = coefficientUp; bildList.coefficientMoney = coefficientMoney;
    }
}
