using System.Linq;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light[] dayLights;
    private Light[] nightLights;

    public static bool isDay;

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

        isDay = true;

        foreach (Light light in nightLights) {
            light.intensity = 0.0f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) {
            isDay = !isDay;

            if (isDay) {
                foreach(var item in dayLights) {
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
                foreach (var item in nightLights) {
                    item.intensity = 1.0f;
                }
                RenderSettings.ambientIntensity = 0.0f;
                RenderSettings.reflectionIntensity = 0.0f;
            }
        }
    }
}
