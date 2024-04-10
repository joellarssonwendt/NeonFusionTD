using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopTurretButton : MonoBehaviour
{
    [Header("Skriv: normal | fire | ice | lightning \n detta väljer vilken turret en knapp ska spawna")]
    [SerializeField] private string turretName;
    [SerializeField] private GameObject tempNormalTurret, tempFireTurret, tempIceTurret, tempLightningTurret;
    [SerializeField] private GameObject normalTurretSprite, fireTurretSprite, iceTurretSprite, lightningTurretSprite;
    private GameObject shopPanel;
    private Shop shop;
    private TempTurretSprite tempTurretSprite;

    private void Start()
    {
        shopPanel = GameObject.FindWithTag("Shop");
        shop = shopPanel.GetComponent<Shop>();
        tempTurretSprite = normalTurretSprite.GetComponent<TempTurretSprite>();
    }
    public void pointerDown()
    {
        if (turretName.ToLower().Contains("normal"))
        {
            shop.SelectStandardTurret();
            tempNormalTurret.SetActive(true);
        }
        else if (turretName.ToLower().Contains("fire"))
        {
            shop.SelectFireTurret();
            tempFireTurret.SetActive(true);
        }
        else if (turretName.ToLower().Contains("ice"))
        {
            Debug.Log("Händer inget för att ice turret inte är tillagd");
            tempIceTurret.SetActive(true);
        }
        else if (turretName.ToLower().Contains("lightning"))
        {
            Debug.Log("Händer inget för att lightning turret inte är tillagd");
            tempLightningTurret.SetActive(true);
        }
    }
}
