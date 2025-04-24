using UnityEngine;
using UnityEngine.InputSystem;

public class ControlScript : MonoBehaviour
{
    private Rigidbody2D rb; // Ссылка на компонент
    private InputAction moveAction; // Ссылка на действие перемещения

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody
        //moveAction = InputSystem.actions.FindAction("Move"); // Ищем действие "Move" в Input System
    }

    void Update()
    {
        // Управление - анализ реакции игрока
        // 1. Input - прямой доступ к устройствам (клавиатура, )
        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            // 2. Physics - физика
            rb.AddForce(Vector2.up * 100f); // Применяем силу к Rigidbody
        }

        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector2.up * 5f); // Применяем силу вверх
        }
        */


        // 2. Input Manager - абстракция над устройствами
        float y = Input.GetAxis("Vertical"); // Получаем значение оси "Vertical"
        rb.AddForce(5f * y * Vector2.up);
        float x = Input.GetAxis("Horizontal"); // Получаем значение оси "Horizontal"
        rb.AddForce(5f * x * Vector2.right);


        // 3. Input System - новая система ввода
        //rb.AddForce(5f * moveAction.ReadValue<Vector2>());
    }
}
