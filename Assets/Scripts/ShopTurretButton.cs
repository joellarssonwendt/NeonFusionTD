using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopTurretButton : MonoBehaviour
{
    [Header("Skriv: normal | fire | ice | lightning \n detta v�ljer vilken turret en knapp ska spawna")]
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
            Debug.Log("H�nder inget f�r att ice turret inte �r tillagd");
        }
        else if (turretName.ToLower().Contains("lightning"))
        {
            Debug.Log("H�nder inget f�r att lightning turret inte �r tillagd");
        }
    }
}
