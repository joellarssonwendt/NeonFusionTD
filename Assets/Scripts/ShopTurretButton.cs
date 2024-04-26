using UnityEngine;

public class ShopTurretButton : MonoBehaviour
{
    [Header("Skriv: normal | fire | ice | lightning | lägg till \"super\" innan för supervariant\n detta väljer vilken turret en knapp ska spawna")]
    [SerializeField] private string turretName;
    [SerializeField] private GameObject tempNormalTurret, tempFireTurret, tempIceTurret, tempLightningTurret, tempSuperNormalTurret, tempSuperFireTurret;
    [SerializeField] private GameObject normalTurretSprite, fireTurretSprite, iceTurretSprite, lightningTurretSprite, superNormalSprite, superFireSprite;
    private GameObject shopPanel;
    private Shop shop;

    private void Start()
    {
        shopPanel = GameObject.FindWithTag("Shop");
        shop = shopPanel.GetComponent<Shop>();
    }
    public void pointerDown()
    {
        if (turretName.ToLower() == "normal")
        {
            shop.SelectStandardTurret();
            tempNormalTurret.SetActive(true);
        }
        if (turretName.ToLower() == "fire")
        {
            shop.SelectFireTurret();
            tempFireTurret.SetActive(true);
        }
        if (turretName.ToLower() == "ice")
        {
            shop.SelectIceTurret();
            tempIceTurret.SetActive(true);
        }
        if (turretName.ToLower() == "lightning")
        {
            shop.SelectLightningTurret();
            tempLightningTurret.SetActive(true);
        }
    }
}
