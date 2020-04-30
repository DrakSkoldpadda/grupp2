using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightLevel : MonoBehaviour
{

    public Light ljus;
    public float ljusNivåIngame;

    // Start is called before the first frame update
    void Start()
    {
        ljus.intensity = ljusNivåIngame;
    }
    
}
