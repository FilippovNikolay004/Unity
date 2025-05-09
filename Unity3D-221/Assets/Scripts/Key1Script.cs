using UnityEngine;
using UnityEngine.UI;

public class Key1Script : MonoBehaviour
{
    private GameObject content;
    private Image incicatorImage;
    private float timeout = 10.0f;
    private float leftTime;

    void Start()
    {
        content = transform.Find("Content").gameObject;
        incicatorImage = transform
            .Find("Indicator/Canvas/Fg")
            .GetComponent<Image>();

        incicatorImage.fillAmount = 1.0f;
        leftTime = timeout;
        GameState.isKey1InTime = true;
    }


    void Update()
    {
        content.transform.Rotate(0, Time.deltaTime * 30f, 0);      
        
        if (leftTime >= 0) {
            incicatorImage.fillAmount = leftTime / timeout;
            incicatorImage.color = new Color(
                Mathf.Clamp01(2.0f * (1.0f - incicatorImage.fillAmount)),
                Mathf.Clamp01(2.0f * incicatorImage.fillAmount),
                0.0f
            );
            leftTime -= Time.deltaTime;
            if (leftTime < 0) {
                GameState.isKey1InTime = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            GameState.isKey1Collected = true;
            Destroy(this.gameObject);
        }
    }
}
