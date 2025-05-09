﻿using UnityEngine;

public class SpawnerScript :MonoBehaviour {
    public static float _difficulty = 0.5f; // Сложность игры (по умолчанию 0.5)
    public static float difficulty {
        get => _difficulty;
        set { 
            _difficulty = value; 
            foodTimeout = timeout + period * 1.5f; // Обновляем таймаут еды при изменении сложности
        }
    }

    public static float _lifeDropChance = 0.2f; // Шанс появления жизни
    public static float lifeDropChance {
        get => _lifeDropChance;
        set => _lifeDropChance = Mathf.Clamp01(value); // Ограничим от 0 до 1
    }

    [SerializeField] private GameObject pipePrefab;
    private const float pipeOffSetMax = 2.5f;

    [SerializeField] private GameObject[] foodPrefabs; // Массив префабов еды
    private const float foodOffSetMax = 4.5f;


    public static float _foodDropChance = 0.4f; // Шанс выпадения еды
    public static float foodDropChance {
        get => _foodDropChance;
        set {
            _foodDropChance = value;
            foodTimeout = timeout + period * 1.5f;
        }
    }


    private static float period => 2.0f - 0.9f * difficulty;
    private static float timeout;
    private static float foodTimeout;

    public static int[] foodCount = new int[2]; // Счетчик еды 

    void Start() {
        timeout = 0;
        foodTimeout = period * 1.5f;
    }

    void Update() {
        timeout -= Time.deltaTime;
        if (timeout < 0) {
            timeout = period;
            SpawnPipe();
        }

        foodTimeout -= Time.deltaTime;
        if (foodTimeout < 0) {
            foodTimeout = period;
            SpawnFood();
        }
    }

    private void SpawnPipe() {
        GameObject pipe = Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position +
            Random.Range(-pipeOffSetMax, pipeOffSetMax) * Vector3.up;
    }
    private void SpawnFood() {
        if (Random.value < foodDropChance) {
            return; // Шанс не спавнить еду
        }

        int index = Random.Range(0, foodPrefabs.Length);
        foodCount[index]++; // Увеличиваем счетчик еды
        CounterScript.UpdateCounter();

        GameObject food = Instantiate(foodPrefabs[index]);
        food.transform.position = this.transform.position +
            Random.Range(-foodOffSetMax, foodOffSetMax) * Vector3.up;
        food.transform.Rotate(0, 0, Random.Range(0, 360));
    }
}
