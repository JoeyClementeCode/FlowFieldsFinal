using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCrystal : MonoBehaviour
{
    public float rotationSpeed = 1f;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
