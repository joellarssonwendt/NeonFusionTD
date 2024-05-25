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
    [Header("wide settings")]
    [SerializeField] private float cameraPositionWide = -0.4f;
    [SerializeField] private float moveUpperIconsValue = 1.5f;
    [SerializeField] private float shopScaleWideScreen = 0.8f;
    [SerializeField] private float shopHeightWideScreen = 180f;
    [SerializeField] private float healthBarPositionYWideScreen = -1.3f;
    [SerializeField] private float healthBarScaleWideScreen = 0.79f;
    [SerializeField] private float mergeWideScreenMoveValue = 2f;
    [SerializeField] private float nextRoundButtonWideMoveValue = -1.3f;



    //thin settings
    [Header("thin settings")]

    [SerializeField] private float cameraPositionThinMobile = 0.8f;
    [SerializeField] private float shopHeightThinScreen = 200f;
    [SerializeField] private float healthBarPositionValueThinScreen = 10f;

    //normal settings
    [Header("normal settings")]

    [SerializeField] private float upperIconsMoveNormalValue = 60f;

    //almostWide settings
    [Header("almostWide settings")]

    [SerializeField] private float cameraPositionAlmostWide = -0.3f;
    [SerializeField] private float shopHeightAlmostWideScreen = 195f;
    [SerializeField] private float shopScaleAlmostWideScreen = 0.8f;
    [SerializeField] private float healthBarPositionYAlmostWideScreen = -55f;
    [SerializeField] private float healthBarScaleAlmostWideScreen = 0.79f;
    [SerializeField] private float upperIconsMoveAlmostWideValue = 105f;

    void Start()
    {
        m_MainCamera = Camera.main;
        float aspectRatio = (float)Screen.width / Screen.height;
        float aspectRatioWide = 0.7f; // lite större än 2:3
        float aspectRatioWidthLimitWide = 9f / 16f;
        float aspectRatioWidthLimitThin = 9f / 18f;
        if (aspectRatio > aspectRatioWide)
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
        else if(aspectRatio > aspectRatioWidthLimitWide && aspectRatio <= aspectRatioWide)
        {
            Debug.Log("Nu Händer de grejer för skärmen är almost wide");
            MoveCameraWhenAlmostWide();
            MoveShopWhenAlmostWide();
            ScaleMoveHealthbarWhenAlmostWide();
            MoveUpperIconsAlmostWide();
            ScaleMoveShopWhenAlmostWide();
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
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y * moveUpperIconsValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x , menuIcon.transform.position.y * moveUpperIconsValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(menuIcon.transform.position.x, mergeArchive.transform.position.y * mergeWideScreenMoveValue, mergeArchive.transform.position.z);
    }
    private void ScaleMoveShopWhenWide()
    {
        shop.GetComponent<RectTransform>().sizeDelta = new Vector2(shop.GetComponent<RectTransform>().sizeDelta.x, shopHeightWideScreen);
        shop.transform.localScale = new Vector3(shopScaleWideScreen, shopScaleWideScreen, shop.transform.localScale.z);
    }
    private void ScaleMoveHealthbarWhenWide()
    {
       healthBar.transform.position = new Vector3(healthBar.transform.position.x, healthBar.transform.position.y * healthBarPositionYWideScreen, healthBar.transform.position.z);
       healthBar.transform.localScale = new Vector3(healthBarScaleWideScreen, 0.65f, healthBar.transform.localScale.z);
    }
    private void MoveNextRoundButtonWhenWide()
    {
        playNextRoundButton.transform.position = new Vector3(playNextRoundButton.transform.position.x, playNextRoundButton.transform.position.y * nextRoundButtonWideMoveValue, playNextRoundButton.transform.position.z);
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
        healthBar.transform.position = new Vector3(healthBar.transform.position.x, healthBar.transform.position.y * healthBarPositionValueThinScreen, healthBar.transform.position.z);
       
    }

    //få metoder för det vanliga läget()
    private void MoveUpperIconsNormalScreen()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y * upperIconsMoveNormalValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, menuIcon.transform.position.y * upperIconsMoveNormalValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, mergeArchive.transform.position.y * upperIconsMoveNormalValue, mergeArchive.transform.position.z);
    }
    //metoder för almostWide
    private void MoveCameraWhenAlmostWide()
    {
        m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, cameraPositionAlmostWide, m_MainCamera.transform.position.z);
    }
    private void MoveShopWhenAlmostWide()
    {
        shop.GetComponent<RectTransform>().sizeDelta = new Vector2(shop.GetComponent<RectTransform>().sizeDelta.x, shopHeightAlmostWideScreen);
    }
    
    private void MoveUpperIconsAlmostWide()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y * upperIconsMoveAlmostWideValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, menuIcon.transform.position.y * upperIconsMoveAlmostWideValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, mergeArchive.transform.position.y * upperIconsMoveAlmostWideValue, mergeArchive.transform.position.z);
    }
    private void ScaleMoveShopWhenAlmostWide()
    {
        shop.GetComponent<RectTransform>().sizeDelta = new Vector2(shop.GetComponent<RectTransform>().sizeDelta.x, shopHeightAlmostWideScreen);
        shop.transform.localScale = new Vector3(shopScaleAlmostWideScreen, shopScaleAlmostWideScreen, shop.transform.localScale.z);
    }
    private void ScaleMoveHealthbarWhenAlmostWide()
    {
        healthBar.transform.position = new Vector3(healthBar.transform.position.x, healthBar.transform.position.y * healthBarPositionYAlmostWideScreen, healthBar.transform.position.z);
        healthBar.transform.localScale = new Vector3(healthBarScaleAlmostWideScreen, 0.65f, healthBar.transform.localScale.z);
    }

}
