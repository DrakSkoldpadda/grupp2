using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPost : Lighted
{
    private Material mat;
    private Color matColor;

    void Awake()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        matColor = mat.color;
    }

    public override void Activated()
    {
        mat.color = Color.yellow;
        if (activatedObject != null)
        {
            activatedObject.Activate();
        }
    }

    public override void Deactivated()
    {
        mat.color = matColor;
        if (activatedObject != null)
        {
            activatedObject.DeActivate();
        }
    }
}