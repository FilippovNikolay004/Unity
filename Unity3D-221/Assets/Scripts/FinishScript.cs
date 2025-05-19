using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            DontDestroyOnLoad(other.gameObject);
            DontDestroyOnLoad(GameObject.Find("Music"));
            SceneManager.LoadScene(1);
        }
    }
}
