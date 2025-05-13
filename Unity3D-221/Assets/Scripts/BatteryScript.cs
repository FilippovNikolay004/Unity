using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    [SerializeField] private float powerCharge = 1.0f;
    public float GetCharge() => powerCharge;
}
