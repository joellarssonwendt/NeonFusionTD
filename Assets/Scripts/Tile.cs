using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tile : MonoBehaviour//, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generera guid för referens till enskilt gameobject")]
    private void GenerateGuide()
    {
        id = System.Guid.NewGuid().ToString();
    }
    private SpriteRenderer spriteRenderer;
    private Color newGray = new Color(20, 20, 20, 1);
    private Color newLightGray = new Color(54, 54, 54, 1);
    public bool isOverATile = false;
    [SerializeField] private GameObject turret;
    [SerializeField] private LayerMask turretLayer;
    public GameObject currentTile;

    BuildManager buildManager;
    public string turretPrefabName;
    private Vector3 turretPosition;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildManager = BuildManager.instance;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && buildManager.isRaycastHittingTile() && currentTile != null && buildManager.GetTurretToBuild() != null)
        {
            PlaceTurret();
        }
        else if (Input.GetMouseButtonUp(0) && !buildManager.isRaycastHittingTile() && buildManager.GetTurretToBuild() != null)
        {
            RemoveMisplacedTurret();
        }

        CheckTurret();
    }

    private void CheckTurret()
    {
        Collider2D[] checkArea = Physics2D.OverlapCircleAll(transform.position, 0.5f, turretLayer);

        if (checkArea.Length > 0)
        {
            //Debug.Log("Turret found.");
            for (int i = 0; i < checkArea.Length; i++)
            {
                turret = checkArea[i].gameObject;
                //Debug.Log("Turret registered.");
            }
        }
        else
        {
            turret = null;
            //Debug.Log("Turret reference nullified.");
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, 0.5f);
    //}

    /*public void LoadData(GameData data)
    {
        if (data.turretPrefabNames.TryGetValue(id, out string prefabName) && data.turretPositions.TryGetValue(id, out Vector3 position))
        {
            turretPrefabName = prefabName;
            turretPosition = position;

            string path = "TurretTypes/" + turretPrefabName;
            GameObject turretPrefab = Resources.Load<GameObject>(path);
            if (turretPrefab != null)
            {
                turret = Instantiate(turretPrefab, turretPosition, Quaternion.identity);
            }
        }
    }
    public void SaveData(ref GameData data)
    {
        if (turret != null && !string.IsNullOrEmpty(turretPrefabName))
        {
            // Spara namnet p  turreten prefab och positionen där den  är placerad
            data.turretPrefabNames[id] = turretPrefabName;
            data.turretPositions[id] = turret.transform.position;
        }
        else
        {
            // Om turreten  är null, ta bort den från dictionaryn
            data.turretPrefabNames.Remove(id);
            data.turretPositions.Remove(id);
        }
    }*/
    public void PlaceTurret()
    {
        if (turret != null || PlayerStats.Bits < GetTowerCost(buildManager.GetTurretToBuild()))
        {
            buildManager.SetTurretToBuildIsNull();
            return;
        }
        
        PlayerStats.AddBits(-GetTowerCost(buildManager.GetTurretToBuild()));
        Debug.Log("Turret Built! Bits left: " + PlayerStats.Bits);
        Vector3 newCalculatedTowerPosition = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, 0);
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, newCalculatedTowerPosition, Quaternion.identity);
        turretPrefabName = turret.name;
        buildManager.SetTurretToBuildIsNull();
    }

    private int GetTowerCost(GameObject turret)
    {
        if(turret.GetComponent<IceTower>() != null)
        {
            return PlayerStats.iceTowerCost;
        }
        else if(turret.GetComponent<FireTurret>() != null)
        {
            return PlayerStats.fireTowerCost;
        }
        else if(turret.GetComponent<LightningTower>() != null)
        {
            return PlayerStats.lightningTowerCost;
        }
        else
        {
            return PlayerStats.normalTowerCost;
        }
    }
    public void RemoveMisplacedTurret()
    {
        buildManager.SetTurretToBuildIsNull();
        return;
    }
    private void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() != null)
        {
            // spriteRenderer.color = newLightGray;
        }
        isOverATile = true;
        currentTile = gameObject;
    }
    private void OnMouseExit()
    {
        // spriteRenderer.color = newGray;
        currentTile = null;
        isOverATile = false;
    }
    public GameObject GetTurret()
    {
        return turret;
    }
    public void SetTurret(GameObject turret)
    {
        this.turret = turret;
    }

    public string GetTurretTag()
    {
        return turret.tag;
    }
    public void SetTurretToNull()
    {
        turret = null;
        turretPrefabName = null;
    }
}
