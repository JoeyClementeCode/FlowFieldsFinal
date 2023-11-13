using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject phantomTower;
    private GameObject phantom;
    [SerializeField] private Camera cam;

    


    public bool buildMode = false;
    public bool onCooldown = false;
    public bool isPlaceable = false;
    public float spawnCooldown = 2.0f;

    public Color placeableColor;
    public Color notPlaceableColor;
    public Material phantomMaterial;
    public Shader phantomShader;

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
        Physics.Raycast(ray, out hit, 5);
        Debug.DrawRay(transform.position, ray.direction, Color.yellow);

        if (hit.collider != null)
        {
            if (phantom == null)
            {
                phantom = Instantiate(phantomTower, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            }

            phantom.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            Checks(hit);
        
            if (Input.GetKeyDown(KeyCode.C) && !onCooldown && isPlaceable)
            {
                StartCoroutine(SpawnTower(hit));
            }
        }
    }

    private void Checks(RaycastHit hit)
    {
        
        if (hit.transform.CompareTag("Ground"))
        {
            isPlaceable = true;
            Debug.Log("Ground");
            phantomMaterial.SetColor("_PhantomColor", placeableColor);
            //StartCoroutine(SpawnTower(hit));
        }
        else
        {
            isPlaceable = false;
            phantomMaterial.SetColor("_PhantomColor", notPlaceableColor);
            Debug.Log("Not Anything?");
        }

        if (hit.transform == null)
        {
            isPlaceable = false;
            Debug.Log("Null");
        }
        
        if (hit.transform.CompareTag("Tower"))
        {
            //StartCoroutine(SwitchPriority(hit));
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
