using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform cameraAnchor;

    private InputAction lookAction;

    private float angleX0;
    private float angleY0;

    private float angleY;
    private float angleX;

    private float sensitivityY = 10.0f;
    private float sensitivityX = 7.0f;

    private float minOffset = 1.0f;
    private float maxOffset = 2.5f;

    private float maxAngleY = 45f;
    private float minAngleY = 5f;
    private float maxAngleYFpv = 35.0f;
    private float minAngleYFpv = -60.0f;

    public static bool isFixed = false;
    public static Transform fixedCameraPosition = null!;

    void Start()
    {
        offset = this.transform.position - cameraAnchor.position;
        lookAction = InputSystem.actions.FindAction("Look");
        
        angleY = angleY0 = this.transform.eulerAngles.y;
        angleX = angleX0 = this.transform.eulerAngles.x;

        GameState.isFpv = offset.magnitude < minOffset;
    }

    void Update()
    {
        if (isFixed) {
            this.transform.position = fixedCameraPosition.position;
            this.transform.rotation = fixedCameraPosition.rotation;
        } else {
            Vector2 zoom = Input.mouseScrollDelta;
            if (zoom.y > 0 && !GameState.isFpv) {
                offset *= 0.9f;
                if (offset.magnitude < minOffset) {
                    offset *= 0.01f;
                    GameState.isFpv = true;
                }
            } else if (zoom.y < 0) {
                if (GameState.isFpv) {
                    offset *= minOffset / offset.magnitude;
                    GameState.isFpv = false;
                }
                if (offset.magnitude < maxOffset) {
                    offset *= 1.1f;
                }
            }

            Vector2 lookValue = Time.deltaTime * lookAction.ReadValue<Vector2>(); // From Unity 6
                                                                                  //new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            angleY += lookValue.x * sensitivityX;
            angleX -= lookValue.y * sensitivityY;

            if (!GameState.isFpv)
                // Ограничение угла наклона вверх/вниз
                angleX = Mathf.Clamp(angleX, minAngleY, maxAngleY);
            else
                // Ограничение угла наклона при виде от первого лица вверх/вниз
                angleX = Mathf.Clamp(angleX, minAngleYFpv, maxAngleYFpv);

            //this.transform.position = cameraAnchor.position + offset;
            //this.transform.position = cameraAnchor.position + Quaternion.Euler(0f, angelY - angelY0, 0f) * offset;
            this.transform.position = cameraAnchor.position + Quaternion.Euler(angleX - angleX0, angleY - angleY0, 0f) * offset;
            this.transform.eulerAngles = new Vector3(angleX, angleY, 0f);
        }
    }
}
