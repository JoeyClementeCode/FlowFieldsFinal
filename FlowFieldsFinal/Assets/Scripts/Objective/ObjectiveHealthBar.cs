using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveHealthBar : MonoBehaviour
{
    // value
    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;
    //[SerializeField] private Transform target;
    //[SerializeField] private Vector3 offset;
    

    public void UpdateHealthbar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    private void Update()
    {
        transform.rotation = cam.transform.rotation;
        //transform.position = target.position + offset;
    }
}
