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
    MergeManager mergeManager;

    // Turret prefabs
    public GameObject standardTurretPrefab;
    public GameObject fireTurretPrefab;
    public GameObject superStandardTurretPrefab;
    public GameObject superFireTurretPrefab;

    // Referens till shopen f�r vilken turret som ska byggas
    private GameObject turretToBuild;

    // Referens till tile script och objekt
    public Tile tileObjectScript;
    public GameObject tileObjectPrefab;

    // Refererar till tilen och tornet som blir klickad p�
    private GameObject pressedTileObject;
    public GameObject selectedTurret;

    // Uppdaterar och s�tter ny referens till tilen under muspekaren
    private GameObject tileUnderPointer;

    // Raycast specifikt f�r torn som inte g�r igenom objekt och raycast f�r tile som g�r igenom alla objekt
    public RaycastHit2D mouseTowerPointer; 
    RaycastHit2D[] mouseTilePointer = new RaycastHit2D[1];

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
                //Debug.Log("get Turret �r inte null");
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
                Debug.Log("trycker p� turret");
                //Tornet som ska flyttas sparas i en referens
                selectedTurret = mouseTowerPointer.collider.gameObject;
                //H�r kollas vad det �r f�r torn och g�r s� att en sprite kopia av objektet syns som man kan dra runt innan man placerar tornet
                ActivateTemporaryTurretSprite();
                //H�r sparas en referens till den tilen som muspekaren �r �ver n�r man klickar p� tornet.
                //Det g�r man f�r att kunna rensa tilen vid lyckad flytt.
                pressedTileObject = tileUnderPointer;
            }
        }
    }
    private void OnReleasePressedTower()
    {
        bool mergeHappened = false;

        if (Input.GetMouseButtonUp(0) && selectedTurret != null)
        {
            if (tileObjectScript.GetTurret() != null)
            {

                if (mergeManager.CanMerge(selectedTurret, tileObjectScript.GetTurret()))
                {
                    if (selectedTurret == tileObjectScript.GetTurret())
                    {
                        Debug.Log("Can't merge with itself!");
                        deselectBuiltTurret();
                        return;
                    }

                    Debug.Log("Merge Successful!");
                    mergeHappened = true;

                    // Spara m�lplatsen f�r merge resultatet.
                    Vector3 mergeLocation = tileObjectScript.GetTurret().transform.position;

                    // Nolst�ll selectedTurrets tile tillst�nd
                    pressedTileObject.GetComponent<Tile>().SetTurretToNull();

                    // Ta bort mergande turrets
                    Destroy(selectedTurret);
                    Destroy(tileObjectScript.GetTurret());

                    // Ta bort referenser till mergande turrets
                    SetTurretToBuildIsNull();
                    deselectBuiltTurret();

                    // Skapa en kopia av merge resultatet, st�ll in mottagande tilens tillst�nd och flytta kopian till r�tt plats
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
                if (isRaycastHittingTile() && !enemySpawner.activeRoundPlaying && !mergeHappened)
                {
                    //h�r flyttas turreten till tilen som musen �r �ver
                    MoveTurret();
                }
                else
                {
                    //h�r deselectas turreten samt Temp sprites f�rsvinner f�r att man missar rutan.
                    deselectBuiltTurret();
                    Debug.Log("deselect");
                }
            }
        }

        if (mergeHappened) SetTurretToBuildIsNull();
        mergeHappened = false;
    }
    public bool isRaycastHittingTile()
    {
        if (mouseTilePointer[0].collider != null && mouseTilePointer[0].collider.gameObject.layer == LayerMask.NameToLayer("tile"))
        {
            // Debug.Log("Raycast tr�ffar: " + mouseTilePointer[0].collider.gameObject.name);
            tileObjectScript = mouseTilePointer[0].collider.gameObject.GetComponent<Tile>();
            tileUnderPointer = mouseTilePointer[0].collider.gameObject;
            return true;
        }
        else
        {
            //Debug.Log("Raycast tr�ffar inte tile");
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
        // Rensar referensen till det tornet som ska byggas fr�n shoppen och deaktiverar den tempor�ra spriten
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

    public void ActivateTemporaryTurretSprite()
    {
        //Baserat p� vad det �r f�r torn det �r som blir klickat s� activeras olika sprites
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
        //rensar referensen till det klickade tornet samt det tempor�ra flyttspritsen blir inaktiva
        selectedTurret = null;
        deactivateTempTurretSprites();
    }
    private void MoveTurret()
    {
        Debug.Log("flytta turret");

        // H�r h�mtar vi positionen av den tile som �r under muspekaren
        Vector3 newCalculatedTowerPosition = new Vector3(tileObjectScript.transform.position.x, tileObjectScript.transform.position.y, 0);

        // Ta bort turrens referens fr�n platsen den ska flyttas till
        if (tileUnderPointer.GetComponent<Tile>().GetTurret() != null)
        {
            tileUnderPointer.GetComponent<Tile>().SetTurretToNull();
        }

        // Flyttar tornet till den nya tilens plats och s�tter in den nya turret referensen
        selectedTurret.transform.position = newCalculatedTowerPosition;
        tileUnderPointer.GetComponent<Tile>().SetTurret(selectedTurret);

        // H�r rensas referensen fr�n den gamla tilen (som sparades n�r man klickade p� tornet som ska flytts) 
        pressedTileObject.GetComponent<Tile>().SetTurretToNull();

        // Rensar referensen f�r tornet som blev klickad p� och deaktiverar den tempor�ra turret spriten. 
        deselectBuiltTurret();
    }
}
