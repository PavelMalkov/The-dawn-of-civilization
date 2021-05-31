using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAnimControl : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    public void SetAnimBildOrUp()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("BildAndUp");
    }
}
