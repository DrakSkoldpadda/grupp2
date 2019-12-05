using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform lookAtTarget;
    [SerializeField] private Transform target;

    [SerializeField] private float cameraMinDistance = 2f;
    [SerializeField] private float cameraMaxDistance = 10f;
    private float wantedCamDistance = 5f;
    private float currentCamDistance;

    public float sensivityX = 1f;
    public float sensivityY = 0.5f;

    [SerializeField] private float smoothTime = 0.02f;

    private const float YAngleMin = -5f;
    private const float YAngleMax = 35f;

    //Så att movement scriptet kan använda variabeln
    public float CurrentX { get; private set; }
    private float currentY = 0f;

    [HideInInspector] public bool canUseCamera;
    [HideInInspector] public bool isInMenu;

    private void Awake()
    {
        if (target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").transform;
            }
        }
    }

    private void Start()
    {
        if (lookAtTarget != null)
        {
            lookAtTarget = target;
        }
    }

    private void Update()
    {
        //Tar mus input för flytta på kameran
        CurrentX += Input.GetAxis("Mouse X") * sensivityX;
        currentY += -Input.GetAxis("Mouse Y") * sensivityY;

        //Tar controller input för att flytta på kameran
        CurrentX += Input.GetAxis("Joy X") * sensivityX;
        currentY += Input.GetAxis("Joy Y") * sensivityY;

        //Så att man inte kan åka runt spelaren i y led
        currentY = Mathf.Clamp(currentY, YAngleMin, YAngleMax);

        WantedCamDistance();

        CamCollisionDistance();
    }

    //Så att cameran slår i teräng så att cameran inte åker utanför kartan
    private void CamCollisionDistance()
    {
        RaycastHit hit;
        float wallBuffer = 0.01f;

        if (Physics.Raycast(target.position, -transform.TransformDirection(Vector3.forward), out hit, wantedCamDistance, LayerMask.GetMask("Terrain")))
        {
            currentCamDistance = hit.distance - wallBuffer;
        }
        else
        {
            currentCamDistance = wantedCamDistance;
        }
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

    private void FixedUpdate()
    {
        if (canUseCamera)
        {
            Vector3 velocity = Vector3.zero;

            //Gör så att kameran "orbittar" runt spelaren
            Vector3 dir = new Vector3(0, 0, -currentCamDistance);
            Quaternion rotation = Quaternion.Euler(currentY, CurrentX, 0);

            transform.position = Vector3.SmoothDamp(transform.position, target.position + rotation * dir, ref velocity, smoothTime);
        }
    }

    private void LateUpdate()
    {
        if (canUseCamera)
        {
            //Vad kameran ska kolla på
            transform.LookAt(lookAtTarget.position);
        }
    }
}
