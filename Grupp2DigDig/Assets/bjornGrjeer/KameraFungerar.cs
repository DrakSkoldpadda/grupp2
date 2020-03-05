using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraFungerar : MonoBehaviour
{
    [SerializeField] private ThirdPersonCamera cameraScript;

    private void Start()
    {
        cameraScript.canUseCamera = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
