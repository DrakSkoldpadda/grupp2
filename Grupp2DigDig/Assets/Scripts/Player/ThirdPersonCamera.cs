using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //Public så att man kan kolla på ett event som sker långt borta
    [HideInInspector] public Transform lookAtTarget;
    private Transform target;

    [SerializeField] private float cameraMaxDistance = 10f;
    private float currentCamDistance;

    [SerializeField] private float sensivityX = 1f;
    [SerializeField] private float sensivityY = 0.5f;

    [SerializeField] private float smoothTime = 0.1f;

    private const float YAngleMin = -10f;
    private const float YAngleMax = 80f;

    private float currentX = 0f;
    private float currentY = 0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        target = GameObject.FindWithTag("Player").transform;
        lookAtTarget = target;

        currentCamDistance = cameraMaxDistance;
    }

    private void Update()
    {
        //Tar mus input för flyttar på kameran
        currentX += Input.GetAxis("Mouse X") * sensivityX;
        currentY += -Input.GetAxis("Mouse Y") * sensivityY;

        //Så att man inte kan åka runt spelaren i y led
        currentY = Mathf.Clamp(currentY, YAngleMin, YAngleMax);

        CamDistance();
    }

    //Så att cameran slår i teräng så att cameran inte åker utanför kartan
    private void CamDistance()
    {
        RaycastHit hit;
        float wallBuffer = 2f;

        if (Physics.Raycast(target.position, -transform.TransformDirection(Vector3.forward), out hit, cameraMaxDistance, LayerMask.GetMask("Terrain")))
        {
            currentCamDistance = hit.distance - wallBuffer;
        }
        else
        {
            currentCamDistance = cameraMaxDistance;
        }
    }

    private void LateUpdate()
    {
        Vector3 velocity = Vector3.zero;

        //Gör så att kameran "orbittar" runt spelaren
        Vector3 dir = new Vector3(0, 0, -currentCamDistance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = Vector3.SmoothDamp(transform.position, target.position + rotation * dir, ref velocity, smoothTime);

        //Vad kameran ska kolla på
        transform.LookAt(lookAtTarget.position);
    }
}
