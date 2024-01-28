using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera firstCamera;
    public Camera secondCamera;

    void Start()
    {
        // Ensure both cameras are initially disabled
        firstCamera.enabled = false;
        secondCamera.enabled = false;

        // Enable the first camera at the start
        SwitchCamera(firstCamera);
    }

    void Update()
    {
        // Check for input to switch cameras (you can customize this part)
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle between cameras
            if (firstCamera.enabled)
                SwitchCamera(secondCamera);
            else
                SwitchCamera(firstCamera);
        }
    }

    void SwitchCamera(Camera newMainCamera)
    {
        // Disable the current main camera
        Camera currentMainCamera = Camera.main;
        if (currentMainCamera != null)
            currentMainCamera.enabled = false;

        // Enable the new main camera
        newMainCamera.enabled = true;

        // Set the new main camera as the main camera (tag is optional)
        newMainCamera.tag = "MainCamera";
    }
}
