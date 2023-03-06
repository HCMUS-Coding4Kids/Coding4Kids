using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlock : BoardBlock
{
    [Header("Graphic Elements")]
    public GameObject image;
    public Color nullColor;

    SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        spriteRenderer = image.GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        UpdateBlock();
    }

    public override void UpdateBlock()
    {
        if (blockData == null || blockData.type != BlockData.Type.Color)
        {
            spriteRenderer.color = nullColor;
        }
        else
        {
            ColorBlockData colorBlockData = blockData as ColorBlockData;
            spriteRenderer.color = colorBlockData.colorPalete;
        }
    }
}
