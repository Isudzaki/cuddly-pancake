using UnityEngine;

public class RotateCanvasNick : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // Assuming the camera is tagged as "MainCamera", you can change this accordingly
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }
    }

    private void Update()
    {
        RotateCanvas();
    }

    private void RotateCanvas()
    {
        transform.eulerAngles = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    }
}
