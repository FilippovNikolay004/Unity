using System.Security.Cryptography;
using UnityEngine;

public class Gates1Script : MonoBehaviour
{

    private float size = 0.2f;
    private float openTime;
    private float openTime1 = 1.0f;
    private float openTime2 = 10.5f;
    private bool isKeyInserted;
    private int hitCount;
    private int KeyNumber;
    private bool isKeyCollected;

    void Start()
    {
        isKeyInserted = false;
        hitCount = 0;
        GameState.AddListener(OnGameStateChanged);
    }


    void Update()
    {
        if (isKeyInserted && -(transform.localPosition.x) < size) {
            transform.Translate(0f, 0f, -(size * Time.deltaTime / openTime));
        }
    }


    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player") {
            if (GameState.isKey1Collected) {
                isKeyInserted = true;
                openTime = GameState.isKey1InTime ? openTime1 : openTime2;
            } else {
                if (hitCount == 0) {
                    ToasterScript.Toast("To open the door, find key 1 (black)");
                } else {
                    ToasterScript.Toast($"{hitCount + 1}nd time I say: To open the door, find key 1 (black)");
                }

                hitCount++;
            }
        }
    }

    private void OnGameStateChanged(string fieldName) {
        if (fieldName == $"isKey {KeyNumber} Collected") {
            isKeyCollected = true;
        }
    }
}
