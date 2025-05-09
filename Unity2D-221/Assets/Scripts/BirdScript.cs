using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdScript : MonoBehaviour
{
    public static float extraLifeChance = 0.2f;

    [SerializeField] private float easyForceMultiplier = 250f;   // сила для простого режима
    [SerializeField] private float hardForceMultiplier = 350f;   // сила для сложного режима
    [SerializeField] private bool isHardMode = false;            // флаг режима игры
    [SerializeField] private TMPro.TextMeshProUGUI triesTmp;

    private Rigidbody2D rb;
    public static float health;
    private float healthTimeout = 20.0f;                        // время, через которое здоровье будет уменьшаться
    private float forceMultiplier;

    private int tries;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        health = 1.0f; // Инициализируем здоровье
        forceMultiplier = isHardMode ? hardForceMultiplier : easyForceMultiplier; // Устанавливаем множитель силы в зависимости от режима
        tries = 3; // Инициализируем количество попыток
        triesTmp.text = tries.ToString(); // Устанавливаем текстовое значение количества попыток
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector2.up * forceMultiplier * Time.timeScale); // Применяем силу вверх при нажатии пробела
        }

        transform.eulerAngles = new Vector3(0, 0, 3f * rb.linearVelocityY); // Поворачиваем объект в зависимости от скорости
    
        health -= Time.deltaTime / healthTimeout; // Уменьшаем здоровье со временем
        if (health <= 0) {
            Loose(); // Вызываем метод проигрыша, если здоровье меньше или равно 0
        }
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
            Loose();
        }
    }

    private void Loose() {
        // Шанс дополнительной жизни
        if (Random.value < extraLifeChance) {
            health = 1.0f;
            AlertScript.Show("Second Chance", "You got an extra life!", "Continue", () => DestroyerScript.ClearField());
            Time.timeScale = 0;
            return;
        }

        tries--; // Уменьшаем количество попыток
        triesTmp.text = tries.ToString(); // Устанавливаем текстовое значение количества попыток

        if (tries > 0) {
            health = 1.0f; // Инициализируем здоровье
            AlertScript.Show("Warning", "You have " + tries + " tries left!", "Continue", () => DestroyerScript.ClearField()); // Показываем предупреждение
        } else {
            AlertScript.Show("Game Over", "You hit a pipe!", "Restart", () => SceneManager.LoadScene(0)); // Показываем сообщение об окончании игры
        }

        Time.timeScale = 0; // Останавливаем игру
    }
}
