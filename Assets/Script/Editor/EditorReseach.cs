using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

/*
[CustomEditor(typeof(ResheachView))]
public class EditorReseach : Editor
{
    //Массив строк, которые хотим видеть в выпадающем списке
    public string[] options = new string[] { "OpenBild", "Up", "OpenOfAbilities" };
    public int index = 0;

    private ResheachView Resheach;

    public void OnEnable()
    {
        Resheach = (ResheachView)target;
    }

    [MenuItem("Examples/Editor GUILayout Popup usage")]
    public override void OnInspectorGUI()
    {
        index = EditorGUILayout.Popup(index, options);
        Resheach.State = index;

        Resheach.IdResheach = EditorGUILayout.IntField("Какой IdScience исследования", Resheach.IdResheach);
        Resheach.IdBild = EditorGUILayout.IntField("Какой ID здания", Resheach.IdBild);

        if (GUI.changed) SetObjectDirty(Resheach.gameObject);
    }
    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}*/
