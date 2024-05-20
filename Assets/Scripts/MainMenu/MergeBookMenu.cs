using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBookMenu : MonoBehaviour
{
    public GameObject mergeRecipeMenu;

    public void Start()
    {
        mergeRecipeMenu.SetActive(false);
    }

    public void ToggleArchiveMenu()
    {
        mergeRecipeMenu.SetActive(!mergeRecipeMenu.activeSelf);

        Time.timeScale = mergeRecipeMenu.activeSelf ? 0.0f : 1.0f;
    }

    public void ReturnToGame()
    {
        mergeRecipeMenu.SetActive (false);

        Time.timeScale = 1.0f;
    }
}
