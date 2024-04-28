using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedChecker : MonoBehaviour
{
    MergeManager mergeManager;
    ReadOnlyCollection<bool> unlockedList;

    public Image pulverizerT2Siluette; //Kinetic + Kinetic
    public Image flamethrowerT2Siluette; //Fire + Fire
    public Image fireburstT2Siluette; //Kinetic + Fire
    public Image frostbiteT2Siluette; // Kinetic + Ice
    public Image shockwaveT2Siluette; // Kinetic + Lightning
    public Image arcticT2Siluette; //Ice + Ice
    public Image frostshockT2Siluette; //Ice + Lightning
    public Image obsidianT2Siluette; //Ice + Fire
    public Image embersurgeT2Siluette; //Fire + Lightning
    public Image teslaT2Siluette; // Lightning + Lightning

    private void Start()
    {
        mergeManager = MergeManager.instance;
        unlockedList = mergeManager.GetUnlockedList();
    }

    public void Update()
    {
        mergeManager.UpdateUnlockedList();
        unlockedList = mergeManager.GetUnlockedList();

        if (unlockedList[0] == true)
        {
            pulverizerT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[1] == true)
        {
            flamethrowerT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[2] == true)
        {
            fireburstT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[3] == true)
        {
            frostbiteT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[4] == true)
        {
            shockwaveT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[5] == true)
        {
            arcticT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[6] == true)
        {
            frostshockT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[7] == true)
        {
            obsidianT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[8] == true)
        {
            embersurgeT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[9] == true)
        {
            teslaT2Siluette.gameObject.SetActive(false);
        }
    }


}
