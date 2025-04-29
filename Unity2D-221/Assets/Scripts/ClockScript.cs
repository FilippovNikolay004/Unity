using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    private float time;

    void Start()
    {
        time = 0;
        clock = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update() {
        time += Time.deltaTime;

        int t = Mathf.FloorToInt(time);
        int h = t / 3600;
        int m = (t % 3600) / 60;
        int s = t % 60;

        if (h > 0) {
            clock.text = string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
        } else {
            clock.text = string.Format("{0:D2}:{1:D2}", m, s);
        }
    }
}
