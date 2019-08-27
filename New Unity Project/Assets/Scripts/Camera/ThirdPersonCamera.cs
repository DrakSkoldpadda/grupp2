using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    private Transform camTransform;

    private Camera cam;

    public float distance = 10f;

    public float sensivityX = 1f;
    public float sensivityY = 0.5f;

    private const float YAngleMin = 0f;
    private const float YAngleMax = 50f;

    private float currentX = 0f;
    private float currentY = 0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        //Tar mus input och flyttar på kameran
        currentX += Input.GetAxis("Mouse X") * sensivityX;
        currentY += Input.GetAxis("Mouse Y") * sensivityY;

        //Så att man inte kan åka runt spelaren i y led
        currentY = Mathf.Clamp(currentY, YAngleMin, YAngleMax);
    }

    private void LateUpdate()
    {
        //Gör så att kameran "orbittar" runt spelaren
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.position + rotation * dir;

        //Kameran kollar på spelaren
        camTransform.LookAt(target.position);
    }
}
