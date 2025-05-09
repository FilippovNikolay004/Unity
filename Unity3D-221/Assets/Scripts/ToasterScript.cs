using System.Collections.Generic;
using UnityEditor.Search;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.InputSystem.InputRemoting;

public class ToasterScript : MonoBehaviour
{
    public static ToasterScript instance;

    private GameObject content;
    private TMPro.TextMeshProUGUI text;
    private CanvasGroup contentGroup;

    private float timeout;
    private float showtime = 3.0f;
    private Queue<ToastMessage> messageQueue = new Queue<ToastMessage>();

    void Start()
    {
        instance = this;

        Transform t = this.transform.Find("Content");
        content = t.gameObject;
        contentGroup = content.GetComponent<CanvasGroup>();
        text = t.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();

        content.SetActive(false);
        timeout = 0.0f;

        GameState.AddListener(OnGameStateChanged);
    }

    
    void Update()
    {
        if (timeout > 0) {
            timeout -= Time.deltaTime;
            contentGroup.alpha = Mathf.Clamp01(timeout * 2.0f);

            if (timeout < 0) {
                content.SetActive(false);
            }
        } else if (messageQueue.Count > 0) {
            var toastMessage = messageQueue.Dequeue();
            content.SetActive(true);
            text.text = toastMessage.message;
            timeout = toastMessage.time == 0.0f ? instance.showtime : toastMessage.time;
        }
    }

    private void OnGameStateChanged(string fieldName) {
        if (fieldName == nameof(GameState.isKey1Collected)) {
            Toast("One key has been found. You can open the grey doors.");
        }
    }
    private void OnDestroy() {
        GameState.RemoveListener(OnGameStateChanged);
    }

public static void Toast(string message, float time = 0.0f) {
        //instance.content.SetActive(true);
        //instance.text.text = message;
        //instance.timeout = time == 0.0f ? instance.showtime : time;

        instance.messageQueue.Enqueue(new ToastMessage { 
            message = message, 
            time = time > 0.0f ? time : instance.showtime
        });
    }

    private class ToastMessage {
        public string message { get; set; }
        public float time { get; set; }



    }
}
