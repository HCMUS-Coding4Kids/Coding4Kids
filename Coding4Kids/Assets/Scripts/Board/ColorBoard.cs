using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBoard : Board
{ 
    public static new ColorBoard Instance { get; private set; }

    public GameObject background;

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

    private void Start()
    {
        GetCenterPoint();
        GenerateGrid();
        background.gameObject.SetActive(true);
        background.transform.localScale = new Vector3(cols + (cols + 1) * spacing, rows + (rows + 1) * spacing, 0);
        GenerateExampleBoard();
    }

}
