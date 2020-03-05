using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighted : MonoBehaviour
{
    private Transform lamp;
    [SerializeField] private float activtedRange = 4;
    [SerializeField] private bool activated;
    private Material mat;
    private Color matColor;

    [SerializeField] private Activated activatedObject;

    private void Awake()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        matColor = mat.color;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckLamp();

        if (Vector3.Distance(lamp.position, transform.position) < activtedRange)
        {
            activated = true;
            mat.color = Color.yellow;

            activatedObject.Activate();
        }
        else
        {
            activated = false;
            mat.color = matColor;
            activatedObject.DeActivate();
        }
    }

    public void CheckLamp()
    {
        lamp = GameObject.FindWithTag("lykta").GetComponent<Transform>();
    }
}
