using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzel2Script : MonoBehaviour
{

    public GameObject hingeJärn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Swagmoney()
    {
        yield return new WaitForSeconds(3);

        transform.Rotate(hingeJärn.transform.up, 900);
    }
}
