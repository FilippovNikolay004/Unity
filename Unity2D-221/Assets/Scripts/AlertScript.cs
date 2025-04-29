using UnityEngine;

public class AlertScript :MonoBehaviour {
    private static TMPro.TextMeshProUGUI title;
    private static TMPro.TextMeshProUGUI message;
    private static TMPro.TextMeshProUGUI actionButtonText;

    private static GameObject content;


    public static void Show(string title, string message, string actionButtonText = "Close") {
        AlertScript.title.text = title;
        AlertScript.message.text = message;
        AlertScript.actionButtonText.text = actionButtonText;

        content.SetActive(true); // Показываем контент
        Time.timeScale = 0.0f; // Останавливаем игру
    }

    void Start() {
        Transform c = transform.Find("Content");
        title = c.Find("Title").GetComponent<TMPro.TextMeshProUGUI>();
        message = c.Find("Message").GetComponent<TMPro.TextMeshProUGUI>();
        actionButtonText = c.Find("Button/Text").GetComponent<TMPro.TextMeshProUGUI>();

        content = c.gameObject;
        content.SetActive(false); // Скрываем контент при старте
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnActionButtonClick(); // Закрываем окно при нажатии Escape
        }
    }

    public void OnActionButtonClick() {
        content.SetActive(false); // Скрываем контент
        Time.timeScale = 1.0f; // Возобновляем игру
    }
}
