using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldQuantization : MonoBehaviour
{
    static Vector3Int Quantize(Vector3 vec, float resolution = 1)
    {
        return new Vector3Int(
            Mathf.FloorToInt(vec.x / resolution), 
            Mathf.FloorToInt(vec.y / resolution),
            Mathf.FloorToInt(vec.z / resolution));
    }
}
