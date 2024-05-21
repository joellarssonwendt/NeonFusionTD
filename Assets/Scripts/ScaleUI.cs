using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleUI : MonoBehaviour
{
    [SerializeField] private GameObject diamondIcon;
    [SerializeField] private GameObject menuIcon;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject mergeArchive;
    [SerializeField] private GameObject playNextRoundButton;
    private Camera m_MainCamera;
    private Vector2 lastScreenSize;

    //fat settings
    private float cameraPositionFat = -0.4f;
    private float moveUpperIconsValue = 120f;
    private float shopScaleFatScreen = 0.8f;
    private float shopHeightFatScreen = 180f;
    private float healthBarPositionYFatScreen = 290f;
    private float healthBarScaleFatScreen = 0.79f;
    private float mergeFatScreenMoveValue = 100f;
    private float nextRoundButtonFatMoveValue = 80f;



    //thin settings
    private float cameraPositionThinMobile = 0.8f;
    private float shopHeightThinScreen = 200f;
    private float healthBarPositionValueThinScreen = 10f;

    //normal settings
    private float upperIconsMoveNormalValue = 65f;

    //wide settings
    private float upperIconsMoveWhenWide = 40f;
    private float healthBarPositionYWideScreen = 92f;
    private float healthBarScaleWideScreen = 0.89f;
    private float shopScaleWideScreen = 0.9f;
    private float shopHeightWideScreen = 185f;

    //resolutions
    private float aspectRatioWide = 0.7f; // lite större än 2:3
    private float aspectRatioNormal = 9f / 16f; //0.5625
    private float aspectRatioThinMobile = 9f / 18f; //0.5

    void Start()
    {
        m_MainCamera = Camera.main;
        lastScreenSize = new Vector2(Screen.width, Screen.height);
        float aspectRatio = (float)Screen.width / Screen.height;
        Debug.Log(aspectRatio);
        if (aspectRatio > aspectRatioWide)
        {
            Debug.Log("Nu händer de grejer för skärmen är Ipad bred.");
            MoveCameraWhenFat();
            MoveIconsWhenFat();
            ScaleMoveShopWhenFat();
            ScaleMoveHealthbarWhenFat();
            MoveNextRoundButtonWhenFat();
        }
        else if (aspectRatio < aspectRatioThinMobile)
        {
            Debug.Log("Nu händer saker för skärmen är en smal mobil");
            MoveCameraWhenThin();
            MoveShopWhenThin();
            MoveHealthbarWhenThin();
        }
        else if (aspectRatio > aspectRatioNormal && aspectRatio <= aspectRatioWide)
        {
            //flytta grejer för att de är en skärm som inte är lika fet som en ipad men ändå väldigt bred.
            Debug.Log("Nu händer de grejer för skärmen är bred men inte lika bred som ipad");
            MoveUpperIconsWide();
            ScaleMoveShopWhenWide();
            ScaleMoveHealthbarWhenWide();
        }
        else
        {
            Debug.Log("Nu händer inget för skärmen är inte smal eller bred");
            MoveUpperIconsNormalScreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
        if (!currentScreenSize.Equals(lastScreenSize))
        {
            OnResolutionChanged();
            lastScreenSize = currentScreenSize;
        }
        
    }
    void OnResolutionChanged()
    {
        // Din kod för att hantera upplösningsändringar
        //
        float aspectRatio = (float)Screen.width / Screen.height;
        //Debug.Log(aspectRatio);
        if (aspectRatio > aspectRatioWide)
        {
            Debug.Log("Nu händer de grejer för skärmen är Ipad bred.");
            MoveCameraWhenFat();
            MoveIconsWhenFat();
            ScaleMoveShopWhenFat();
            ScaleMoveHealthbarWhenFat();
            MoveNextRoundButtonWhenFat();
        }
        else if (aspectRatio < aspectRatioThinMobile)
        {
            Debug.Log("Nu händer saker för skärmen är en smal mobil");
            MoveCameraWhenThin();
            MoveShopWhenThin();
            MoveHealthbarWhenThin();
        }
        else if (aspectRatio > aspectRatioNormal && aspectRatio <= aspectRatioWide)
        {
            //flytta grejer för att de är en skärm som inte är lika fet som en ipad men ändå väldigt bred.
            Debug.Log("Nu händer de grejer för skärmen är bred men inte lika bred som ipad");
            MoveUpperIconsWide();
            ScaleMoveShopWhenWide();
            ScaleMoveHealthbarWhenWide();
        }
        else
        {
            Debug.Log("Nu händer inget för skärmen är inte smal eller bred");
            MoveUpperIconsNormalScreen();
        }
    }
    //metoder för FatScreen
    private void MoveCameraWhenFat()
    {
        m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, cameraPositionFat, m_MainCamera.transform.position.z);
    }
    private void MoveIconsWhenFat()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y + moveUpperIconsValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x -20f, menuIcon.transform.position.y + moveUpperIconsValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(menuIcon.transform.position.x, mergeArchive.transform.position.y - mergeFatScreenMoveValue, mergeArchive.transform.position.z);
    }
    private void ScaleMoveShopWhenFat()
    {
        shop.GetComponent<RectTransform>().sizeDelta = new Vector2(shop.GetComponent<RectTransform>().sizeDelta.x, shopHeightFatScreen);
        shop.transform.localScale = new Vector3(shopScaleFatScreen, shopScaleFatScreen, shop.transform.localScale.z);
    }
    private void ScaleMoveHealthbarWhenFat()
    {
       healthBar.transform.position = new Vector3(healthBar.transform.position.x, healthBarPositionYFatScreen, healthBar.transform.position.z);
       healthBar.transform.localScale = new Vector3(healthBarScaleFatScreen, 0.65f, healthBar.transform.localScale.z);
    }
    private void MoveNextRoundButtonWhenFat()
    {
        playNextRoundButton.transform.position = new Vector3(playNextRoundButton.transform.position.x, playNextRoundButton.transform.position.y - nextRoundButtonFatMoveValue, playNextRoundButton.transform.position.z);
    }



    //metoder för thinScreen
    private void MoveCameraWhenThin()
    {
        m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, cameraPositionThinMobile, m_MainCamera.transform.position.z);
    }
    private void MoveShopWhenThin()
    {
        shop.GetComponent<RectTransform>().sizeDelta = new Vector2(shop.GetComponent<RectTransform>().sizeDelta.x, shopHeightThinScreen);
    }
    private void MoveHealthbarWhenThin()
    {
        healthBar.transform.position = new Vector3(healthBar.transform.position.x, healthBar.transform.position.y + healthBarPositionValueThinScreen, healthBar.transform.position.z);
       
    }

    //normal värden
    private void MoveUpperIconsNormalScreen()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y + upperIconsMoveNormalValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, menuIcon.transform.position.y + upperIconsMoveNormalValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, mergeArchive.transform.position.y + upperIconsMoveNormalValue, mergeArchive.transform.position.z);
    }

    // metoder för wideScreen

    private void MoveUpperIconsWide()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y + upperIconsMoveWhenWide, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, menuIcon.transform.position.y + upperIconsMoveWhenWide, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, mergeArchive.transform.position.y + upperIconsMoveWhenWide, mergeArchive.transform.position.z);
    }
    private void ScaleMoveShopWhenWide()
    {
        shop.GetComponent<RectTransform>().sizeDelta = new Vector2(shop.GetComponent<RectTransform>().sizeDelta.x, shopHeightWideScreen);
        shop.transform.localScale = new Vector3(shopScaleWideScreen, shopScaleWideScreen, shop.transform.localScale.z);
    }
    private void ScaleMoveHealthbarWhenWide()
    {
        healthBar.transform.position = new Vector3(healthBar.transform.position.x, healthBarPositionYWideScreen, healthBar.transform.position.z);
        healthBar.transform.localScale = new Vector3(healthBarScaleWideScreen, 0.65f, healthBar.transform.localScale.z);
    }

}
