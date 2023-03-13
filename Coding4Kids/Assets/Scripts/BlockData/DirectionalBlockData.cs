using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/Directional")]
public class DirectionalBlockData : BlockData
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public Direction direction;
}
