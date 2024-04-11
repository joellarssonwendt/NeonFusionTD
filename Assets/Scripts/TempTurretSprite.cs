using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TempTurretSprite : MonoBehaviour
{
    [SerializeField] private GameObject tempTurretHolder;
    [SerializeField] private GameObject attackRangeSprite;
    private Image image;
    private Image attackRangeImage;
    private Color green = Color.green
;   private Color transparentGreen = new Color(67f, 255f, 100f, 0.4f);
    private Color red = new Color(222f, 0, 0, 0.7f);
    private Color redTransparent = new Color(222f, 0, 0, 0.4f);

    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        image = GetComponent<Image>();
        attackRangeImage = attackRangeSprite.GetComponent<Image>();
        image.color = red;
        attackRangeImage.color = red;
    }
    private void Update()
    {
        tempTurretHolder.transform.position = Input.mousePosition;

        if (buildManager.checkIfMouseIsOverATile())
        {
            changeColorToGreen();
        }
        if(!buildManager.checkIfMouseIsOverATile()) 
        { 
            changeColorToRed();
        }
    }
    private void changeColorToRed()
    {
        image.color = redTransparent;
        attackRangeImage.color = red;
    }
    private void changeColorToGreen()
    {
        image.color = green;
        attackRangeImage.color = transparentGreen;
    }
}
