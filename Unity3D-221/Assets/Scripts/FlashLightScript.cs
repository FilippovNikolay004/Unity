using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private GameObject player;
    private Light _light;
    public static float charge;
    private float chargeLifeTime = 10.0f;

    private float minAngle = 20f;
    private float maxAngle = 80f;
    private float angleChangeSpeed = 30f;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null) {
            Debug.Log("FlashlightScript: Player not found");
        }

        _light = GetComponent<Light>();
        charge = 1.0f;
    }


    void Update()
    {
        if (player == null) return;

        this.transform.position = player.transform.position;
        this.transform.forward = Camera.main.transform.forward;


        if (GameState.isFpv & !GameState.isDay) {
            _light.intensity = Mathf.Clamp01(charge);
            charge -= charge < 0 ? 0 : Time.deltaTime / chargeLifeTime;

            // Управление углом свечения фонарика
            if (Input.GetKey(KeyCode.Q)) {
                _light.spotAngle = Mathf.Clamp(_light.spotAngle - angleChangeSpeed * Time.deltaTime, minAngle, maxAngle);
            }
            if (Input.GetKey(KeyCode.E)) {
                _light.spotAngle = Mathf.Clamp(_light.spotAngle + angleChangeSpeed * Time.deltaTime, minAngle, maxAngle);
            }
        } else {
            _light.intensity = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Battery")) {
            

            BatteryScript battery = other.gameObject.GetComponent<BatteryScript>();
            if (battery != null) {
                charge += battery.GetCharge();
            }

            

            GameObject.Destroy(other.gameObject);
            GameEventSystem.EmitEvent(new GameEvent { 
                type = "Battery",
                toast = $"You found a battery. Your charge: {charge:F1}",
                sound = EffectSounds.batteryCollected
            });
            Debug.Log("Battery collected: " + charge);
        }
    }
}
