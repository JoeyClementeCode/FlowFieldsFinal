using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Public Variables")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject phantomTower;
    [SerializeField] private Camera cam;
    public float spawnCooldown = 2.0f;
    public float rayDistance = 10.0f;
    
    private GameObject phantom;

    [Header("Checks")]
    public bool buildMode = false;
    public bool onCooldown = false;
    public bool isPlaceable = false;

    [Header("Tower Checks")]
    public bool isTower = false;
    public TowerAgent currentTower;

    [Header("Pre-Visualization")]
    [ColorUsage(true, true)]
    public Color placeableColor;
    [ColorUsage(true, true)]
    public Color notPlaceableColor;
    public Material phantomMaterial;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            BuildMode();
        }
        
        if (buildMode)
        {
            Building();
        }
    }
    
    private void BuildMode()
    {
        if (buildMode)
        {
            buildMode = false;

            if (phantom != null)
            {
                Destroy(phantom);
                phantom = null;
            }
        }
        else if (!buildMode)
        {
            
            buildMode = true;
        }
    }

    private void Building()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, rayDistance);
        Debug.DrawRay(transform.position, ray.direction, Color.yellow);

        if (hit.collider != null)
        {
            if (phantom == null)
            {
                phantom = Instantiate(phantomTower, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            }

            phantom.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            Checks(hit);
            Inputs(hit);
        }
    }

    private void Checks(RaycastHit hit)
    {
        
        if (hit.transform.CompareTag("Ground"))
        {
            isTower = false;
            isPlaceable = true;
            Debug.Log("Ground");
            phantomMaterial.SetColor("_PhantomColor", placeableColor);
        }
        else if (hit.transform.CompareTag("Tower"))
        {
            isTower = true;
            isPlaceable = false;
            Debug.Log("Tower");
            phantomMaterial.SetColor("_PhantomColor", notPlaceableColor);
            
        }
        else
        {
            isTower = false;
            isPlaceable = false;
            phantomMaterial.SetColor("_PhantomColor", notPlaceableColor);
            Debug.Log("Not Anything?");
        }

        if (hit.transform == null)
        {
            isPlaceable = false;
            isTower = false;
            phantomMaterial.SetColor("_PhantomColor", notPlaceableColor);
            Debug.Log("Null");
        }
    }

    private void Inputs(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.C) && !onCooldown && isPlaceable)
        {
            StartCoroutine(SpawnTower(hit));
        }

        if (Input.GetKeyDown(KeyCode.G) && !onCooldown && isTower)
        {
            currentTower = hit.transform.GetComponent<TowerAgent>();
            StartCoroutine(ActivateTower());
        }
    }

    private IEnumerator SpawnTower(RaycastHit hit)
    {
        onCooldown = true;

        if (GameManager.instance.economy.SpawnTower())
        {
            Instantiate(towerPrefab, new Vector3(hit.point.x, hit.point.y,  hit.point.z), Quaternion.identity);
        }
        else
        {
            Debug.Log("Not Enough Currency");
        }
        
        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }

    private IEnumerator ActivateTower()
    {
        onCooldown = true;
        Debug.Log("Tower UI Activated");

        GameManager.instance.ui.TowerUI(currentTower);

        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }

    public void Switch()
    {
        StartCoroutine(SwitchPriority());
    }
    
    private IEnumerator SwitchPriority()
    {
        onCooldown = true;
        Debug.Log("Switch");

        switch (currentTower.targetPriority)
        {
            case TowerAgent.TowerTargetPriority.First:
                currentTower.targetPriority = TowerAgent.TowerTargetPriority.Close;
                break;
            case TowerAgent.TowerTargetPriority.Close:
                currentTower.targetPriority = TowerAgent.TowerTargetPriority.Strong;
                break;
            case TowerAgent.TowerTargetPriority.Strong:
                currentTower.targetPriority = TowerAgent.TowerTargetPriority.First;
                break;
        }
        
        GameManager.instance.ui.UpdateTowerUI();
        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }
    
}
