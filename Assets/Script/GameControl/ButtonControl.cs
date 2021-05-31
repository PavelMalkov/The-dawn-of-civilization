using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ButtonControl : MonoBehaviour
{
    public bool State = false; // состояния 

    // false - нехватает купить денег
    // true - хватает купить денег

    public Sprite ButtonReadyPressed;
    public Sprite ButtonPressed;
    public Sprite ButtonReadyPressedMax;
    public Sprite ButtonPressedMax;
    public Sprite CanNotPressed;

    public GameObject ClickOn;
    public GameObject ClickOff;

    // буферные спрайты
    Sprite spriteReadyPress;
    Sprite SpritePress;

    int localparam = -1;


    SpriteState spriteState;

    public TextMeshProUGUI text;

    private void Start()
    {
        /*spriteState = new SpriteState();
        spriteState = this.GetComponent<Button>().spriteState;
        spriteState.pressedSprite = ButtonPressedMax; */      
    }

    /// <summary>
    /// Установление параметров спрайтов на включение и выключение кнопок
    /// </summary>
    /// <param name="param"> Если параметр == 0, то изображение кнопки разовой покупки, если параметр == 1, то изображение максимальной покупки, если параметр == 2 то кнопка неактивная </param>
    public void SetData(int param)
    {
        if (param == 0)
        {
            spriteReadyPress = ButtonReadyPressed;
            SpritePress = ButtonPressed;
        }
        if (param == 1)
        {
            spriteReadyPress = ButtonReadyPressedMax;
            SpritePress = ButtonPressedMax;
        }
        if (param == 2)
        {
            spriteReadyPress = CanNotPressed;
            SpritePress = CanNotPressed;
        }
        //
        if (param != localparam)
        {
            localparam = param;
            SetButton();
        }
    }

    private void OnImaClick()
    {
        if (ClickOn) ClickOn.SetActive(true);
        if (ClickOff) ClickOff.SetActive(false);
    }

    private void OffImaClick()
    {
        if (ClickOn) ClickOff.SetActive(true);
        if (ClickOff) ClickOn.SetActive(false);
    }

    public void SetButton() // включаем кнопку покупки
    {
        this.GetComponent<Image>().sprite = spriteReadyPress;
        if (localparam != 2)
        {
            if (text) text.alignment = TextAlignmentOptions.Center;
            OnImaClick();
        } 
        else
        {
            OffImaClick();
            if (text) text.alignment = TextAlignmentOptions.Bottom;
        }
    }

    public void PressButton() // нажата кнопка покупки
    {
        OffImaClick();
        this.GetComponent<Image>().sprite = SpritePress;
        if (text) text.alignment = TextAlignmentOptions.Bottom;
    }
}
