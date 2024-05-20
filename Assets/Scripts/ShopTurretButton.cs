using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTurretButton : MonoBehaviour
{
    [Header("Skriv: normal | fire | ice | lightning | lägg till \"super\" innan för supervariant\n detta väljer vilken turret en knapp ska spawna")]
    [SerializeField] private string turretName;
    [SerializeField] private GameObject tempNormalTurret, tempFireTurret, tempIceTurret, tempLightningTurret, tempSuperNormalTurret, tempSuperFireTurret;
    [SerializeField] private GameObject normalTurretButtonObject, fireTurretButtonObject, iceTurretButtonObject, lightningTurretButtonObject;
    [SerializeField] private GameObject audioManager;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject costText;
    private PlayerStats playerStats;
    private Color greenText = new Color(0f, 241f, 0.5f, 1f);
    private Color redText = Color.red;
    private GameObject shopPanel;
    private Shop shop;
    

    private void Start()
    {
        shopPanel = GameObject.FindWithTag("Shop");
        shop = shopPanel.GetComponent<Shop>();
        playerStats = gameManager.GetComponent<PlayerStats>();
        Invoke("UpdateCostTextColor", 0.1f);
    }
    private void Update()
    {
        UpdateCostTextColor();
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

    public void UpdateCostTextColor()
    {
        if (turretName.ToLower() == "normal")
        {
            if(PlayerStats.Bits >= PlayerStats.normalTowerCost)
            {
                costText.GetComponent<TextMeshProUGUI>().color = greenText;
            }
            else
            {
                costText.GetComponent<TextMeshProUGUI>().color = redText;
            }
        }
        else if (turretName.ToLower() == "ice"){
            if (PlayerStats.Bits >= PlayerStats.iceTowerCost)
            {
                costText.GetComponent<TextMeshProUGUI>().color = greenText;
            }
            else
            {
                costText.GetComponent<TextMeshProUGUI>().color = redText;
            }
        }
        else if (turretName.ToLower() == "lightning")
        {
            if (PlayerStats.Bits >= PlayerStats.lightningTowerCost)
            {
                costText.GetComponent<TextMeshProUGUI>().color = greenText;
            }
            else
            {
                costText.GetComponent<TextMeshProUGUI>().color = redText;
            }
        }
        else if (turretName.ToLower() == "fire")
        {
            if (PlayerStats.Bits >= PlayerStats.fireTowerCost)
            {
                costText.GetComponent<TextMeshProUGUI>().color = greenText;
            }
            else
            {
                costText.GetComponent<TextMeshProUGUI>().color = redText;
            }
        }
    }
}
