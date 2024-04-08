using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    private GameObject turretToBuild;

    private void Awake()
    {
        Instance = this;
    }
    public GameObject getTurretTobuild()
    {
        return turretToBuild;
    }
}
