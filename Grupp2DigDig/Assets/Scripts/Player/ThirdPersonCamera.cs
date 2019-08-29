using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform lookAtTarget;
    private Transform target;
    private Transform camTransform;

    [SerializeField] private float cameraDistance = 10f;
    private float currentCamDistance;

    [SerializeField] private float sensivityX = 1f;
    [SerializeField] private float sensivityY = 0.5f;

    private const float YAngleMin = -10f;
    private const float YAngleMax = 80f;

    private float currentX = 0f;
    private float currentY = 0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        camTransform = transform;

        target = GameObject.FindWithTag("Player").transform;
        lookAtTarget = target;
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
        //Så att cameran slår i teräng så att cameran inte åker utanför kartan
        RaycastHit hit;

        if (Vector3.Distance(transform.position, target.position) < cameraDistance)
        {
            if (Physics.Raycast(target.position, (transform.position - target.position), out hit, cameraDistance))
            {
                if (hit.collider.gameObject.tag == "Terrain")
                {
                    hit.distance = currentCamDistance;
                }
            }
        }

        //Gör så att kameran "orbittar" runt spelaren
        Vector3 dir = new Vector3(0, 0, -currentCamDistance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.position + rotation * dir;

        //Vad kameran ska kolla på
        camTransform.LookAt(lookAtTarget.position);
    }
}
