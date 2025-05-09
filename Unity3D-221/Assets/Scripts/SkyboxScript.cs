using UnityEngine;

public class SkyboxScript : MonoBehaviour
{
    [SerializeField] private Material daySkyBox;
    [SerializeField] private Material nightSkyBox;


    void Start()
    {
        GameState.AddListener(OnGameStateChanged);
        TargetSkyBlox();
    }

    private void OnGameStateChanged(string fieldName) {
        if (fieldName == nameof(GameState.isDay)) { TargetSkyBlox(); }
    }
    private void TargetSkyBlox() {
        RenderSettings.skybox = GameState.isDay ? daySkyBox : nightSkyBox;
    }
    private void OnDestroy() {
        GameState.RemoveListener(OnGameStateChanged);
    }
}
