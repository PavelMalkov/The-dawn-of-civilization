using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageAnimation : MonoBehaviour
{
    Transform transform; // позиция
    [SerializeField]
    TextMeshPro textMesh;
    [SerializeField]
    GameObject currency;

    [SerializeField]
    Sprite[] CurrencysSprite = new Sprite[4];

    Vector2 position; // позиция где появляется объект

    public float sum; // текст

    public int NumCurrency = 0;// номер валюты

    public float positionDelt = 1;
    public int tipe;

    private Vector2 Move = new Vector2(0, 1);
    public float speed;
    private float div = 1000;
    private bool AndClick = true; 

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = "+ " + sum.ToString(); // устанавливаем текст
        currency.GetComponent<SpriteRenderer>().sprite = CurrencysSprite[NumCurrency]; // устанавливаем иконку
        position = GetComponent<Transform>().position;
        transform = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        Click();

        anim.SetBool("OnStart", true);
    }

    public void Click()
    {
        textMesh.text = "+ " + sum.ToString(); // устанавливаем текст
        AndClick = false;
    }

    // анимация установление статуса клик Вкл
    private void ChancheBool()
    {
        AndClick = true;
    }

    // удаление объекта
    private void Delete()
    {
        Destroy(gameObject);
    }

    private void Sum() //сложение
    {
        if (tipe == 0) Currency.Gold += sum;
        if (tipe == 1) Currency.Science += sum;
    }

    // Корутина для движения объекта
    IEnumerator Cor()
    {
        if(AndClick)
        {
            anim.SetBool("OnStart", false);
            while (true)
            {
                transform.Translate(Move.normalized * speed / div);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
