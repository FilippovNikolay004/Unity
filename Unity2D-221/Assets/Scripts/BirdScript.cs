using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField] private float easyForceMultiplier = 250f;   // сила для простого режима
    [SerializeField] private float hardForceMultiplier = 350f;   // сила для сложного режима
    [SerializeField] private bool isHardMode = false;            // флаг режима игры

    private Rigidbody2D rb;
    private float health;
    private float forceMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        health = 100f; // Инициализируем здоровье
        forceMultiplier = isHardMode ? hardForceMultiplier : easyForceMultiplier; // Устанавливаем множитель силы в зависимости от режима
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector2.up * forceMultiplier); // Применяем силу вверх при нажатии пробела
        }

        transform.eulerAngles = new Vector3(0, 0, 3f * rb.linearVelocityY); // Поворачиваем объект в зависимости от скорости
    
        health -= Time.deltaTime; // Уменьшаем здоровье со временем
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Food")) {
            Destroy(other.gameObject); // Уничтожаем объект, если он с тегом "Food"
            health = Mathf.Min(health + 10f, 100f); // Увеличиваем здоровье
            // health = health >= 100f ? 100f : health += 10f; // Ограничиваем здоровье до 100
            Debug.Log("Health: " + health); // Выводим здоровье в консоль
        }
    }
}
