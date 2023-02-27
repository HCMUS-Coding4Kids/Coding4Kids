using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/All-type")]
public class BlockData : ScriptableObject
{
    [TextArea(5, 20)]
    public string description;
    public Sprite thumbnail;
    public enum Type
    {
        None, Directional
    }

    public Type type;
}
