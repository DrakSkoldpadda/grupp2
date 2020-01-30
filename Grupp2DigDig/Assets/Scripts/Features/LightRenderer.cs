using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LightRenderer : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    private LineRenderer line;

    [SerializeField] private LightRange lightRange;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * lightRange.lightRange / 10;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * lightRange.lightRange / 10;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }
}