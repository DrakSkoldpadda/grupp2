using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalAvtivator : Lighted
{
    [SerializeField] private Renderer crystalRenderer;
    public Color crystaltColor;

    [SerializeField] private Light spotLight;
    [SerializeField] private Light pointLight;

    private bool startLighting;

    public float spotOriginalIntesety;
    public float pointOriginalIntesety;

    public float lightIncreaseSpeed;


    private float redColor = 0;
    private float blueColor = 0;
    private float greenColor = 0;

    private void Awake()
    {
        if(spotLight == null)
        {
            Debug.LogError("Crystal " + gameObject.name + " is missing a spotlight");
        }
        if (pointLight == null)
        {
            Debug.LogError("Crystal " + gameObject.name + " is missing a pointlight");
        }
    }

    private void Start()
    {
        crystalRenderer = GetComponent<Renderer>();

        spotOriginalIntesety = spotLight.intensity;
        pointOriginalIntesety = pointLight.intensity;

        spotLight.intensity = 0f;
        pointLight.intensity = 0f;

        crystalRenderer.material.SetColor("_EmissionColor", new Color(0, 0, 0));
        crystalRenderer.material.SetColor("_BaseColor", new Color(0, 0, 0));

        spotLight.color = crystaltColor;
        pointLight.color = crystaltColor;
    }

    public override void Activated()
    {
        startLighting = true;
    }

    public override void Update()
    {
        base.Update();

        if (startLighting)
        {
            if (spotLight.intensity < spotOriginalIntesety)
                spotLight.intensity = spotLight.intensity + lightIncreaseSpeed * Time.deltaTime;

            if (pointLight.intensity < pointOriginalIntesety)
                pointLight.intensity = pointLight.intensity + lightIncreaseSpeed * Time.deltaTime;

            if (redColor < crystaltColor.r)
                redColor = redColor + 0.1f * Time.deltaTime;

            if (greenColor < crystaltColor.g)
                greenColor = greenColor + 0.1f * Time.deltaTime;

            if (blueColor < crystaltColor.b)
                blueColor = blueColor + 0.1f * Time.deltaTime;


            crystalRenderer.material.SetColor("_EmissionColor", new Color(redColor, greenColor, blueColor));
            crystalRenderer.material.SetColor("_BaseColor", new Color(redColor, greenColor, blueColor));




        }

    }

}
