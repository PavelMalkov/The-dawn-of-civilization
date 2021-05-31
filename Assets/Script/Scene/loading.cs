using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scene-Loading/Loading")]
public class loading : MonoBehaviour
{
    [Header("Загружаемая сцена")]
    public string sceneName;
    [Header("Остальные объекты")]
    public Image LoadingImg;
    //public Text progressText;

    private void Start()
    {
        StartCoroutine(Asyncload());
    }

    IEnumerator Asyncload()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            LoadingImg.fillAmount = progress;
            yield return null;
        }
        
    }
}
