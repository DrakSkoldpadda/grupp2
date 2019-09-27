using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampColor : MonoBehaviour
{
    [SerializeField] private float rate = 1;
    [SerializeField] private float currentColor;

    [SerializeField] private Light light;


    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentColor == 1f)
        {
            currentColor = 0f;
        }

        currentColor = rate * Time.deltaTime;

        light.color = Color.HSVToRGB(currentColor, 1, 1);
    }
}
