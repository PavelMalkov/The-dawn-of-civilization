using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryMen : MonoBehaviour
{
    private Animator anim;
    private FollowRoute follow;

    float speed;
    void Start()
    {
        anim = GetComponent<Animator>();
        follow = GetComponent<FollowRoute>();

        anim.SetBool("IsRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
//        if (follow.speed == 0) anim.SetBool("IsRunning", false);
//        else if (follow.speed > 0) anim.SetBool("IsRunning", true);
    }

    private void OnMouseDown()
    {
        SetStop();
        anim.SetTrigger("OnClick");
    }

    private void SetStop()
    {
        //anim.SetBool("IsRunning", false);
        if (GetComponent<FollowRoute>())
        {
            if (GetComponent<FollowRoute>().speed != 0) speed = GetComponent<FollowRoute>().speed;
            GetComponent<FollowRoute>().speed = 0;
        }
    }

    private void SetStart()
    {
        if (GetComponent<FollowRoute>())
        {
            GetComponent<FollowRoute>().speed = speed;
        }
        //anim.SetBool("IsRunning", true);
    }
}
