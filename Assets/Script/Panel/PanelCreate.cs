using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCreate : MonoBehaviour
{
    [SerializeField]
    List<GameObject> BildOnScreen = new List<GameObject>();

    private void Start()
    {
        foreach (var gameObject in BildOnScreen)
        {
            gameObject.SetActive(true);
        }
    }

    // размеры объекта в зависимости находящихся на нем объектов (купленных)
    public void ChancheActive()
    {
        //int i = 0;
        if (this.name == "Constraction")
        {
            foreach (var gameObject in BildOnScreen)
            {
                gameObject.GetComponent<BildView>().UpdateData();
            }
            foreach (var gameObject in BildOnScreen)
            {
                if (gameObject.GetComponent<BildView>().GetCanBay()) gameObject.SetActive(true);
                else gameObject.SetActive(false);
            }
        }
        else if (this.name == "Boost")
        {
            foreach (var gameObject in BildOnScreen)
            {
                gameObject.GetComponent<BoostView>().UpdateData();
            }
            foreach (var gameObject in BildOnScreen)
            {
                if (gameObject.GetComponent<BoostView>().Visible()) gameObject.SetActive(true);
                else gameObject.SetActive(false);
            }
        }
        print("ChancheActive");
    }
}
