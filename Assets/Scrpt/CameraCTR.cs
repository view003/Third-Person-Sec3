using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCTR : MonoBehaviour
{
    [SerializeField] private float mouseSensitiveity = 3.0f;

    private float rotationY;
    private float rotationX;

    [SerializeField] private Transform target;

    [SerializeField] private float distanceFromTarget = 3.0f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField] private float smoothTime = 0.2f;

    [SerializeField] private Vector2 rotationXMinMax = new Vector2 (-20, 40);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitiveity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitiveity;

        rotationY += mouseX;
        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);

        Vector3 nextRotaion = new Vector3 (rotationX, rotationY);

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotaion, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
