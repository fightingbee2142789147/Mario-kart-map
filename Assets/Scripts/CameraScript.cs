using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    // The target we are following
    public Controls control;
    public Transform target;
    // The distance in the x-z plane to the target
    public int distance = 10;
    // the height we want the camera to be above the target
    public int height = 10;
    // How much we 
    public float heightDamping = 2.0f;
    public float rotationDamping = 0.6f;

    void Start()
    {
        control = GameObject.Find("Player").GetComponent<Controls>();
    }
    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!Input.GetKey(KeyCode.C))
        {
            rotationDamping = control.rotateSpeed + 6;
        }
        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(target);
        
    }
}

   