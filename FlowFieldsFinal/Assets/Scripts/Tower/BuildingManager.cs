using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject phantomTower;
    [SerializeField] private Camera cam;


    public bool buildMode = false;
    public bool visualizationActive = false;
    public bool onCooldown = false;
    public float spawnCooldown = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            BuildMode();
        }
        
        if (buildMode)
        {
            Visualization();
            
            if (Input.GetKeyDown(KeyCode.C) && !onCooldown)
            {
                CastLine();
            }
        }
    }
    
    private void BuildMode()
    {
        if (buildMode)
        {
            buildMode = false;
            visualizationActive = false;
            Visualization();
        }
        else if (!buildMode)
        {
            buildMode = true;
            visualizationActive = true;
        }
    }

    private void Visualization()
    {
        if (visualizationActive)
        {
            
        }
        else if (!visualizationActive)
        {
            
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
        Debug.Log("Hitting Tower");

        TowerAgent tower = hit.transform.GetComponentInParent<TowerAgent>();

        switch (tower.targetPriority)
        {
            case TowerAgent.TowerTargetPriority.First:
                tower.targetPriority = TowerAgent.TowerTargetPriority.Close;
                break;
            case TowerAgent.TowerTargetPriority.Close:
                tower.targetPriority = TowerAgent.TowerTargetPriority.Strong;
                break;
            case TowerAgent.TowerTargetPriority.Strong:
                tower.targetPriority = TowerAgent.TowerTargetPriority.First;
                break;
        }
        
        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }
    
}
