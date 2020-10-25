using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionPanel : MonoBehaviour
{
    int Begin, End;
    float X, Y;
    public float progress;
    public float step;
    private Vector2 Move = new Vector2(0, 1);
    bool StateOpen = false;

    Vector2 Open;
    Vector2 Closed;

    private bool ClouseFlag = true; // Это параметр впервый ли раз запускается игра

    // дублирующийся префаб
    public RectTransform prefab;
    public RectTransform content;
    public List<Image> BildOnScreen = new List<Image>();

    private List<GameObject> BildGameObjects = new List<GameObject>();

    private void OnApplicationPause() // Это нужно если игрок свернул игру
    {
        if (ClouseFlag)
        {
            ClouseFlag = false;
        }
        else ChancheActive();
    }

    void Start()
    {
        // Расчет размеров панели
        RectTransform Panel = GetComponent<RectTransform>(); ;
        X = Data.X;
        Y = Data.Y;
        RectTransformExtensions.SetTop(Panel,(Y / 2));

        print(X + " " + Y);

        Closed = new Vector2(0,-Y);
        Open = new Vector2(0, -Y / 2);
        transform.localPosition = Closed;

        // генерация блоков
        int i = 0;
        foreach (var image in BildOnScreen)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            BildView bildView = instance.GetComponent<BildView>();
            bildView.Id = i;
            bildView.BildMain = image;
            instance.transform.SetParent(content, false); // устанавлиеваем его дочерним
            instance.SetActive(true); // 
            BildGameObjects.Add(instance);
            i++;
        }
    }

    // размеры объекта в зависимости находящихся на нем объектов (купленных)
    public void ChancheActive()
    {
        int i = 0;
        foreach (var gameObject in BildGameObjects)
        {
            if (i <= Data.BildCount)
            {
                gameObject.SetActive(true);
            }
            else gameObject.SetActive(false);
            i++;
        }
        print("ChancheActive");
    }

    // Update is called once per frame
    void Update()
    {
        if (StateOpen)
        {
            if (transform.localPosition.x == Open.x && transform.localPosition.y == Open.y)
            {
                progress = 0;
            }
            else
            {
                transform.localPosition = Vector2.Lerp(Closed, Open, progress / 1000);
                progress += step;
            }            
        }
        if (!StateOpen)
        {
            if (transform.localPosition.x == Closed.x && transform.localPosition.y == Closed.y)
            {
                progress = 0;
            }
            else
            {
                transform.localPosition = Vector2.Lerp(Open, Closed, progress / 1000);
                progress += step;
            }
        }
    }

    public void ChancheStateOpen()
    {
        StateOpen = StateOpen ? false : true;
    }

    public void ClouseOpen()
    {
        StateOpen = false;
    }
}
