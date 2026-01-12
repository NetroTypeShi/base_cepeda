using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera cameraA;
    public Camera cameraB;
    public KeyCode switchKey = KeyCode.V;
    public bool movement2d = true;
    public bool movement3d = false;
    bool useA;

    void Start()
    {
        if (cameraA != null) cameraA.enabled = true;
        if (cameraB != null) cameraB.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey)) 
        {
            ToggleCameras();
            ToggleControls();
        }
            
    }

    void ToggleCameras()
    {
        if (cameraA == null && cameraB == null) return;

        if (cameraA == null)
        {
            cameraB.enabled = !cameraB.enabled;
            return;
        }
        if (cameraB == null)
        {
            cameraA.enabled = !cameraA.enabled;
            return;
        }

        // Normal case: toggle between the two cameras
        useA = !cameraA.enabled;
        cameraA.enabled = useA;
        cameraB.enabled = !useA;

    }
    void ToggleControls()
    {
        if (cameraA.enabled)
        {
            movement2d = true;
            movement3d = false;
        }
        if (cameraB.enabled)
        {
            movement2d = false;
            movement3d = true;
        }
    }
}
