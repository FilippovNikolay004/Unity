using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private int keyNumber = 1;
    [SerializeField] private float timeout = 10.0f;

    private GameObject content;
    private Image indicatorImage;
    private float leftTime;
    private bool isInTime = true;

    void Start() {
        content = transform.Find("Content").gameObject;
        indicatorImage = transform
            .Find("Indicator/Canvas/Fg")
            .GetComponent<Image>();

        indicatorImage.fillAmount = 1.0f;
        leftTime = timeout;
        GameState.isKey1InTime = true;
    }


    void Update() {
        content.transform.Rotate(0, Time.deltaTime * 30f, 0);

        if (leftTime >= 0) {
            indicatorImage.fillAmount = leftTime / timeout;
            indicatorImage.color = new Color(
                Mathf.Clamp01(2.0f * (1.0f - indicatorImage.fillAmount)),
                Mathf.Clamp01(2.0f * indicatorImage.fillAmount),
                0.0f
            );
            leftTime -= Time.deltaTime;
            if (leftTime < 0) {
                //GameState.isKey1InTime = false;
                //GameState.SetProperty($"isKey{keyNumber}InTime", false);
                isInTime = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            //GameState.isKey1Collected = true;
            //GameState.SetProperty($"isKey{keyNumber}Collected", true);
            GameEventSystem.EmitEvent(new GameEvent {
                type = $"Key{keyNumber}Collected",
                payload = isInTime,
                toast = $"key #{keyNumber} has been found. You can open the black door."
            });
            Destroy(this.gameObject);
        }
    }
}
