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

    public Scrollbar scrollbar;
    private bool scrollbarFlag = true;

    public VerticalLayoutGroup group;
    private bool groupFlag = true;

    // дублирующийся префаб
    public RectTransform prefab;
    public RectTransform content;
    public List<Image> BildOnScreen;

    private float Shir;

    void Start()
    {
        // Расчет размеров панели
        RectTransform Panel = GetComponent<RectTransform>(); ;
        X = Data.X;
        Y = Data.Y;
        RectTransformExtensions.SetTop(Panel,Y / 2);

        print(X + " " + Y);

        Closed = new Vector2(0,-Y);
        Open = new Vector2(0, -Y/2);
        transform.localPosition = Closed;

        // генерация блоков
        int i = 0;
        foreach (var image in BildOnScreen)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            //BildView bildView = GetComponent<BildView>().gameObject(instance);
            BildView bildView = instance.GetComponent<BildView>();
            bildView.Id = i;
            bildView.BildMain = image;
            instance.transform.SetParent(content, false);
            if (i > Data.BildCount) instance.SetActive(false);
            i++;
        }
        
        group.padding.bottom = Mathf.Max(0, (int)((Y / 2)  * 0.75 - (Y / 2) * 0.2 * (Data.BildCount + 1)));

        // обновление значение на промотке и выравнивания
        if (scrollbarFlag) { scrollbar.value = 1; scrollbarFlag = false; } // надо посоветоваться нужно ли всегда с начала включать
        else scrollbarFlag = true;
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

        group.padding.bottom = Mathf.Max(0, (int)((Y / 2) * 0.75 - (Y / 2) * 0.2 * (Data.BildCount + 1)));
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
