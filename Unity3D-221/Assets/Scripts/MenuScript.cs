using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private GameObject content;
    
    private bool isMuted;

    private Slider effectsSlider;
    private Slider musicSlider;
    private Slider gateVolumeSlider;
    private Toggle muteToggle;

    private float startTimeScale;

    private float defaultMusicVolume;
    private float defaultEffectsVolume;
    private float defaultGateVolume;
    private bool defaultIsMuted;


    void Start()
    {
        GetDefaults();

        content = transform.Find("Content").gameObject;
        
        effectsSlider = transform.Find("Content/Sounds/EffectsSlider").GetComponent<Slider>();
        musicSlider = transform.Find("Content/Sounds/MusicSlider").GetComponent<Slider>();
        gateVolumeSlider = transform.Find("Content/Sounds/GatesSlider").GetComponent<Slider>();
        muteToggle = transform.Find("Content/Sounds/MuteToggle").GetComponent<Toggle>();

        LoadSaves();
        OnMuteValueChanged(isMuted);

        startTimeScale = Time.timeScale;
        
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

    private void GetDefaults() {
        defaultEffectsVolume = GameState.effectsVolume;
        defaultMusicVolume = GameState.musicVolume;
        defaultGateVolume = GameState.gateVolume;
        defaultIsMuted = false;
    }

    private void LoadSaves() {
        if (PlayerPrefs.HasKey("effectsVolume")) {
            GameState.effectsVolume =
                effectsSlider.value =
                PlayerPrefs.GetFloat("effectsVolume");
        } else {
            effectsSlider.value = defaultEffectsVolume;
        }

        if (PlayerPrefs.HasKey("musicVolume")) {
            GameState.musicVolume =
                musicSlider.value =
                PlayerPrefs.GetFloat("musicVolume");
        } else {
            musicSlider.value = defaultMusicVolume;
        }

        if (PlayerPrefs.HasKey("gateVolume")) {
            GameState.gateVolume =
                gateVolumeSlider.value =
                PlayerPrefs.GetFloat("gateVolume");
        }

        if (PlayerPrefs.HasKey("isMuted")) {
            isMuted = muteToggle.isOn = PlayerPrefs.GetInt("isMuted") == 1;
        } else {
            isMuted = defaultIsMuted;
        }
    }


    private void Hide() {
        content.SetActive(false);
        Time.timeScale = startTimeScale;
    }
    private void Show() {
        startTimeScale = Time.timeScale;
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
    public void OnGateVolumeValueChanged(float volume) {
        if (!isMuted) {
            GameState.gateVolume = volume;
        }
    }
    public void OnMuteValueChanged(bool isMute) {
        isMuted = isMute;

        if (isMute) {
            GameState.effectsVolume = 0.0f;
            GameState.musicVolume = 0.0f;
            GameState.gateVolume = 0.0f;
        } else {
            GameState.effectsVolume = effectsSlider.value;
            GameState.musicVolume = musicSlider.value;
            GameState.gateVolume = gateVolumeSlider.value;
        }
    }


    // Buttons
    public void OnContinueClick() {
        Hide();
    }
    public void OnDefaultsClick() {
        GameState.effectsVolume = effectsSlider.value = defaultEffectsVolume;
        GameState.musicVolume = musicSlider.value = defaultMusicVolume;
        GameState.gateVolume = gateVolumeSlider.value = defaultGateVolume;
        isMuted = muteToggle.isOn = defaultIsMuted;
    }
    public void OnExitClick() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        #if UNITY_STANDALONE
            Application.Quit();
        #endif
    }


    private void OnDestroy() {
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.value);
        PlayerPrefs.SetFloat("musicVolume",musicSlider.value);
        PlayerPrefs.SetFloat("gateVolume", gateVolumeSlider.value);
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);

        PlayerPrefs.Save();
    }
}
