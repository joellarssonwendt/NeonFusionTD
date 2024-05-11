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

    public GameObject notificationBubble;
    private bool isNotificationActive = false;
    private bool newTurretDiscovered = false;

    private void Start()
    {
        mergeManager = MergeManager.instance;
        unlockedList = mergeManager.GetUnlockedList();
        notificationBubble.SetActive(false);
        UpdateSilhouettes();
    }

    public void Update()
    {
        mergeManager.UpdateUnlockedList();
        unlockedList = mergeManager.GetUnlockedList();

        if(IsAnyTurretUnlocked() && newTurretDiscovered)
        {
            notificationBubble.SetActive(true);
            isNotificationActive = true;
            newTurretDiscovered = false;        //resetta allt efter notifikationen ges.
        } else if (!IsAnyTurretUnlocked()) {
            newTurretDiscovered = false;
        }
        UpdateSilhouettes();
    }

    private bool IsAnyTurretUnlocked()
    {
        foreach (bool unlocked in unlockedList)
        {
            if (unlocked)
            {
                newTurretDiscovered = true;
                return true;
            }
        }
        return false;
    }

    private void UpdateSilhouettes()
    {
        pulverizerT2Siluette.gameObject.SetActive(!unlockedList[0]);
        flamethrowerT2Siluette.gameObject.SetActive(!unlockedList[1]);
        fireburstT2Siluette.gameObject.SetActive(!unlockedList[2]);
        frostbiteT2Siluette.gameObject.SetActive(!unlockedList[3]);
        shockwaveT2Siluette.gameObject.SetActive(!unlockedList[4]);
        arcticT2Siluette.gameObject.SetActive(!unlockedList[5]);
        frostshockT2Siluette.gameObject.SetActive(!unlockedList[6]);
        obsidianT2Siluette.gameObject.SetActive(!unlockedList[7]);
        embersurgeT2Siluette.gameObject.SetActive(!unlockedList[8]);
        teslaT2Siluette.gameObject.SetActive(!unlockedList[9]);
    }

    public void RemoveNotificationBubble()
    {
        if(isNotificationActive)
        {
            notificationBubble.SetActive(false);
            isNotificationActive = false;
        }
    }

}
