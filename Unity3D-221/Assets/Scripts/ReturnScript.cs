using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            DontDestroyOnLoad(other.gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
