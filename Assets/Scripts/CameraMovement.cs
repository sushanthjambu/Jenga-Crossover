using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform target; // The GameObject we want to focus on
    public float rotationSpeed = 1.0f;
    public float zoomSpeed = 10.0f;
    public float moveSpeed = 1.0f;
    public Vector3 offsetPosition = new Vector3(5.0f, 3.0f, 0f);

    private Vector3 lastMousePosition;
    private bool isUpdatingTarget;

    public void UpdateTarget(Transform newTarget)
    {
        target = newTarget;
        isUpdatingTarget = true;
    }
    void Update()
    {
        //True when user clciks on 6,7 and 8 buttons in UI
        if (isUpdatingTarget)
        {
            //Camera to move from front of structures
            Vector3 newPosition = target.position - target.forward + offsetPosition;

            //Lerp for smooth movement
            transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, newPosition) < 0.02)
            {
                isUpdatingTarget = false;
            }
            if (target != null)
            {
                transform.LookAt(target);
            }
            return;
        }
        // Check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // Calculate the delta movement of the mouse
            float xDelta = Input.mousePosition.x - lastMousePosition.x;
            float yDelta = Input.mousePosition.y - lastMousePosition.y;
            
            if(Mathf.Abs(yDelta) > Mathf.Abs(xDelta))
            {
                // Camera up down movement
                //Vector3 offset = new Vector3(0, yDelta, 0) * rotationSpeed * Time.deltaTime;
                //transform.Translate(offset);
                transform.RotateAround(target.position, transform.right, yDelta * rotationSpeed * Time.deltaTime);
            }
            else
            {
                //Rotate Around a target
                transform.RotateAround(target.position, Vector3.up, xDelta * rotationSpeed * Time.deltaTime);
            }
            
            // Update the last mouse position
            lastMousePosition = Input.mousePosition;
        }

        // Zoom in and out with the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * scroll * zoomSpeed * Time.deltaTime);

        // Make the camera look at the target
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
