using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript Instance;

    private AudioSource music;



    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать при переходе между сценами
        }
        else
        {
            Destroy(gameObject); // Удалить дубликат
            return;
        }

        music = GetComponent<AudioSource>();
        GameState.AddListener(OnGameStateChanged);

        // Обновить громкость при старте
        music.volume = GameState.musicVolume;
    }

    private void OnGameStateChanged(string fieldName)
    {
        // Настроить на любое изменение, или только по имени поля
        music.volume = GameState.musicVolume;
    }

    private void OnDestroy()
    {
        // Только если именно этот экземпляр уничтожается
        if (Instance == this)
        {
            GameState.RemoveListener(OnGameStateChanged);
        }
    }
}
