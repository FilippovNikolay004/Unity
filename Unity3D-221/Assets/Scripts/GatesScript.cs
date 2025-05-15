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

        if (nextKey == null) {
            Debug.LogError($"{gameObject.name}: nextKey is NULL!");
        } else {
            Debug.Log($"{gameObject.name}: nextKey is {nextKey.name}");
        }

        //AudioSource[] openingSounds = GetComponents<AudioSource>();
        //openingSound1 = openingSounds[0];
        //openingSound2 = openingSounds[1];

        GameEventSystem.Subscribe(OnGameEvent);
    }

    void Update() {
        if (!isOpened && isKeyInserted && -(transform.localPosition.magnitude) > -size) {
            transform.Translate(-(size * Time.deltaTime / openTime * openDirection));

            if (-(transform.localPosition.magnitude) <= -size) {
                // Opening ends
                isOpened = true;
                //openingSound1.Stop();
                //openingSound2.Stop();

                if (nextKey != null) {
                    Debug.Log($"Not NULL");
                    nextKey.StartTimer();
                }
            }
        }

        //if ((openingSound1.isPlaying || openingSound2.isPlaying)) {
        //    openingSound1.volume = openingSound2.volume = 
        //        Time.timeScale == 0.0f ? 0.0f : GameState.effectsVolume;
        //}
    }
    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player") {
            if (isKeyCollected) {
                if (!isKeyInserted) { 
                    // Opening begins
                    isKeyInserted = true;
                    openTime = isKeyInTime ? openTime1 : openTime2;
                    //(isKeyInTime ? openingSound1 : openingSound2).Play();
                }
            } else {
                if (hitCount == 0) {
                    ToasterScript.Toast($"To open the door, find key #{keyNumber}");
                } else {
                    ToasterScript.Toast($"{hitCount + 1}nd time I say: To open the door, find key #{keyNumber}");
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
    private void OnDestroy() {
        GameEventSystem.Unsubscribe(OnGameEvent);
    }
}
