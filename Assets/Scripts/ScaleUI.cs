using UnityEngine;

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
    [Header("Fat Screen Values:")]
    [SerializeField] private float cameraPositionFat;
    [SerializeField] private float moveUpperIconsFatValue;
    [SerializeField] private float moveUpperMenuButtonsFatValue;
    [SerializeField] private float shopScaleFatScreen;
    [SerializeField] private float shopHeightFatScreen;
    [SerializeField] private float healthBarPositionYFatScreen;
    [SerializeField] private float healthBarScaleFatScreen;
    [SerializeField] private float mergeFatScreenMoveYValue;
    [SerializeField] private float mergeFatScreenMoveXValue;
    [SerializeField] private float nextRoundButtonFatMoveValue;

    //wide settings
    [Header("Wide Screen Values:")]
    [SerializeField] private float cameraPositionWide;
    [SerializeField] private float moveUpperMenuButtonsWideValue;
    [SerializeField] private float upperIconsMoveWhenWide;
    [SerializeField] private float shopScaleWideScreen;
    [SerializeField] private float shopHeightWideScreen;
    [SerializeField] private float healthBarPositionYWideScreen;
    [SerializeField] private float healthBarScaleWideScreen;
    [SerializeField] private float mergeWideScreenMoveYValue;
    [SerializeField] private float mergeWideScreenMovexValue;
    [SerializeField] private float nextRoundButtonWideMoveValue;


    //normal settings
    [Header("Normal Screen Values:")]
    [SerializeField] private float cameraPositionNormal;
    [SerializeField] private float moveUpperMenuButtonsValue;
    [SerializeField] private float upperIconsMoveNormalValue;
    [SerializeField] private float shopScaleNormalScreen;
    [SerializeField] private float shopHeightNormalScreen;
    [SerializeField] private float healthBarPositionYNormalScreen;
    [SerializeField] private float healthBarScaleNormalScreen;
    [SerializeField] private float mergeNormalScreenMoveYValue;
    [SerializeField] private float mergeNormalScreenMoveXValue;
    [SerializeField] private float nextRoundButtonNormalMoveValue;

    [Header("Thin Screen Values:")]
    //thin settings
    [SerializeField] private float cameraPositionThinMobile;
    [SerializeField] private float moveUpperMenuButtonsValueThin;
    [SerializeField] private float upperIconsMoveNormalValueThin;
    [SerializeField] private float shopScaleThinScreen;
    [SerializeField] private float shopHeightThinScreen;
    [SerializeField] private float healthBarPositionValueThinScreen;
    [SerializeField] private float healthBarScaleThinScreen;
    [SerializeField] private float mergeThinScreenMoveYValue;
    [SerializeField] private float mergeThinScreenMoveXValue;
    [SerializeField] private float nextRoundButtonThinMoveValue;

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
            ChangeValuesForFatResolution();
        }
        else if (aspectRatio < aspectRatioThinMobile)
        {
            Debug.Log("Nu händer saker för skärmen är en smal mobil");
            ChangeValuesForThinResolution();
        }
        else if (aspectRatio > aspectRatioNormal && aspectRatio <= aspectRatioWide)
        {
            Debug.Log("Nu händer de grejer för skärmen är bred men inte lika bred som ipad");
            ChangeValuesForWideResolution();
        }
        else
        {
            Debug.Log("Nu ändras det till normal värden för skärmen är varken smal eller bred");
            ChangeValuesForNormalResolution();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
         if (!currentScreenSize.Equals(lastScreenSize))
         {
             lastScreenSize = currentScreenSize;
             CheckAspectRatio();
         }*/
        float aspectRatio = (float)Screen.width / Screen.height;
        Debug.Log(aspectRatio);

        if (aspectRatio > aspectRatioWide)
        {
            Debug.Log("Nu händer de grejer för skärmen är Ipad bred.");
            ChangeValuesForFatResolution();
        }
        else if (aspectRatio < aspectRatioThinMobile)
        {
            Debug.Log("Nu händer saker för skärmen är en smal mobil");
            ChangeValuesForThinResolution();
        }
        else if (aspectRatio > aspectRatioNormal && aspectRatio <= aspectRatioWide)
        {
            Debug.Log("Nu händer de grejer för skärmen är bred men inte lika bred som ipad");
            ChangeValuesForWideResolution();
        }
        else
        {
            Debug.Log("Nu ändras det till normal värden för skärmen är varken smal eller bred");
            ChangeValuesForNormalResolution();
        }
    }
    private void CheckAspectRatio()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        Debug.Log(aspectRatio);

        if (aspectRatio > aspectRatioWide)
        {
            Debug.Log("Nu händer de grejer för skärmen är Ipad bred.");
            ChangeValuesForFatResolution();
        }
        else if (aspectRatio < aspectRatioThinMobile)
        {
            Debug.Log("Nu händer saker för skärmen är en smal mobil");
            ChangeValuesForThinResolution();
        }
        else if (aspectRatio > aspectRatioNormal && aspectRatio <= aspectRatioWide)
        {
            Debug.Log("Nu händer de grejer för skärmen är bred men inte lika bred som ipad");
            ChangeValuesForWideResolution();
        }
        else
        {
            Debug.Log("Nu ändras det till normal värden för skärmen är varken smal eller bred");
            ChangeValuesForNormalResolution();
        }
    }



    //samlings metoder
    private void ChangeValuesForFatResolution()
    {
        MoveCameraWhenFat();
        MoveIconsWhenFat();
        ScaleMoveShopWhenFat();
        ScaleMoveHealthbarWhenFat();
        MoveNextRoundButtonWhenFat();
    }
    private void ChangeValuesForWideResolution()
    {
        MoveCameraWhenNotFatOrThin();
        MoveUpperIconsWide();
        ScaleMoveShopWhenWide();
        ScaleMoveHealthbarWhenWide();
        MoveNextRoundButtonWhenWide();
    }
    private void ChangeValuesForNormalResolution()
    {
        MoveCameraWhenNotFatOrThin();
        MoveUpperIconsNormalScreen();
        ScaleMoveShopWhenNormal();
        ScaleMoveHealthbarWhenNormal();
        MoveNextRoundButtonWhenNormal();
    }
    private void ChangeValuesForThinResolution()
    {
        MoveCameraWhenThin();
        MoveUpperIconsThinScreen();
        ScaleMoveShopWhenThin();
        ScaleMoveHealthbarWhenThin();
        MoveNextRoundButtonWhenThin();
    }


    //metoder för FatScreen
    private void MoveCameraWhenFat()
    {
        m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, cameraPositionFat, m_MainCamera.transform.position.z);
    }
    private void MoveIconsWhenFat()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, moveUpperIconsFatValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, moveUpperMenuButtonsFatValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, moveUpperMenuButtonsFatValue, mergeArchive.transform.position.z);
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
        playNextRoundButton.transform.position = new Vector3(playNextRoundButton.transform.position.x, nextRoundButtonFatMoveValue, playNextRoundButton.transform.position.z);
    }

    // metoder för wideScreen

    private void MoveUpperIconsWide()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, upperIconsMoveWhenWide, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, moveUpperMenuButtonsWideValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, moveUpperMenuButtonsWideValue, mergeArchive.transform.position.z);
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
        playNextRoundButton.transform.position = new Vector3(playNextRoundButton.transform.position.x, nextRoundButtonWideMoveValue, playNextRoundButton.transform.position.z);
    }



    //normal värden
    private void MoveCameraWhenNotFatOrThin()
    {
        m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, 0, m_MainCamera.transform.position.z);
    }
    private void MoveUpperIconsNormalScreen()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, upperIconsMoveNormalValue, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, moveUpperMenuButtonsValue, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, moveUpperMenuButtonsValue, mergeArchive.transform.position.z);
    }
    private void ScaleMoveShopWhenNormal()
    {
        shop.GetComponent<RectTransform>().sizeDelta = new Vector2(shop.GetComponent<RectTransform>().sizeDelta.x, shopHeightNormalScreen);
        shop.transform.localScale = new Vector3(shopScaleNormalScreen, shopScaleNormalScreen, shop.transform.localScale.z);
    }
    private void ScaleMoveHealthbarWhenNormal()
    {
        healthBar.transform.position = new Vector3(healthBar.transform.position.x, healthBarPositionYNormalScreen, healthBar.transform.position.z);
        healthBar.transform.localScale = new Vector3(healthBarScaleNormalScreen, 0.65f, healthBar.transform.localScale.z);
    }
    private void MoveNextRoundButtonWhenNormal()
    {
        playNextRoundButton.transform.position = new Vector3(playNextRoundButton.transform.position.x, nextRoundButtonNormalMoveValue, playNextRoundButton.transform.position.z);
    }

    //metoder för thinScreen
    private void MoveCameraWhenThin()
    {
        m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, cameraPositionThinMobile, m_MainCamera.transform.position.z);
    }
    private void MoveUpperIconsThinScreen()
    {
        diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, upperIconsMoveNormalValueThin, diamondIcon.transform.position.z);
        menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, moveUpperMenuButtonsValueThin, menuIcon.transform.position.z);
        mergeArchive.transform.position = new Vector3(mergeArchive.transform.position.x, moveUpperMenuButtonsValueThin, mergeArchive.transform.position.z);
    }
    private void ScaleMoveShopWhenThin()
    {
        shop.GetComponent<RectTransform>().sizeDelta = new Vector2(shop.GetComponent<RectTransform>().sizeDelta.x, shopHeightThinScreen);
        shop.transform.localScale = new Vector3(shopScaleThinScreen, shopScaleThinScreen, shop.transform.localScale.z);
    }
    private void ScaleMoveHealthbarWhenThin()
    {
        healthBar.transform.position = new Vector3(healthBar.transform.position.x, healthBarPositionValueThinScreen, healthBar.transform.position.z);
        healthBar.transform.localScale = new Vector3(healthBarScaleThinScreen, healthBarScaleThinScreen, healthBar.transform.localScale.z);
    }
    private void MoveNextRoundButtonWhenThin()
    {
        playNextRoundButton.transform.position = new Vector3(playNextRoundButton.transform.position.x, nextRoundButtonThinMoveValue, playNextRoundButton.transform.position.z);
    }
}
