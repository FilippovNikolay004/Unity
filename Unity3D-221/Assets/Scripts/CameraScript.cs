using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform cameraAnchor;

    private InputAction lookAction;

    private float angelY;
    private float angelX;

    private float sensitivityY = 10.0f;
    private float sensitivityX = 7.0f;


    void Start()
    {
        offset = this.transform.position - cameraAnchor.position;
        lookAction = InputSystem.actions.FindAction("Look");
        
        angelY = this.transform.eulerAngles.y;
        angelX = this.transform.eulerAngles.x;
        
    }

    void Update()
    {
        Vector2 lookValue = Time.deltaTime * lookAction.ReadValue<Vector2>(); // From Unity 6
                                                                              //new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        angelY += lookValue.x * sensitivityX;
        angelX -= lookValue.y * sensitivityY;

        // Ограничение угла наклона вверх/вниз
        angelX = Mathf.Clamp(angelX, 5f, 45f);

        //this.transform.position = cameraAnchor.position + offset;
        this.transform.position = cameraAnchor.position + Quaternion.Euler(0f, angelY, 0f) * offset;
        this.transform.eulerAngles = new Vector3(angelX, angelY, 0f);

    }
}
