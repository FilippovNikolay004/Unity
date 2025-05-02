using UnityEngine;

public class CameraFixedScript : MonoBehaviour {
    private int numberOfClicks = 0;
    private int count;

    void Start() {
        count = transform.childCount;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.X)) {
            if (numberOfClicks >= count) {
                numberOfClicks = 0;
                CameraScript.isFixed = false;
            } else {
                CameraScript.fixedCameraPosition = transform.GetChild(numberOfClicks++).transform;
                CameraScript.isFixed = true;
            }
        }
    }
}
