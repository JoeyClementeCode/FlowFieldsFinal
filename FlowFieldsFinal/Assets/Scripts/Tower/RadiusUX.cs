using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(LineRenderer))]
public class RadiusUX : MonoBehaviour
{
    [Range(0, 50)] public int segments = 50;
    public Vector2 radius;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        UpdatePoints();
    }

    void UpdatePoints()
    {
        float x, y, z;
        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius.x;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius.y;
            
            line.SetPosition(i, new Vector3(x, 0, y));

            angle += 360f / segments;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
