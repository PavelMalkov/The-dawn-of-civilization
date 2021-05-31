using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSunMoon : MonoBehaviour
{
    string NameGameObject;

    //зададим параметры элипса
    public float Radios;
    //Коэфициенты a и b
    public float a;
    public float b;

    public float TimeBegin; 
    public float TimeEnd;

    float time = 13;

    float hTime; // шаг по времени


    // Start is called before the first frame update
    void Start()
    {
        //установление начальных координат
        //transform.position = new Vector2(0, 2);
        // получение название объекта
        NameGameObject = this.name;
        // Расчет необходимых значения шага времени
        if (NameGameObject == "Sun") hTime = Mathf.Abs((TimeEnd - TimeBegin) / (2 * Mathf.Sqrt(Radios)));
        else if (NameGameObject == "Moon") hTime = Mathf.Abs((24 - TimeEnd + TimeBegin) / (2 * Mathf.Sqrt(Radios)));

        //старт отслеживания времени и смены положения
        //StartCoroutine(MoveTime());

        if (NameGameObject == "Sun") SetPositionSun();
        else if (NameGameObject == "Moon") SetPositionMoon();
        else if (NameGameObject == "Night") SetNight();
    }

    void SetPositionSun()
    {
        float x, y;
        if (time <= TimeBegin || time >= TimeEnd) { transform.position = new Vector2(0, 2); }
        else
        {
            x = ((time - TimeBegin) / hTime) - Mathf.Sqrt(Radios);
            if ((Radios - a * x * x) == 0) y = 0;
            else y = (Mathf.Sqrt((Radios - a * x * x) / b));

            transform.position = new Vector2(x, y);
        }
    }

    void SetPositionMoon()
    {
        float x, y;
        if (time > TimeBegin && time < TimeEnd) { transform.position = new Vector2(0, 2); }
        else
        {
            if (time < TimeEnd) x = ((24 - TimeEnd + time) / hTime) - Mathf.Sqrt(Radios);
            else x = ((time - TimeEnd) / hTime) - Mathf.Sqrt(Radios);
            y = (Mathf.Sqrt((Radios - a * x * x) / b));

            transform.position = new Vector2(x, y);
        }
    }

    void SetNight()
    {
        float Dawn = 6; //Рассвет
        float MeridianSun = 13; //Зенит
        float Sunset = 19; //Закат

        float LocalTimeAlfa = 0;

        // график альфа канала
        // солнце поднимается
        if (time >= Dawn && time < MeridianSun)          { LocalTimeAlfa = 0.4f + (time - Dawn) * ((0.6f) / Mathf.Abs(Dawn - MeridianSun)) ; }
        // солнце опускается
        else if (time >= MeridianSun && time < Sunset)   { LocalTimeAlfa = 1f - (time - MeridianSun) * ((0.7f) / Mathf.Abs(MeridianSun - Sunset)); }
        // солца нет
        else if (time >= Sunset && time <= 24)           { LocalTimeAlfa = 0.3f + (time - Sunset) * ((0.1f)/(Mathf.Abs(Sunset - 24))); }
        // солнца нет
        else if (time >= 0 && time < Dawn)               { LocalTimeAlfa = 0.4f - (time) * (0.1f / Dawn); }


        Color color = GetComponent<SpriteRenderer>().color;
        color.a = Mathf.Clamp(1 - LocalTimeAlfa, 0, 1);
        GetComponent<SpriteRenderer>().color = color;
    }

    IEnumerator MoveTime()
    {
        while (true)
        {
            // взависимости какой у нас объект мы запускаем функцию ему соответствующему
            if (NameGameObject == "Sun") SetPositionSun();
            else if (NameGameObject == "Moon") SetPositionMoon();
            else if (NameGameObject == "Night") SetNight();
            time += 0.002f;
            time = time >= 24 ? 0 : time;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
