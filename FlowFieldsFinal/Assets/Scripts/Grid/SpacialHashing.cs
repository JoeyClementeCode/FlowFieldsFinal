using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Dictionary<Vector3Int, HashSet<GameObject>> hashmap;
    private Dictionary<Vector3Int, Vector3Int> came_from;

    public static Vector3Int Quantize(Vector3 vec, float resolution = 1)
    {
        return new Vector3Int(
            Mathf.FloorToInt(vec.x / resolution), 
            Mathf.FloorToInt(vec.y / resolution),
            Mathf.FloorToInt(vec.z / resolution));
    }

    void Move(GameObject go, Vector3 previous, Vector3 current)
    {
        Vector3Int previousBucket = Quantize(previous);
        Vector3Int currentBucket = Quantize(current);

        if (previousBucket != currentBucket)
        {
            hashmap[previousBucket].Remove(go);
            hashmap[currentBucket].Add(go);
            
            // renegerate our flowfield aka. camefrom map
        }

    }
}
