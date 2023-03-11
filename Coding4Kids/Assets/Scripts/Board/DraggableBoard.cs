using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DraggableBoard : Board
{
    public static new DraggableBoard Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public static BlockData draggingData = null;

    private void Start()
    {
        GetCenterPoint();
        GenerateGrid();
        GenerateExampleBoard();
    }


}
