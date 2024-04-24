using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedChecker : MonoBehaviour
{
    MergeManager mergeManager;
    ReadOnlyCollection<bool> unlockedList;

    public Image kineticT2Siluette;
    public Image flameThrowerT2Siluette;
    public Image kineticFireT2Siluette;

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
            kineticT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[1] == true)
        {
            flameThrowerT2Siluette.gameObject.SetActive(false);
        }

        if (unlockedList[2] == true)
        {
            kineticFireT2Siluette.gameObject.SetActive(false);
        }
    }


}
