using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/Color")]
public class ColorBlockData : BlockData
{
    public enum ColorCode
    {
        Red, Orange, Yellow, Green, Blue
    }

    public ColorCode color;
    public Color colorPalete;
}
