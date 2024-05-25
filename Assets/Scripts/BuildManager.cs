using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{
    [SerializeField] List<Tile> listOfAllTiles;
    [SerializeField] private GameObject tempKineticTower, tempFireTower, tempFrostTower, tempShockTower, tempPulverizerTower, tempFlamethrowerTower, tempArcticTower;
    [SerializeField] private GameObject tempObsidianTower, tempShockFrostTower, tempFireShockTower, tempFrostKineticTower, tempShockKineticTower, tempTeslaTower, tempFireKineticTower;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject testDiamond;
    [SerializeField] private ParticleSystem placeTowerEffect;
    private GameObject placeEffectReference;
    //[SerializeField] private LayerMask turret, tile;
    private int rayCastDistance = 100;
    public static BuildManager instance;
    EnemySpawner enemySpawner;
    MergeManager mergeManager;
    [SerializeField] private GameObject audioManager;

    [Header("Turret prefabs")]
    public GameObject kineticTowerPrefab;
    public GameObject fireTowerPrefab;
    public GameObject frostTowerPrefab;
    public GameObject shockTowerPrefab;
   
    // Referens till shopen för vilken turret som ska byggas
    private GameObject turretToBuild;

    // Referens till tile script och objekt
    [Header("\n")]
    public Tile tileObjectScript;
    public GameObject tileObjectPrefab;

    // Refererar till tilen och tornet som blir klickad på
    private GameObject pressedTileObject;
    public GameObject selectedTurret;

    // Uppdaterar och sätter ny referens till tilen under muspekaren
    private GameObject tileUnderPointer;

    // Raycast specifikt för torn som inte går igenom objekt och raycast för tile som går igenom alla objekt
    public RaycastHit2D mouseTowerPointer; 
    RaycastHit2D[] mouseTilePointer = new RaycastHit2D[1];

    // Misc
    GameObject mouseUpTurret;

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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // Du kan ändra färgen efter behov
        Gizmos.DrawWireSphere(new Vector3(-0.92f, 6.3f, -1), 3f);
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
            mouseUpTurret = null;

            if (mouseTowerPointer.collider != null && mouseTowerPointer.collider.gameObject.layer == LayerMask.NameToLayer("turret"))
            {
                Debug.Log("trycker på turret");
                //Tornet som ska flyttas sparas i en referens
                selectedTurret = mouseTowerPointer.collider.gameObject;
                //Här kollas vad det är för torn och gör så att en sprite kopia av objektet syns som man kan dra runt innan man placerar tornet
                ActivateTemporaryTurretSprite();
                //Här sparas en referens till den tilen som muspekaren är över när man klickar på tornet.
                //Det gör man för att kunna rensa tilen vid lyckad flytt.
                pressedTileObject = tileUnderPointer;
            }
        }
    }

    public bool IsMouseUpOverTurret(GameObject turret)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (mouseTowerPointer.collider != null && mouseTowerPointer.collider.gameObject.layer == LayerMask.NameToLayer("turret"))
            {
                Debug.Log("släpper mus/touch på turret");
                mouseUpTurret = mouseTowerPointer.collider.gameObject;
                if (mouseUpTurret == turret)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public GameObject GetMouseUpTurret()
    {
        return mouseUpTurret;
    }

    private void OnReleasePressedTower()
    {
        bool mergeHappened = false;
        GameObject heldTurret = selectedTurret;
        GameObject targetTurret = tileObjectScript.GetTurret();
        //GameObject oldTile = tileUnderPointer;

        if (Input.GetMouseButtonUp(0) && selectedTurret != null)
        {
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
                    audioManager.GetComponent<AudioManager>().Play("ErrorShopButton");
                }
            }

            if (tileObjectScript.GetTurret() != null && selectedTurret != null)
            { 

                if (mergeManager.CanMerge(heldTurret, targetTurret))
                {
                    Debug.Log("Merge Successful!");
                    mergeHappened = true;

                    // Spara målplatsen för merge resultatet.
                    Vector3 mergeLocation = tileObjectScript.GetTurret().transform.position;

                    // Ta bort mergande turrets
                    Destroy(heldTurret);
                    Destroy(targetTurret);

                    // Ta bort referenser till mergande turrets
                    SetTurretToBuildIsNull();
                    deselectBuiltTurret();

                    // Skapa en kopia av merge resultatet, ställ in mottagande tilens tillstånd och flytta kopian till rätt plats
                    GameObject mergeResult = Instantiate(mergeManager.GetMergeResult());
                    mergeResult.transform.position = mergeLocation;
                    CreatePlaceTowerParticles(mergeLocation);
                }
                else
                {
                    Debug.Log("Merge Failed!");
                }

                deselectBuiltTurret();
                Debug.Log("deselect");
            }

            
        }

        if (mergeHappened) SetTurretToBuildIsNull();
        mergeHappened = false;
    }
    public bool isRaycastHittingTile()
    {
        if (mouseTilePointer[0].collider != null && mouseTilePointer[0].collider.gameObject.layer == LayerMask.NameToLayer("tile"))
        {
            // Debug.Log("Raycast träffar: " + mouseTilePointer[0].collider.gameObject.name);
            tileObjectScript = mouseTilePointer[0].collider.gameObject.GetComponent<Tile>();
            tileUnderPointer = mouseTilePointer[0].collider.gameObject;
            return true;
        }
        else
        {
            //Debug.Log("Raycast träffar inte tile");
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
        // Rensar referensen till det tornet som ska byggas från shoppen och deaktiverar den temporära spriten
        deactivateTempTurretSprites();
        turretToBuild = null;
    }

    public void deactivateTempTurretSprites()
    {
        tempKineticTower.SetActive(false);
        tempFireTower.SetActive(false);
        tempFrostTower.SetActive(false);
        tempShockTower.SetActive(false);
        tempPulverizerTower.SetActive(false);
        tempFlamethrowerTower.SetActive(false);
        tempArcticTower.SetActive(false);
        tempTeslaTower.SetActive(false);
        tempFrostKineticTower.SetActive(false);
        tempShockKineticTower.SetActive(false);
        tempFireKineticTower.SetActive(false);
        tempObsidianTower.SetActive(false);
        tempShockFrostTower.SetActive(false);
        tempFireShockTower.SetActive(false);
    }

    public void ActivateTemporaryTurretSprite()
    {
        //Baserat på vad det är för torn det är som blir klickat så activeras olika sprites
        if (turretToBuild == null)
        {
            Debug.Log("turretSelcted");
            if (selectedTurret.GetComponent<KineticTower>() != null)
            {
                Debug.Log("KineticTowerOnTileSelected");
                tempKineticTower.SetActive(true);
            }
            else if (selectedTurret.GetComponent<FireTower>() != null)
            {
                Debug.Log("FireTowerOnTileSelected");
                tempFireTower.SetActive(true);
            }
            else if (selectedTurret.GetComponent<FlamethrowerTower>() != null)
            {
                Debug.Log("FlamethrowerTowerOnTileSelected");
                tempFlamethrowerTower.SetActive(true);
            }
            else if (selectedTurret.GetComponent<PulverizerTower>() != null)
            {
                Debug.Log("PulverizerTowerOnTileSelected");
                tempPulverizerTower.SetActive(true);
            }
            else if (selectedTurret.GetComponent<FireKineticTower>() != null)
            {
                Debug.Log("FireKineticTowerOnTileSelected");
                tempFireKineticTower.SetActive(true);
            }
            else if(selectedTurret.GetComponent<FrostTower>() != null)
            {
                Debug.Log("FrostTowerOnTileSelected");
                tempFrostTower.SetActive(true);
            }
            else if(selectedTurret.GetComponent<ShockTower>() != null)
            {
                Debug.Log("ShockTowerOnTileSelected");
                tempShockTower.SetActive(true);
            }
            else if(selectedTurret.GetComponent<TeslaTower>() != null)
            {
                Debug.Log("TeslaTurretOnTileSelected");
                tempTeslaTower.SetActive(true);
            }
            else if(selectedTurret.GetComponent<ArcticTower>() != null)
            {
                Debug.Log("ArcticTurretOnTileSelected");
                tempArcticTower.SetActive(true);
            }
            else if(selectedTurret.GetComponent<ShockFrostTower>() != null)
            {
                Debug.Log("ShockFrostTowerOnTileSelected");
                tempShockFrostTower.SetActive(true);
            }
            else if(selectedTurret.GetComponent<ObsidianTower>() != null) 
            {
                Debug.Log("ObsidianTurretOnTileSelected");
                tempObsidianTower.SetActive(true);
            }
            else if(selectedTurret.GetComponent<FrostKineticTower>() != null)
            {
                Debug.Log("FrostKineticTowerTurretOnTileSelected");
                tempFrostKineticTower.SetActive(true);
            }
            else if(selectedTurret.GetComponent<ShockKineticTower>() != null)
            {
                Debug.Log("ShockKineticTowerTurretOnTileSelected");
                tempShockKineticTower.SetActive(true);
            }
            else if (selectedTurret.GetComponent<FireShockTower>() != null)
            {
                Debug.Log("FireShockTowerOnTileSelected");
                tempFireShockTower.SetActive(true);
            }
        }
    }
    public void deselectBuiltTurret()
    {
        //rensar referensen till det klickade tornet samt det temporära flyttspritsen blir inaktiva
        //audioManager.GetComponent<AudioManager>().Play("Error");
        selectedTurret = null;
        deactivateTempTurretSprites();
    }
    private void MoveTurret()
    {
        Debug.Log("flytta turret");

        // Här hämtar vi positionen av den tile som är under muspekaren
        Vector3 newCalculatedTowerPosition = new Vector3(tileObjectScript.transform.position.x, tileObjectScript.transform.position.y, 0);

        audioManager.GetComponent<AudioManager>().PlaySoundEffect("MoveTower");

        // Ta bort turrens referens från platsen den ska flyttas till
        if (tileUnderPointer.GetComponent<Tile>().GetTurret() != null)
        {
            tileUnderPointer.GetComponent<Tile>().SetTurretToNull();
        }

        // Flyttar tornet till den nya tilens plats och sätter in den nya turret referensen
        selectedTurret.transform.position = newCalculatedTowerPosition;
        tileUnderPointer.GetComponent<Tile>().SetTurret(selectedTurret);

        // Här rensas referensen från den gamla tilen (som sparades när man klickade på tornet som ska flytts) 
        if (pressedTileObject != null)
        {
            pressedTileObject.GetComponent<Tile>().SetTurretToNull();
        }

        // Rensar referensen för tornet som blev klickad på och deaktiverar den temporära turret spriten.
        deselectBuiltTurret();
    }

    public GameObject GetTileUnderPointer()
    {
        return tileUnderPointer;
    }

    public GameObject GetPressedTileObject()
    {
        return pressedTileObject;
    }

    public RaycastHit2D GetMouseTowerPointer()
    {
        return mouseTowerPointer;
    }
    private void CreatePlaceTowerParticles(Vector3 position)
    {
        ParticleSystem particleObject = Instantiate(placeTowerEffect, position, Quaternion.identity);
        placeEffectReference = particleObject.gameObject;
        if (placeEffectReference != null)
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
