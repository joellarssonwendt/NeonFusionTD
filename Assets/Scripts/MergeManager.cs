using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Cache
    public static MergeManager instance;
    BuildManager buildManager;

    public static Action<bool> OnMergeAction;
    public Dictionary<string, bool> MergeDictionary = new Dictionary<string, bool>(); //TDname

    private GameObject mergeResult = null;

    // Upgrade Prefabs
    [SerializeField] private GameObject normalNormal, fireFire, normalFire, normalIce, normalLightning, iceIce, iceLightning, iceFire, lightningFire, lightningLightning;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en MergeManager!");
            return;
        }
        instance = this;
    }

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public bool CanMerge(GameObject heldTurret, GameObject tileTurret)
    {
        if (heldTurret == tileTurret)
        {
            buildManager.deselectBuiltTurret();
            Debug.Log("Can't merge with itself!");
            return false;
        }

        if (PlayerStats.Crystals >= 10)
        {
            //combo 1
            if (heldTurret.CompareTag("normal") && tileTurret.CompareTag("normal"))
            {
                mergeResult = normalNormal;
                if (!MergeDictionary.ContainsKey("pulverizer"))
                {
                    MergeDictionary["pulverizer"] = true;
                    OnMergeAction?.Invoke(true);
                } else {
                    MergeDictionary["pulverizer"] = true;
                    OnMergeAction?.Invoke(false);
                }
                
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 2
            if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("fire"))
            {
                mergeResult = fireFire;
                if (!MergeDictionary.ContainsKey("flamethrower"))
                {
                    MergeDictionary["flamethrower"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["flamethrower"] = true;
                    OnMergeAction?.Invoke(false);
                }
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 3
            if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("normal") || (heldTurret.CompareTag("normal") && tileTurret.CompareTag("fire")))
            {
                mergeResult = normalFire;
                if (!MergeDictionary.ContainsKey("fireburst"))
                {
                    MergeDictionary["fireburst"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["fireburst"] = true;
                    OnMergeAction?.Invoke(false);
                }
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 4
            if (heldTurret.CompareTag("ice") && tileTurret.CompareTag("normal") || (heldTurret.CompareTag("normal") && tileTurret.CompareTag("ice")))
            {
                mergeResult = normalIce;
                if (!MergeDictionary.ContainsKey("frostbite"))
                {
                    MergeDictionary["frostbite"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["frostbite"] = true;
                    OnMergeAction?.Invoke(false);
                }
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 5
            if (heldTurret.CompareTag("lightning") && tileTurret.CompareTag("normal") || (heldTurret.CompareTag("normal") && tileTurret.CompareTag("lightning")))
            {
                mergeResult = normalLightning;
                if (!MergeDictionary.ContainsKey("shockwave"))
                {
                    MergeDictionary["shockwave"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["shockwave"] = true;
                    OnMergeAction?.Invoke(false);
                }
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 6
            if (heldTurret.CompareTag("ice") && tileTurret.CompareTag("ice"))
            {
                mergeResult = iceIce;
                if (!MergeDictionary.ContainsKey("arctic"))
                {
                    MergeDictionary["arctic"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["arctic"] = true;
                    OnMergeAction?.Invoke(false);
                }

                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 7
            if (heldTurret.CompareTag("ice") && tileTurret.CompareTag("lightning") || (heldTurret.CompareTag("lightning") && tileTurret.CompareTag("ice")))
            {
                mergeResult = iceLightning;
                if (!MergeDictionary.ContainsKey("frostshock"))
                {
                    MergeDictionary["frostshock"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["frostshock"] = true;
                    OnMergeAction?.Invoke(false);
                }

                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 8
            if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("ice") || (heldTurret.CompareTag("ice") && tileTurret.CompareTag("fire")))
            {
                mergeResult = iceFire;
                if (!MergeDictionary.ContainsKey("obsidian"))
                {
                    MergeDictionary["obsidian"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["obsidian"] = true;
                    OnMergeAction?.Invoke(false);
                }

                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 9
            if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("lightning") || (heldTurret.CompareTag("lightning") && tileTurret.CompareTag("fire")))
            {
                mergeResult = lightningFire;
                if (!MergeDictionary.ContainsKey("embersurge"))
                {
                    MergeDictionary["embersurge"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["embersurge"] = true;
                    OnMergeAction?.Invoke(false);
                }
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            //combo 10
            if (heldTurret.CompareTag("lightning") && tileTurret.CompareTag("lightning"))
            {
                mergeResult = lightningLightning;
                if (!MergeDictionary.ContainsKey("tesla"))
                {
                    MergeDictionary["tesla"] = true;
                    OnMergeAction?.Invoke(true);
                }
                else
                {
                    MergeDictionary["tesla"] = true;
                    OnMergeAction?.Invoke(false);
                }

                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                //Merge();
                return true;
            }
            return false;
        }
        Debug.Log("Not enough bits to merge");
        return false;
    }

    //private void Merge()
    //{
    //    GameObject heldTurret = buildManager.selectedTurret;
    //    GameObject targetTurret = buildManager.tileObjectScript.GetTurret();
    //    GameObject oldTile = buildManager.GetTileUnderPointer();

    //    Debug.Log("Merge Successful!");

    //    // Spara målplatsen för merge resultatet.
    //    Vector3 mergeLocation = buildManager.GetMouseTowerPointer().transform.position;

    //    // Nolställ selectedTurrets tile tillstånd
    //    oldTile.GetComponent<Tile>().SetTurretToNull();

    //    // Ta bort mergande turrets
    //    Destroy(heldTurret);
    //    Destroy(targetTurret);

    //    // Ta bort referenser till mergande turrets
    //    buildManager.SetTurretToBuildIsNull();
    //    buildManager.deselectBuiltTurret();

    //    // Skapa en kopia av merge resultatet, ställ in mottagande tilens tillstånd och flytta kopian till rätt plats
    //    Instantiate(mergeResult);
    //    buildManager.GetTileUnderPointer().GetComponent<Tile>().SetTurret(mergeResult);
    //    mergeResult.transform.position = mergeLocation;
    //}

    public GameObject GetMergeResult()
    {
        return mergeResult;
    }


}
