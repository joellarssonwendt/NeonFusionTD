using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brush", menuName = "Painting")]
public class BaseBrush : ScriptableObject
{
    public string DisplayName;
    public Texture2D BrushTexture;
    public bool bIsTintable = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
