using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private InputAction moveAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update() {
        Vector2 moveValue = moveAction.ReadValue<Vector2>(); // From Unity 6
                                                             //new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                                                             //rb.AddForce(moveValue.x, 0f, moveValue.y);
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        if (cameraForward == Vector3.zero) {
            cameraForward = Camera.main.transform.up;
        } else {
            cameraForward.Normalize();
        }

        Vector3 force = cameraForward * moveValue.y + cameraRight * moveValue.x;
        rb.AddForce(force);
    }
}
