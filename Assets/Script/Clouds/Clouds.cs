using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    //Массив облаков
    [Tooltip("Это генерируеммые облака")]
    public GameObject[] Cloud;
    // Задаваемые значения области где генерируются облака
    public float RandomYTop, RandomYDown, RandomXLeft, RandomXRight;
    // Колич
    public int CountCloudsMin, CountCloudsMax;

    private Vector2 SpawnCloudsPosition = new Vector2();


    [HideInInspector]
    public int CountClouds;

    // Запуск генерации облаков
    void Start()
    {
        GenerateClouds();
        StartCoroutine(SpawnCloudOne());
    }

    void GenerateClouds()
    {
        int GenerateNumber;
        float step = 0.8f, delta = 1.8f;
        float Start = RandomXLeft + delta;

        CountClouds = Random.Range(CountCloudsMin, CountCloudsMax);
        for (int i = 0; i < CountClouds; ++i)
        {
            GenerateNumber = Random.Range(0, Cloud.Length);
            SpawnClouds(GenerateNumber, Start);
            Start += step;
        }
    }

    // Генератор одного облака по указанной координате X
    void SpawnClouds(int Number, float X)
    {
        // Положение объекта
        SpawnCloudsPosition = new Vector2(X, Random.Range(RandomYTop, RandomYDown));
        // Создание объекта
        Instantiate(Cloud[Number], SpawnCloudsPosition, Quaternion.identity);

    }

    void SpawnClouds(int Number)
    {
        // Положение объекта
        SpawnCloudsPosition = new Vector2(Random.Range(RandomXLeft, RandomXRight), Random.Range(RandomYTop, RandomYDown));
        // Создание объекта
        Instantiate(Cloud[Number], SpawnCloudsPosition, Quaternion.identity);

    }

    // Генератор одного облака
    void SpawnClouds()
    {
        // Положение объекта
        SpawnCloudsPosition = new Vector2(RandomXLeft, Random.Range(RandomYTop, RandomYDown));
        // Создание объекта
        Instantiate(Cloud[Random.Range(0, Cloud.Length)], SpawnCloudsPosition, Quaternion.identity);
    }
    
    IEnumerator SpawnCloudOne()
    {
        while (true)
        {
            SpawnClouds();
            yield return new WaitForSeconds(Random.Range(20, 35));
        }
    }

}
