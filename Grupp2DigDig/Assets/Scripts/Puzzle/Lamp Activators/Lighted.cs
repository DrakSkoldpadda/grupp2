using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighted : MonoBehaviour
{
    private Transform lamp;
    [SerializeField] private bool activated;
    [SerializeField] private float activtedRange = 4;

    [SerializeField] protected private Activated activatedObject;

    // Update is called once per frame
    private void Update()
    {
        CheckLamp();

        if (Vector3.Distance(lamp.position, transform.position) < activtedRange)
        {
            activated = true;
            Activated();
        }
        else
        {
            activated = false;
            Deactivated();
        }
    }

    virtual public void Activated() { }

    virtual public void Deactivated() { }

    public void CheckLamp()
    {
        lamp = GameObject.FindWithTag("lykta").GetComponent<Transform>();
    }
}
