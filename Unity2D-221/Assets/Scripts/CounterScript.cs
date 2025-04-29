using UnityEngine;

public class CounterScript : MonoBehaviour
{
    private static TMPro.TextMeshProUGUI counter1;
    private static TMPro.TextMeshProUGUI counter2;

    public static void UpdateCounter() {
        counter1.text = SpawnerScript.foodCount[0].ToString();
        counter2.text = SpawnerScript.foodCount[1].ToString();
    }

    void Start()
    {
        counter1 = transform.Find("CounterForFood/Value").GetComponent<TMPro.TextMeshProUGUI>();
        counter2 = transform.Find("CounterForFood1/Value").GetComponent<TMPro.TextMeshProUGUI>();
    }
}
