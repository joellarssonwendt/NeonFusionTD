using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopTurretButton : MonoBehaviour
{
    [Header("Skriv: normal | fire | ice | lightning \n detta väljer vilken turret en knapp ska spawna")]
    [SerializeField] private string turretName;
    private GameObject shopPanel;
    private Shop shop;


    private void Start()
    {
        shopPanel = GameObject.FindWithTag("Shop");
        shop = shopPanel.GetComponent<Shop>();
    }
    public void pointerDown()
    {
        if (turretName.ToLower().Contains("normal"))
        {
            shop.SelectStandardTurret();
        }
        else if (turretName.ToLower().Contains("fire"))
        {
            shop.SelectFireTurret();
        }
        else if (turretName.ToLower().Contains("ice"))
        {
            Debug.Log("Händer inget för att ice turret inte är tillagd");
        }
        else if (turretName.ToLower().Contains("lightning"))
        {
            Debug.Log("Händer inget för att lightning turret inte är tillagd");
        }
    }
}
