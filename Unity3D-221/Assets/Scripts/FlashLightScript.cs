using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null) {
            Debug.Log("FlashlightScript: Player not found");
        }
    }


    void Update()
    {
        if (player == null) return;
    }
}
