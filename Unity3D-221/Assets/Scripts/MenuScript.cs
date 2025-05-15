using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private GameObject content;

    void Start()
    {
        content = transform.Find("Content").gameObject;
        Hide();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (content.activeInHierarchy) {
                Hide();
            } else {
                Show();
            }
        }
    }


    private void Hide() {
        content.SetActive(false);
        Time.timeScale = 1.0f;
    }
    private void Show() {
        content.SetActive(true);
        Time.timeScale = 0.0f;
    }


    public void OnEffectsVolumeValueChanged(float volume) {
        GameState.effectsVolume = volume;
    }
    public void OnMusicVolumeValueChanged(float volume) {
        GameState.musicVolume = volume;
    }
    public void OnMusicVolumeValueChanged(bool isMute) {
        
    }
}
