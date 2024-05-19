using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour, IDataPersistence
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
    [SerializeField] private GameObject audioManager;
    [SerializeField] private ParticleSystem placeTowerEffect;
    private GameObject placeEffectReference;
    public GameObject currentTile;

    BuildManager buildManager;
    MergeManager mergeManager;
    public string turretPrefabName;
    private Vector3 turretPosition;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildManager = BuildManager.instance;
        mergeManager = MergeManager.instance;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && buildManager.isRaycastHittingTile() && currentTile != null && buildManager.GetTurretToBuild() != null
            || buildManager.IsMouseUpOverTurret(turret) && buildManager.GetTurretToBuild() != null)
        {
            if (PlayerStats.Bits < GetTowerCost(buildManager.GetTurretToBuild()))
            {
                ClearSelection();
                return;
            }

            PlaceTurret();
        }
        else if (Input.GetMouseButtonUp(0) && !buildManager.isRaycastHittingTile() && buildManager.GetTurretToBuild() != null)
        {
            ClearSelection();
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
                turretPrefabName = turret.name;
                //Debug.Log("Turret registered.");
            }
        }
        else
        {
            turret = null;
            turretPrefabName = null;
            //Debug.Log("Turret reference nullified.");
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, 0.5f);
    //}

    public void LoadData(GameData data)
    {
        if (data.turretPrefabNames.TryGetValue(id, out string prefabName) && data.turretPositions.TryGetValue(id, out Vector3 position))
        {
            Debug.Log("Laddar turret/tile data");
            turretPosition = position;
            turretPrefabName = prefabName;
            string fixedPrefabName = turretPrefabName.Replace("(Clone)", "");

            string path = "TurretTypes/" + fixedPrefabName;
            Debug.Log(path);
            GameObject turretPrefab = Resources.Load<GameObject>(path);
            if (turretPrefab != null)
            {
                Debug.Log("Spawnar sparade turrets vid uppladning");
                SetTurret(turret);
                turret = Instantiate(turretPrefab, turretPosition, Quaternion.identity);
            }
        }
    }
    public void SaveData(ref GameData data)
    {
        if (turret != null && !string.IsNullOrEmpty(turretPrefabName))
        {
            Debug.Log("Sparar turret/tile data");
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
    }
    public void PlaceTurret()
    {
        if (PlayerStats.Bits < GetTowerCost(buildManager.GetTurretToBuild()))
        {
            audioManager.GetComponent<AudioManager>().Play("Error");
            ClearSelection();
            return;
        }

        if (turret != null)
        {
            Debug.Log("Trying to merge...");

            // Hämta selected turret innan det deselectas på något vis. Ny variabel i build managern?
            GameObject heldTurret = buildManager.GetTurretToBuild();
            GameObject targetTurret = turret;
            Vector3 mergeLocation = transform.position;
            ClearSelection();

            if (mergeManager.CanMerge(heldTurret, targetTurret))
            {
                PlayerStats.AddBits(-GetTowerCost(heldTurret));
                Debug.Log("Merge from shop success! Bits left: " + PlayerStats.Bits);
                Destroy(targetTurret);
                GameObject mergeResult = Instantiate(mergeManager.GetMergeResult());
                mergeResult.transform.position = mergeLocation;
                CreatePlaceTowerParticles(mergeLocation);
            }
            else
            {
                Debug.Log("Merge Failed!");
            }

            return;
        }
        else
        {
            PlayerStats.AddBits(-GetTowerCost(buildManager.GetTurretToBuild()));
            Debug.Log("Turret Built! Bits left: " + PlayerStats.Bits);
            Vector3 newCalculatedTowerPosition = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, 0);
            GameObject turretToBuild = buildManager.GetTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild, newCalculatedTowerPosition, Quaternion.identity);
            CreatePlaceTowerParticles(newCalculatedTowerPosition);
            audioManager.GetComponent<AudioManager>().PlaySoundEffect("BuyTower");
            audioManager.GetComponent<AudioManager>().PlaySoundEffect("PlaceTower");
            
            //turretPrefabName = turret.name;
            ClearSelection();
        }
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
    public void ClearSelection()
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
    private void CreatePlaceTowerParticles(Vector3 position)
    {
        ParticleSystem particleObject = Instantiate(placeTowerEffect, position, Quaternion.identity);
        placeEffectReference = particleObject.gameObject;
        if(placeEffectReference != null)
        {
            Invoke("DestroyPlaceTowerParticles", 3f);
        }

    }
    private void DestroyPlaceTowerParticles()
    {
        if (placeEffectReference != null)
        {
            Destroy(placeEffectReference);
        }
    }



}
