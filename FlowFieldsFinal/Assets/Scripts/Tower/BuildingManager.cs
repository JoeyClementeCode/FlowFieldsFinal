using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Camera cam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CastLine();
        }
    }

    private void CastLine()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);// = Physics.Raycast(cam.transform.position, cam.transform.forward, 50.0f);

        if (hit.transform.CompareTag("Ground"))
        {
            Instantiate(towerPrefab, new Vector3(hit.point.x, hit.point.y - 0.5f, hit.point.z), Quaternion.identity);
        }
        else
        {
            Debug.Log("Not Ground");
        }
    }
    
}
