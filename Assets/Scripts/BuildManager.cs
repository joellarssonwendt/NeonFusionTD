using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildManager : MonoBehaviour
{
    [SerializeField] List<Tile> listOfAllTiles;
    [SerializeField] private GameObject tempNormalTurret, tempFireTurret, tempIceTurret, tempLightningTurret, tempSuperNormalTurret, tempSuperFireTurret;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject testDiamond;
    //[SerializeField] private LayerMask turret, tile;
    private int rayCastDistance = 100;
    public static BuildManager instance;
    EnemySpawner enemySpawner;
    public GameObject standardTurretPrefab;
    public GameObject fireTurretPrefab;
    public GameObject superStandardTurretPrefab;
    public GameObject superFireTurretPrefab;
    private GameObject turretToBuild;
    public Tile tileObjectScript;
    public GameObject tileObjectPrefab;
    private GameObject pressedTileObject;
    private GameObject tileUnderPointer;
    public GameObject selectedTurret;
    public RaycastHit2D mouseTowerPointer;
    RaycastHit2D[] mouseTilePointer = new RaycastHit2D[1];
    MergeManager mergeManager;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en BuildManager");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        enemySpawner = EnemySpawner.instance;
        tileObjectScript = tileObjectPrefab.GetComponent<Tile>();
        mergeManager = MergeManager.instance;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            OnPointerRaycast(Input.mousePosition);
        }
        else if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                OnPointerRaycast(touch.position);
            }
        }
        OnPressTower();
        OnReleasePressedTower();
        isRaycastHittingTile();

        if (tileObjectScript != null)
        {
            if (tileObjectScript.GetTurret() != null)
            {
                //Debug.Log("get Turret är inte null");
            }
            else
            {
                //Debug.Log("get Turret == null");
            }
        }
    }

    private void OnPointerRaycast(Vector3 screenPosition)
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = -3;
        Debug.DrawRay(mouseWorldPosition, mainCamera.transform.forward * rayCastDistance, Color.red);
        mouseTowerPointer = Physics2D.Raycast(mouseWorldPosition, mainCamera.transform.forward, rayCastDistance, LayerMask.GetMask("turret"));
        mouseTilePointer[0] = new RaycastHit2D();
        Physics2D.RaycastNonAlloc(mouseWorldPosition, mainCamera.transform.forward, mouseTilePointer, rayCastDistance, LayerMask.GetMask("tile"));
    }

    private void OnPressTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseTowerPointer.collider != null && mouseTowerPointer.collider.gameObject.layer == LayerMask.NameToLayer("turret"))
            {
                Debug.Log("trycker på turret");
                selectedTurret = mouseTowerPointer.collider.gameObject;
                selectBuiltTurret();
                pressedTileObject = tileUnderPointer;
            }
        }
    }
    private void OnReleasePressedTower()
    {
        if (Input.GetMouseButtonUp(0) && selectedTurret != null)
        {
            if (tileObjectScript.GetTurret() != null)
            {
                if (mergeManager.CanMerge(selectedTurret, tileObjectScript.GetTurret()))
                {
                    if (pressedTileObject == tileUnderPointer)
                    {
                        Debug.Log("Can't merge with itself!");
                        return;
                    }

                    Debug.Log("Merge Successful!");

                    // Spara målplatsen för merge resultatet.
                    Vector3 mergeLocation = tileObjectScript.GetTurret().transform.position;

                    // Nolställ selectedTurrets tile tillstånd
                    pressedTileObject.GetComponent<Tile>().SetTurretToNull();

                    // Ta bort mergande turrets
                    Destroy(selectedTurret);
                    Destroy(tileObjectScript.GetTurret());

                    // Skapa en kopia av merge resultatet, ställ in mottagande tilens tillstånd och flytta kopian till rätt plats
                    GameObject mergeResult = Instantiate(mergeManager.GetMergeResult());
                    tileUnderPointer.GetComponent<Tile>().SetTurret(mergeResult);
                    mergeResult.transform.position = mergeLocation;
                }
                else
                {
                    Debug.Log("Merge Failed!");
                }

                deselectBuiltTurret();
                Debug.Log("deselect");
            }

            if (tileObjectScript.GetTurret() == null)
            {
                if (isRaycastHittingTile() && !enemySpawner.activeRoundPlaying)
                {
                    //här flyttas turreten till tilen som musen är över
                    MoveTurret();
                }
                else
                {
                    //här deselectas turreten samt Temp sprites försvinner för att man missar rutan.
                    deselectBuiltTurret();
                    Debug.Log("deselect");
                }
            }
        }
    }
    public bool isRaycastHittingTile()
    {
        if (mouseTilePointer[0].collider != null && mouseTilePointer[0].collider.gameObject.layer == LayerMask.NameToLayer("tile"))
        {
            Debug.Log("Raycast träffar: " + mouseTilePointer[0].collider.gameObject.name);
            tileObjectScript = mouseTilePointer[0].collider.gameObject.GetComponent<Tile>();
            tileUnderPointer = mouseTilePointer[0].collider.gameObject;
            return true;
        }
        else
        {
            Debug.Log("Raycast träffar inte tile");
            return false;
        }
    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
    public void SetTurretToBuildIsNull()
    {
        deactivateTempTurretSprites();
        turretToBuild = null;
    }

    public void deactivateTempTurretSprites()
    {
        tempNormalTurret.SetActive(false);
        tempFireTurret.SetActive(false);
        // tempIceTurret.SetActive(false);
        // tempLightningTurret.SetActive(false);
        tempSuperNormalTurret.SetActive(false);
        tempSuperFireTurret.SetActive(false);
    }

    public void selectBuiltTurret()
    {
        if (turretToBuild == null)
        {
            Debug.Log("turretSelcted");
            if (selectedTurret.GetComponent<NormalTurret>() != null)
            {
                Debug.Log("normalTurretOnTileSelected");
                tempNormalTurret.SetActive(true);
            }
            else if (selectedTurret.GetComponent<FireTurret>() != null)
            {
                Debug.Log("FireTurretOnTileSelected");
                tempFireTurret.SetActive(true);
            }
            else if (selectedTurret.GetComponent<SuperFireTurret>() != null)
            {
                Debug.Log("SuperFireTurretOnTileSelected");
                tempSuperFireTurret.SetActive(true);
            }
            else if (selectedTurret.GetComponent<SuperNormalTurret>() != null)
            {
                Debug.Log("SuperNormalTurretOnTileSelected");
                tempSuperNormalTurret.SetActive(true);
            }
        }
    }
    public void deselectBuiltTurret()
    {
        selectedTurret = null;
        deactivateTempTurretSprites();
    }
    private void MoveTurret()
    {
        Debug.Log("flytta turret");
        Vector3 newCalculatedTowerPosition = new Vector3(tileObjectScript.transform.position.x, tileObjectScript.transform.position.y, 0);

        // Ta bort turrens referens från den tidigare tilen
        if (tileUnderPointer.GetComponent<Tile>().GetTurret() != null)
        {
            tileUnderPointer.GetComponent<Tile>().SetTurretToNull();
        }

        selectedTurret.transform.position = newCalculatedTowerPosition;
        tileUnderPointer.GetComponent<Tile>().SetTurret(selectedTurret);
        pressedTileObject.GetComponent<Tile>().SetTurretToNull();
        deselectBuiltTurret();
    }
}
