using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField] private float easyForceMultiplier = 250f;   // сила для простого режима
    [SerializeField] private float hardForceMultiplier = 350f;   // сила для сложного режима
    [SerializeField] private bool isHardMode = false;            // флаг режима игры

    private Rigidbody2D rb;
    public static float health;
    private float healthTimeout = 20.0f;                       // время, через которое здоровье будет уменьшаться
    private float forceMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        health = 1.0f; // Инициализируем здоровье
        forceMultiplier = isHardMode ? hardForceMultiplier : easyForceMultiplier; // Устанавливаем множитель силы в зависимости от режима
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector2.up * forceMultiplier); // Применяем силу вверх при нажатии пробела
        }

        transform.eulerAngles = new Vector3(0, 0, 3f * rb.linearVelocityY); // Поворачиваем объект в зависимости от скорости
    
        health -= Time.deltaTime / healthTimeout; // Уменьшаем здоровье со временем
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Food")) {
            FoodScript foodScript = other.GetComponent<FoodScript>(); // Получаем компонент FoodScript
            if (foodScript != null) {
                health = Mathf.Clamp01(health + foodScript.healthBonus / healthTimeout); // Увеличиваем здоровье на значение из FoodScript
            }

            Destroy(other.gameObject); // Уничтожаем объект, если он с тегом "Food"
            Debug.Log("Health: " + health); // Выводим здоровье в консоль
        }

        if (other.CompareTag("Pipe")) {
            AlertScript.Show("Game Over", "You hit a pipe!"); // Показываем сообщение об окончании игры
            Time.timeScale = 0; // Останавливаем игру
        }
    }
}
