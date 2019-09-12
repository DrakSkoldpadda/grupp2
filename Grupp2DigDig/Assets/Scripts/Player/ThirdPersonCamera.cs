using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //Public så att man kan kolla på ett event som sker långt borta
    private Transform lookAtTarget;
    private Transform target;

    [SerializeField] private float cameraMinDistance = 2f;
    [SerializeField] private float cameraMaxDistance = 13f;
    [SerializeField] private float wantedCamDistance;
    private float currentCamDistance;

    [SerializeField] private float sensivityX = 1f;
    [SerializeField] private float sensivityY = 0.5f;

    [SerializeField] private float smoothTime = 0.05f;

    private const float YAngleMin = -10f;
    private const float YAngleMax = 80f;

    //Så att movement scriptet kan använda variabeln
    public float CurrentX { get; private set; }
    private float currentY = 0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        target = GameObject.FindWithTag("Player").transform;
        lookAtTarget = target;

        wantedCamDistance = 7f;
    }

    private void Update()
    {
        //Tar mus input för flyttar på kameran
        CurrentX += Input.GetAxis("Mouse X") * sensivityX;
        currentY += -Input.GetAxis("Mouse Y") * sensivityY;

        //Så att man inte kan åka runt spelaren i y led
        currentY = Mathf.Clamp(currentY, YAngleMin, YAngleMax);

        WantedCamDistance();

        CamCollisionDistance();
    }

    //Så att man kan scrolla in och ut från karaktären
    private void WantedCamDistance()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && wantedCamDistance != cameraMinDistance)
        {
            wantedCamDistance--;
        }
        else if (wantedCamDistance < cameraMinDistance)
        {
            wantedCamDistance = cameraMinDistance;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && wantedCamDistance != cameraMaxDistance)
        {
            wantedCamDistance++;
        }
        else if (wantedCamDistance > cameraMaxDistance)
        {
            wantedCamDistance = cameraMaxDistance;
        }
    }

    //Så att cameran slår i teräng så att cameran inte åker utanför kartan
    private void CamCollisionDistance()
    {
        RaycastHit hit;
        float wallBuffer = 2f;

        if (Physics.Raycast(target.position, -transform.TransformDirection(Vector3.forward), out hit, wantedCamDistance, LayerMask.GetMask("Terrain")))
        {
            currentCamDistance = hit.distance - wallBuffer;
        }
        else
        {
            currentCamDistance = wantedCamDistance;
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;

        //Gör så att kameran "orbittar" runt spelaren
        Vector3 dir = new Vector3(0, 0, -currentCamDistance);
        Quaternion rotation = Quaternion.Euler(currentY, CurrentX, 0);

        transform.position = Vector3.SmoothDamp(transform.position, target.position + rotation * dir, ref velocity, smoothTime);
    }

    private void LateUpdate()
    {
        //transform.position = target.position + rotation * dir;

        //Vad kameran ska kolla på
        transform.LookAt(lookAtTarget.position);
    }
}
