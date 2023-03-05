using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/All-type")]
public class BlockData : ScriptableObject
{
    [TextArea(5, 20)]
    public string description;
    public Sprite thumbnail;
    public Color backgroundColor;
    public enum Type
    {
        None, Directional, Color, Function
    }

    public Type type;
}
