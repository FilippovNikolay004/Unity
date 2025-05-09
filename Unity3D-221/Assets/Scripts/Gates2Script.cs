using UnityEngine;

public class Gates2Script : MonoBehaviour
{
    private Vector3 openDirection = Vector3.right;

    private float size = 8.48f;
    private float openTime = 10.0f;
    private float openTime1 = 1.0f;
    private float openTime2 = 10.5f;
    private bool isKeyInserted;
    private int hitCount;

    void Start() {
        isKeyInserted = false;
    }


    void Update() {
        if (isKeyInserted && -(transform.localPosition.z) < size) {
            transform.Translate(0f, 0f, -(size * Time.deltaTime / openTime));
        }
    }


    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player") {
            if (GameState.isKey2Collected) {
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
}
