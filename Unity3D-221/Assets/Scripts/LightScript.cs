using System.Linq;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light[] dayLights;
    private Light[] nightLights;

    void Start()
    {
        dayLights = GameObject
            .FindGameObjectsWithTag("Day")
            .Select(g => g.GetComponent<Light>())
            .ToArray();
        nightLights = GameObject
            .FindGameObjectsWithTag("Night")
            .Select(g => g.GetComponent<Light>())
            .ToArray();

        GameState.isDay = true;

        foreach (Light light in nightLights) {
            light.intensity = 0.0f;
        }

        GameState.AddListener(OnGameStateChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) {
            GameState.isDay = !GameState.isDay;
        }
    }



    private void ToggleLights() {
        if (GameState.isDay) {
            foreach (var item in dayLights) {
                item.intensity = 1.0f;
            }
            foreach (var item in nightLights) {
                item.intensity = 0.0f;
            }
            RenderSettings.ambientIntensity = 1.0f;
            RenderSettings.reflectionIntensity = 1.0f;
        } else {
            foreach (var item in dayLights) {
                item.intensity = 0.0f;
            }

            if (!GameState.isFpv) {
                foreach (var item in nightLights) {
                    item.intensity = 1.0f;
                }
            }

            RenderSettings.ambientIntensity = 0.0f;
            RenderSettings.reflectionIntensity = 0.0f;
        }
    }
    private void FpvChanged() {
        if (!GameState.isDay) {
            foreach (var item in nightLights) {
                item.intensity = GameState.isFpv ? 0.0f : 1.0f;
            }
        }
    }

    private void OnGameStateChanged(string fieldName) {
        if (fieldName == nameof(GameState.isDay)) {
            ToggleLights();
        } else if (fieldName == nameof(GameState.isFpv)) {
            FpvChanged();
        }
    }
    private void OnDestroy() {
        GameState.RemoveListener(OnGameStateChanged);
    }
}
