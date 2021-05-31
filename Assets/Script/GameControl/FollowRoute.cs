using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{

    // скорость следования
    [SerializeField]
    float directionMove = 0.5f;
    // скорость
    [SerializeField]
    public float speed = 0.5f;

    [SerializeField]
    private GameObject[] routes;

    [SerializeField]
    private float Z; //  отклонения от основного холма
    [SerializeField]
    private int routeToGo = 0; // номер откуда начинается движение
    private float tParam;
    private Vector3 Position;
    int rand;

    RoutsParametr Way = new RoutsParametr(); // вызываем конструктор для создание связей пути
    List<int> PossibleWays; // возможные пути (будут обновлятся)

    private bool coroutineAllowed;

    private void Start()
    {        
        coroutineAllowed = true;
        //tParam = 0f; // положение относительно начала

        SetRoadAndSpeedMod(routeToGo);

        foreach (var item in PossibleWays)
        {
            Debug.Log("Пары возможного ходаи его направление:  " + item.ToString()); // 
        }

    }

    private void SetRoadAndSpeedMod(int position)
    {
        Way.Position = position; // номер начального маршрута
        PossibleWays = Way.GetResearch(); // определение всевозможных маршрутов
        rand = PossibleWays[Random.Range(0, PossibleWays.Count)]; // Случайное число
        directionMove = rand / Mathf.Abs(rand); // его скорость (направление)
        routeToGo = Mathf.Abs(rand); // определения номера машрута
        tParam = directionMove > 0f ? 0f : 1f;
    }

    private void Update()
    {
        if (coroutineAllowed) StartCoroutine(GoByTheRoute(routeToGo - 1));
        //StartCoroutine(GoByTheRoute(routeToGo));
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].transform.GetChild(0).position;
        Vector3 p1 = routes[routeNumber].transform.GetChild(1).position;
        Vector3 p2 = routes[routeNumber].transform.GetChild(2).position;
        Vector3 p3 = routes[routeNumber].transform.GetChild(3).position;

        while (tParam <= 1 && tParam >= 0) // пока не окажемся на концах кривой
        {
            tParam += Time.deltaTime * directionMove * speed;
            //Mathf.Clamp(tParam, 0 , 1);
            Position = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            Position.z = Z + Position.z; 

            transform.position = Position;
            yield return new WaitForSeconds(0.02f);
        }

        SetRoadAndSpeedMod(Way.GetPosition(rand));

        coroutineAllowed = true;
    }
}
public class RoutsParametr
{
    int pos; // позиция на какой вершине находится игрок

    int[] first  = new int[19] { 0, 1, 1, 3, 4, 4, 4, 7, 8, 8, 8, 8, 13, 14, 14, 11, 12, 12, 12 };
    int[] second = new int[19] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,13,14, 15, 16, 12, 18, 19, 17 };

    public int Position
    {
        get
        {
            return pos;
        }

        set
        {
            pos = value;
        }
    }

    public RoutsParametr() // второй номер это номер следуемого пути + 1
    {
        pos = 0;
    }

    // получение номера вершины графа где оказался 
    public int GetPosition(int direction)
    {
        if (direction < 0)
            for (int i = 0; i < 19; i++)
            {
                if (second[i] == Mathf.Abs(direction)) return first[i];
            }
        else return direction;

        return 0;
    }

    // Функция расчета какими путями сожно пойти
    public List<int> GetResearch()
    {
        List<int> buf = new List<int>(); // 1 значение куда напрявляется 2 значение значение + или - по какому направлению движутся

        for (int i = 0; i < 19; i++)
        {
            if (second[i] == pos) buf.Add((second[i]) * (-1)); // если значение равно значению конечной точке отправления
            else if (first[i] == pos) buf.Add(second[i]); // если значение равно значению начальной точке отправления
        }
        
        return buf; // список номеров в точки которые можем попасть и направление
    }
}