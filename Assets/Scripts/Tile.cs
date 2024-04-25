using System;
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
    }

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
        if (turret != null || PlayerStats.Bits < PlayerStats.towerCost)
        {
            buildManager.SetTurretToBuildIsNull();
            return;
        }

        PlayerStats.AddBits(-PlayerStats.towerCost);
        Debug.Log("Turret Built! Bits left: " + PlayerStats.Bits);
        Vector3 newCalculatedTowerPosition = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, 0);
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, newCalculatedTowerPosition, Quaternion.identity);
        turretPrefabName = turret.name;
        buildManager.SetTurretToBuildIsNull();
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
