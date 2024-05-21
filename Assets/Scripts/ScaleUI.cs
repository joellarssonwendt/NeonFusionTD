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

    //wide settings
    private float cameraPositionWide = -0.4f;
    private float moveUpperIconsValue = 120f;
    private float shopScaleWideScreen = 0.8f;
    private float shopHeightWideScreen = 180f;
    private float healthBarPositionYWideScreen = 290f;
    private float healthBarScaleWideScreen = 0.79f;
    private float mergeWideScreenMoveValue = 100f;
    private float nextRoundButtonWideMoveValue = 80f;



    //thin settings
    private float cameraPositionThinMobile = 0.8f;
    private float shopHeightThinScreen = 200f;
    private float healthBarPositionValueThinScreen = 10f;

    //normal settings
    private float upperIconsMoveNormalValue = 40f;

    void Start()
    {
        m_MainCamera = Camera.main;
        float aspectRatio = (float)Screen.width / Screen.height;
        float aspectRatioWidthLimitWide = 9f / 16f;
        float aspectRatioWidthLimitThin = 9f / 18f;
        if (aspectRatio > aspectRatioWidthLimitWide)
        {
            Debug.Log("Nu händer de grejer för skärmen är bred.");
            MoveCameraWhenWide();
            MoveIconsWhenWide();
            ScaleMoveShopWhenWide();
            ScaleMoveHealthbarWhenWide();
            MoveNextRoundButtonWhenWide();
        }
        else if(aspectRatio < aspectRatioWidthLimitThin)
        {
            Debug.Log("Nu händer saker för skärmen är en smal mobil");
            MoveCameraWhenThin();
            MoveShopWhenThin();
            MoveHealthbarWhenThin();
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

    }
    //metoder för wideScreen
    private void MoveCameraWhenWide()
    {
        m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, cameraPositionWide, m_MainCamera.transform.position.z);
    }
    private void MoveIconsWhenWide()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y + moveUpperIconsValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x -20f, menuIcon.transform.position.y + moveUpperIconsValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(menuIcon.transform.position.x, mergeArchive.transform.position.y - mergeWideScreenMoveValue, mergeArchive.transform.position.z);
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
    private void MoveNextRoundButtonWhenWide()
    {
        playNextRoundButton.transform.position = new Vector3(playNextRoundButton.transform.position.x, playNextRoundButton.transform.position.y - nextRoundButtonWideMoveValue, playNextRoundButton.transform.position.z);
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

    //få metoder för det vanliga läget()
    private void MoveUpperIconsNormalScreen()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y + upperIconsMoveNormalValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, menuIcon.transform.position.y + upperIconsMoveNormalValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, mergeArchive.transform.position.y + upperIconsMoveNormalValue, mergeArchive.transform.position.z);
    }

}
