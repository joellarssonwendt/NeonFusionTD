using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedChecker : MonoBehaviour
{
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

    private void OnEnable()
    {
        MergeManager.OnMergeAction += OnMergeAction;
    }

    
    private void OnDisable()
    {
        MergeManager.OnMergeAction += OnMergeAction;
    }

    private void UpdateSilhouettes()
    {
        pulverizerT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("pulverizer", out bool pulverizer) || !pulverizer);
        flamethrowerT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("flamethrower", out bool flamethrower) || !flamethrower);
        fireburstT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("fireburst", out bool fireburst) || !fireburst);
        frostbiteT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("frostbite", out bool frostbite) || !frostbite);
        shockwaveT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("shockwave", out bool shockwave) || !shockwave);
        arcticT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("arctic", out bool arctic) || !arctic);
        frostshockT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("frostshock", out bool frostshock) || !frostshock);
        obsidianT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("obsidian", out bool obsidian) || !obsidian);
        embersurgeT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("embersurge", out bool embersurge) || !embersurge);
        teslaT2Siluette.gameObject.SetActive(!MergeManager.instance.MergeDictionary.TryGetValue("tesla", out bool tesla) || !tesla);
    }

    private void OnMergeAction(bool state)
    {        
        Debug.Log("Method is used");
        notificationBubble.SetActive(state);
        UpdateSilhouettes();
    }

    public void RemoveNotificationBubble()
    {
        notificationBubble.SetActive(false);
    }
}
