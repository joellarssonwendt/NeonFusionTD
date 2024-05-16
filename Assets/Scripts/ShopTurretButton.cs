using UnityEngine;
using UnityEngine.UI;

public class ShopTurretButton : MonoBehaviour
{
    [Header("Skriv: normal | fire | ice | lightning | lägg till \"super\" innan för supervariant\n detta väljer vilken turret en knapp ska spawna")]
    [SerializeField] private string turretName;
    [SerializeField] private GameObject tempNormalTurret, tempFireTurret, tempIceTurret, tempLightningTurret, tempSuperNormalTurret, tempSuperFireTurret;
    [SerializeField] private GameObject normalTurretButtonObject, fireTurretButtonObject, iceTurretButtonObject, lightningTurretButtonObject;
    [SerializeField] private GameObject audioManager;
    private GameObject shopPanel;
    private Shop shop;
    

    private void Start()
    {
        shopPanel = GameObject.FindWithTag("Shop");
        shop = shopPanel.GetComponent<Shop>();
        
    }
    public void pointerDown()
    {
        if (turretName.ToLower() == "normal" && normalTurretButtonObject.GetComponent<Button>().interactable)
        {
            shop.SelectStandardTurret();
            tempNormalTurret.SetActive(true);
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("ShopButton");
        }
        else if (turretName.ToLower() == "fire" && fireTurretButtonObject.GetComponent<Button>().interactable)
        {
            shop.SelectFireTurret();
            tempFireTurret.SetActive(true);
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("ShopButton");
        }
        else if (turretName.ToLower() == "ice" && iceTurretButtonObject.GetComponent<Button>().interactable)
        {
            shop.SelectIceTurret();
            tempIceTurret.SetActive(true);
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("ShopButton");
        }
        else if (turretName.ToLower() == "lightning" && lightningTurretButtonObject.GetComponent<Button>().interactable)
        {
            shop.SelectLightningTurret();
            tempLightningTurret.SetActive(true);
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("ShopButton");
        }
        else
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("ErrorShopButton");
        }
    }

    public void EnableNormalTowerButton()
    {
        normalTurretButtonObject.GetComponent<Button>().interactable = true;
    }
    public void EnableIceTowerButton()
    {
        iceTurretButtonObject.GetComponent<Button>().interactable = true;
    }
    public void EnableLightningTowerButton()
    {
        lightningTurretButtonObject.GetComponent<Button>().interactable = true;
    }
    public void EnableFireTowerButton()
    {
        fireTurretButtonObject.GetComponent<Button>().interactable = true;
    }

}
