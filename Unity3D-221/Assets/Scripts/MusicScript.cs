using UnityEngine;

public class MusicScript : MonoBehaviour
{
    private AudioSource music;

    void Start()
    {
        music = GetComponent<AudioSource>();

        GameState.AddListener(OnGameStateChanged);
    }


    private void OnGameStateChanged(string fieldName) {
        //if (fieldName == null || fieldName == nameof(GameState.musicVolume)) 
        {
            music.volume = GameState.musicVolume;
        }
    }
    private void OnDestroy() {
        GameState.RemoveListener(OnGameStateChanged);
    }
}
