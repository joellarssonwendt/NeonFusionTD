using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBookMenu : MonoBehaviour
{
    public GameObject mergeRecipeMenu;
    private float originalTimeScale;

    public void Start()
    {
        mergeRecipeMenu.SetActive(false);
    }

    public void ToggleArchiveMenu()
    {
        if (!mergeRecipeMenu.activeSelf)
        {
            originalTimeScale = Time.timeScale;
        }
        mergeRecipeMenu.SetActive(!mergeRecipeMenu.activeSelf);

        Time.timeScale = mergeRecipeMenu.activeSelf ? 0.0f : originalTimeScale;
    }

    public void ReturnToGame()
    {
        mergeRecipeMenu.SetActive (false);

        Time.timeScale = 1.0f;
    }
}
