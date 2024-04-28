using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableCanvas : MonoBehaviour
{

    enum EPaintingMode
    {
        Off,
        Draw
    }


    [SerializeField] float RaycastDistance = 10.0f;
    [SerializeField] LayerMask PaintableCanvasLayerMask = ~0;
    [SerializeField] MeshFilter CanvasMeshFilter;
    [SerializeField] MeshRenderer CanvasMeshRenderer;
    [SerializeField] int PixelsPerMetre = 200;
    [SerializeField] Color CanvasDefaultColour = Color.white;

    EPaintingMode PaintingMode_PrimaryMouse = EPaintingMode.Draw;

    int CanvasWidthInPixels;
    int CanvasHeightInPixels;

    Texture2D PaintableTexture;

    BaseBrush ActiveBrush;
    
  
    // Start is called before the first frame update
    void Start()
    {

        CanvasWidthInPixels = Mathf.CeilToInt(CanvasMeshFilter.mesh.bounds.size.x * CanvasMeshFilter.transform.localScale.x * PixelsPerMetre);
        CanvasHeightInPixels = Mathf.CeilToInt(CanvasMeshFilter.mesh.bounds.size.y * CanvasMeshFilter.transform.localScale.y * PixelsPerMetre);


        PaintableTexture = new Texture2D(CanvasWidthInPixels, CanvasHeightInPixels, TextureFormat.ARGB32, false);

        for (int y = 0; y < CanvasHeightInPixels; y++)
        {
            for (int x = 0; x < CanvasWidthInPixels; x++)
            {
                PaintableTexture.SetPixel(x, y, CanvasDefaultColour);
            }
        }

        PaintableTexture.Apply();

        CanvasMeshRenderer.material.mainTexture = PaintableTexture;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (ActiveBrush != null)
        {
            if (PaintingMode_PrimaryMouse == EPaintingMode.Draw && Input.GetMouseButton(0))
            {
                Update_PerformDrawing(PaintingMode_PrimaryMouse);
            }
        }*/
    }

    /*void Update_PerformDrawing(EPaintingMode PaintingMode)
    {
        Ray DrawingRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.RaycastNonAlloc(DrawingRay, HitResults, RaycastDistance, PaintableCanvasLayerMask) > 0)
        {
            PerformDrawingWith(ActiveBrush, ActiveColour, HitResults[0].textureCoord);
        }
    }
    */

    
}
