﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficialRotatePlayer : MonoBehaviour
{
    [SerializeField] private OfficialPlayerMovement mov;
    public float rotSpeed = 5.0f;
    private Quaternion wishRot;

    [Header("CanMove")]
    private Menus menuScript;

    void Start()
    {
        menuScript = GameObject.FindGameObjectWithTag("UI").GetComponent<Menus>();
        mov = GetComponentInParent<OfficialPlayerMovement>();
    }

    void Update()
    {
        if (menuScript.canMove)
        {

            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                Vector3 playerMovDir = new Vector3(mov.playerVelocity.x, 0.0f, mov.playerVelocity.z); // Excludes y so that we don't look up and down
                wishRot = Quaternion.LookRotation(playerMovDir); // Make the velocity into a rotation (How? I have no fucking clue)

                transform.rotation = Quaternion.Slerp(transform.rotation, wishRot, rotSpeed * Time.deltaTime); // Rotates towoards currently moved direction

            }
        }
    }
}
