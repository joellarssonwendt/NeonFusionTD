using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingMenu : MonoBehaviour
{
    public GameObject drawingMenu;
    public GameObject drawingUi;
    public GameObject fireButton;
    //public GameObject iceButton;
    //public GameObject lightningButton;



    // Start is called before the first frame update
    void Start()
    {
        drawingMenu.SetActive(false);
       // fireButton.SetActive(false);
        //drawingUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void toggleMenu()
    {
        drawingMenu.SetActive(!drawingMenu.activeSelf);
        
    }

    public void closeDrawing()
    {
        drawingMenu.SetActive (!drawingMenu.activeSelf);
        drawingUi.SetActive(!drawingUi.activeSelf);
    }


    public void togglefire()
    {
 
        fireButton.SetActive(!fireButton.activeSelf);
    }
    public void toggledrawingui()
    {
        drawingUi.SetActive (!drawingUi.activeSelf);
    }
}
