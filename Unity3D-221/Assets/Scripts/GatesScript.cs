using System.Security.Cryptography;
using UnityEngine;

public class GatesScript : MonoBehaviour
{
    [SerializeField] private int keyNumber = 1;
    [SerializeField] private Vector3 openDirection = Vector3.forward;
    [SerializeField] private float size = 0.7f;
    [SerializeField] private KeyScript nextKey;

    private float openTime;
    private float openTime1 = 4.0f;
    private float openTime2 = 10.5f;

    private int hitCount;

    private bool isKeyInTime = true;
    private bool isOpened = false;
    private bool isKeyInserted;
    private bool isKeyCollected;

    private AudioSource openingSound1;
    private AudioSource openingSound2;


    void Start() {
        isKeyInserted = false;
        hitCount = 0;

        AudioSource[] openingSounds = GetComponents<AudioSource>();
        if (openingSounds.Length == 0) {
            Debug.LogError($"Is Empty");
        }

        if (openingSounds.Length > 0)
            openingSound1 = openingSounds[0];

        if (openingSounds.Length > 1)
            openingSound2 = openingSounds[1];

        GameEventSystem.Subscribe(OnGameEvent);
        GameState.AddListener(OnGameStateChanged);
    }

    void Update() {
        if (!isOpened && isKeyInserted && -(transform.localPosition.magnitude) > -size) {
            transform.Translate(-(size * Time.deltaTime / openTime * openDirection));

            if (-(transform.localPosition.magnitude) <= -size) {
                // Opening ends
                isOpened = true;

                if (openingSound1 != null) {
                    openingSound1.Stop();
                }
                if (openingSound2 != null) {
                    openingSound2.Stop();
                }

                if (nextKey != null) {
                    Debug.Log($"Not NULL");
                    nextKey.StartTimer();
                }
            }
        }

        if (openingSound1 != null && openingSound2 != null &&
            (openingSound1.isPlaying || openingSound2.isPlaying)) {
            openingSound1.volume = openingSound2.volume =
                Time.timeScale == 0.0f ? 0.0f : GameState.effectsVolume;
        } else if (openingSound1 != null && openingSound1.isPlaying) {
            openingSound1.volume = Time.timeScale == 0.0f ? 0.0f : GameState.effectsVolume;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player") {
            if (isKeyCollected) {
                if (!isKeyInserted) { 
                    // Opening begins
                    isKeyInserted = true;
                    openTime = isKeyInTime ? openTime1 : openTime2;

                    if (openingSound1 != null && openingSound2 != null) {
                        (isKeyInTime ? openingSound1 : openingSound2).Play();
                    } else if (openingSound1 != null) {
                        openingSound1.Play();
                    }
                }
            } else {
                if (hitCount == 0) {
                    GameEventSystem.EmitEvent(new GameEvent {
                        type = $"GateHit",
                        toast = $"To open the door, find key #{keyNumber}"
                    });
                } else {
                    GameEventSystem.EmitEvent(new GameEvent {
                        type = $"GateHit",
                        toast = $"{hitCount + 1}nd time I say: To open the door, find key #{keyNumber}"
                    });
                }

                hitCount++;
            }
        }
    }

    private void OnGameEvent(GameEvent gameEvent) {
        if (gameEvent.type == $"Key{keyNumber}Collected") {
            isKeyCollected = true;
            isKeyInTime = (bool)gameEvent.payload;
        }
    }
    private void OnGameStateChanged(string fieldName) {
        if (fieldName == null || fieldName == nameof(GameState.effectsVolume)) {
            if (openingSound1 != null)
                openingSound1.volume = GameState.effectsVolume;
            
            if (openingSound2 != null)
                openingSound2.volume = GameState.effectsVolume;
        }
    }
    private void OnDestroy() {
        GameEventSystem.Unsubscribe(OnGameEvent);
        GameState.RemoveListener(OnGameStateChanged);
    }
}
