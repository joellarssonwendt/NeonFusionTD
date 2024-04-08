using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool hasTurretOnTile = false;
    private Color grayColor = Color.gray;
    private Color whiteColor = Color.white;

    
    private void OnMouseEnter()
    {
        spriteRenderer.color = grayColor;
    }
    private void OnMouseExit()
    {
        spriteRenderer.color = whiteColor;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
