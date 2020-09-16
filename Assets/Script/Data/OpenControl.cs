using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OpenControl
{
    public static bool OpenConstraction = false, OpenReseache = false, OpenDeveloped = false, OpenScore = false;

    public static void Open(int a)
    {
        if (a == 1)
        {
            OpenConstraction = OpenConstraction ? false : true;
            OpenReseache = false;
            OpenDeveloped = false;
            OpenScore = false;
        }
        if (a == 2)
        {
            OpenConstraction = false;
            OpenReseache = OpenReseache ? false : true;
            OpenDeveloped = false;
            OpenScore = false;
        }
        if (a == 3)
        {
            OpenConstraction = false;
            OpenReseache = false;
            OpenDeveloped = OpenDeveloped ? false : true;
            OpenScore = false;
        }
        if (a == 4)
        {
            OpenConstraction = false;
            OpenReseache = false;
            OpenDeveloped = false;
            OpenScore = OpenScore ? false : true;
        }
    }

    public static bool GetOpen(int a)
    {
        bool outOpen = false;
        if (a == 1)
        {
            outOpen = OpenConstraction;
        }
        if (a == 2)
        {
            outOpen = OpenReseache;
        }
        if (a == 3)
        {
            outOpen = OpenDeveloped;
        }
        if (a == 4)
        {
            outOpen = OpenScore;
        }
        return outOpen;
    }
}
