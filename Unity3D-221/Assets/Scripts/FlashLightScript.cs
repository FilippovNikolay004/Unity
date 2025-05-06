using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private GameObject player;
    private Light _light;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null) {
            Debug.Log("FlashlightScript: Player not found");
        }

        _light = GetComponent<Light>();
    }


    void Update()
    {
        if (player == null) return;

        if (CameraScript.isFpv & !LightScript.isDay) {
            this.transform.position = player.transform.position;
            this.transform.forward = Camera.main.transform.forward;
            _light.intensity = 1.0f;
        } else {
            _light.intensity = 0.0f;
        }
    }
}
