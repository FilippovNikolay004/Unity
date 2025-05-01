using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.name);
        DeepDestroy(other.gameObject);
    }

    public static void DestroyAllObjectsWithTag(string tag = "Pipe") {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects) {
            DeepDestroy(obj);
        }
    }

    private static void DeepDestroy(GameObject obj) {
        Transform current = obj.transform;
        while (current != null) {
            Transform parent = current.parent;
            Destroy(current.gameObject);
            current = parent;
        }
    }

    public static void ClearField() {
        DestroyAllObjectsWithTag("Pipe");
        DestroyAllObjectsWithTag("Food");
    }
}
