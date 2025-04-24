using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private float easySpeed = 5f;     // скорость для простого режима
    [SerializeField] private float hardSpeed = 15f;    // скорость для сложного режима
    [SerializeField] private bool isHardMode = false;  // флаг режима игры

    private float speed;

    void Start() {
        speed = isHardMode ? hardSpeed : easySpeed;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World); // Двигаем объект влево
    }

    #region TODO
    /*
     * Використовуючи [SerializeField] підібрати мінімальну та максимальну
     * швидкість руху елементів для складного та простого режимів гри.
     * Зробити аналогічні дії з множником сили для управління персонажем.
     */
    #endregion
}
