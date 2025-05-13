using UnityEngine;

public class GatesScript : MonoBehaviour
{
    [SerializeField] private int KeyNumber = 1;
    [SerializeField] private Vector3 openDirection = Vector3.forward;
    [SerializeField] private float size = 0.7f;

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
        openingSound1 = openingSounds[0];
        openingSound2 = openingSounds[1];

        GameEventSystem.Subscribe(OnGameEvent);
    }

    void Update() {
        if (!isOpened && isKeyInserted && -(transform.localPosition.magnitude) > -size) {
            transform.Translate(-(size * Time.deltaTime / openTime * openDirection));

            if (-(transform.localPosition.magnitude) <= -size) {
                // Opening ends
                isOpened = true;
                openingSound1.Stop();
                openingSound2.Stop();
            }
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
                    (isKeyInTime ? openingSound1 : openingSound2).Play();
                }
            } else {
                if (hitCount == 0) {
                    ToasterScript.Toast($"To open the door, find key #{KeyNumber}");
                } else {
                    ToasterScript.Toast($"{hitCount + 1}nd time I say: To open the door, find key #{KeyNumber}");
                }

                hitCount++;
            }
        }
    }

    private void OnGameEvent(GameEvent gameEvent) {
        if (gameEvent.type == $"Key{KeyNumber}Collected") {
            isKeyCollected = true;
            isKeyInTime = (bool)gameEvent.payload;
        }
    }
    private void OnDestroy() {
        GameEventSystem.Unsubscribe(OnGameEvent);
    }
}
