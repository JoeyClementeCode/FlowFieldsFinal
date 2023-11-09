using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Camera cam;

    public bool onCooldown = false;
    public float spawnCooldown = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !onCooldown)
        {
            CastLine();
        }
    }

    private void CastLine()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.transform.CompareTag("Ground"))
        {
            StartCoroutine(SpawnTower(hit));
        }
        if (hit.transform.CompareTag("Tower"))
        {
            StartCoroutine(SwitchPriority(hit));
        }
        else
        {
            Debug.Log("Not Anything?");
        }
    }

    private IEnumerator SpawnTower(RaycastHit hit)
    {
        onCooldown = true;

        if (GameManager.instance.economy.SpawnTower())
        {
            Instantiate(towerPrefab, new Vector3(hit.point.x, hit.point.y - 0.5f, hit.point.z), Quaternion.identity);
        }
        else
        {
            Debug.Log("Not Enough Currency");
        }
        
        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }

    private IEnumerator SwitchPriority(RaycastHit hit)
    {
        onCooldown = true;

        TowerAgent tower = hit.transform.GetComponent<TowerAgent>();
        tower.targetPriority++;
        
        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }
    
}
