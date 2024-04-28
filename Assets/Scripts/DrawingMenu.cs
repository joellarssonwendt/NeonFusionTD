using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingMenu : MonoBehaviour
{
    public GameObject drawingMenu;
    public GameObject drawingUi;
    public GameObject fireButton;
    public GameObject iceButton;
    public GameObject lightningButton;



    // Start is called before the first frame update
    void Start()
    {
        drawingMenu.SetActive(false);
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
        drawingMenu.SetActive (false);
        drawingUi.SetActive(false);
    }


    public void togglefire()
    {
        fireButton.SetActive(false);
    }

}
