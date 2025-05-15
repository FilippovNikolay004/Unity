using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private int keyNumber = 1;
    [SerializeField] private float timeout = 10.0f;
    [SerializeField] private string desk = "color";
    [SerializeField] private bool isFirstKey = false;

    private GameObject content;
    private Image indicatorImage;

    private float leftTime;

    private bool isInTime = true;
    private bool isTimerRunning = false;


    void Start() {
        content = transform.Find("Content").gameObject;
        indicatorImage = transform
            .Find("Indicator/Canvas/Fg")
            .GetComponent<Image>();

        indicatorImage.fillAmount = 1.0f;
        if (isFirstKey) {
            StartTimer();
        }
    }


    void Update() {
        content.transform.Rotate(0, Time.deltaTime * 30f, 0);
        if (isTimerRunning && leftTime >= 0) {
            indicatorImage.fillAmount = leftTime / timeout;
            indicatorImage.color = new Color(
                Mathf.Clamp01(2.0f * (1.0f - indicatorImage.fillAmount)),
                Mathf.Clamp01(2.0f * indicatorImage.fillAmount),
                0.0f
            );
            leftTime -= Time.deltaTime;
            if (leftTime < 0) {
                isInTime = false;
            }
        }
    }

    public void StartTimer() {
        leftTime = timeout;
        isTimerRunning = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            GameEventSystem.EmitEvent(new GameEvent {
                type = $"Key{keyNumber}Collected",
                payload = isInTime,
                toast = $"key #{keyNumber} has been found. You can open the {desk} door.",
                sound = isInTime
                ? EffectSounds.keyCollectedInTime
                : EffectSounds.keyCollectedOutOfTime
            });
            Destroy(this.gameObject);
        }
    }
}
