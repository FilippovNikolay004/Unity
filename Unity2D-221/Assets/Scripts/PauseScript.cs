using System;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private GameObject content;

    void Start()
    {
        Transform c = transform.Find("Content");
        content = c.gameObject;

        if (content.activeSelf) {
            Time.timeScale = 0.0f; // Возобновляем игру
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            TogglePause(); // Переключаем паузу при нажатии Escape
        }
    }

    private void TogglePause() {
        if (content.activeSelf) {
            content.SetActive(false); // Скрываем контент
            Time.timeScale = 1.0f; // Возобновляем игру
        } else {
            content.SetActive(true); // Показываем контент
            Time.timeScale = 0.0f; // Останавливаем игру
        }
    }

    public void OnButtonClick() {
        content.SetActive(false); // Скрываем контент
        Time.timeScale = 1.0f; // Возобновляем игру
    }


    public void OnIntervalValueChanged(Single value) {
        SpawnerScript.difficulty = value; // Устанавливаем сложность игры
        Debug.Log($"Chance hard value: {value}");
    }
    public void OnIntervalValueChancesLife(Single value) {
        SpawnerScript.foodDropChance = value;
        Debug.Log($"Chance life value: {value}"); // Устанавливаем шанс доп. жизни
    }
    public void OnIntervalValueChanceFood(Single value) {
        BirdScript.extraLifeChance = value;
        Debug.Log($"Chance food value: {value}"); // Устанавливаем шанс спавна еды
    }
}
