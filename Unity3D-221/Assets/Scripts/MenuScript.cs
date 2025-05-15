using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private GameObject content;
    
    private bool isMuted;

    private Slider effectsSlider;
    private Slider musicSlider;
    private Toggle muteToggle;


    void Start()
    {
        content = transform.Find("Content").gameObject;
        
        effectsSlider = transform.Find("Content/Sounds/EffectsSlider").GetComponent<Slider>();
        musicSlider = transform.Find("Content/Sounds/MusicSlider").GetComponent<Slider>();
        muteToggle = transform.Find("Content/Sounds/MuteToggle").GetComponent<Toggle>();

        if (PlayerPrefs.HasKey("effectsVolume")) {
            GameState.effectsVolume = 
                effectsSlider.value = 
                PlayerPrefs.GetFloat("effectsVolume");
        } else {
            effectsSlider.value = GameState.effectsVolume;
        }

        if (PlayerPrefs.HasKey("musicVolume")) {
            GameState.musicVolume =
                musicSlider.value =
                PlayerPrefs.GetFloat("musicVolume");
        } else {
            musicSlider.value = GameState.musicVolume;
        }

        if (PlayerPrefs.HasKey("isMuted")) {
            isMuted = muteToggle.isOn = PlayerPrefs.GetInt("isMuted") == 1;
        } else {
            isMuted = muteToggle.isOn;
        }

        OnMuteValueChanged(isMuted);
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
        if (!isMuted) {
            GameState.effectsVolume = volume;
        }
    }
    public void OnMusicVolumeValueChanged(float volume) {
        if (!isMuted) {
            GameState.musicVolume = volume;
        }
    }
    public void OnMuteValueChanged(bool isMute) {
        isMuted = isMute;

        if (isMute) {
            GameState.effectsVolume = 0.0f;
            GameState.musicVolume = 0.0f;
        } else {
            GameState.effectsVolume = effectsSlider.value;
            GameState.musicVolume = musicSlider.value;
        }
    }

    private void OnDestroy() {
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.value);
        PlayerPrefs.SetFloat("musicVolume",musicSlider.value);
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);

        PlayerPrefs.Save();
    }
}
