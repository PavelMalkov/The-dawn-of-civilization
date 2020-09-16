using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBild : MonoBehaviour
{
    public float OneClickCost;

    public void OnMainBildClick()
    {
        Data.count += OneClickCost;
    }
}
