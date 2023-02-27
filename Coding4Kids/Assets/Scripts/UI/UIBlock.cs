using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBlock : MonoBehaviour
{
    public BlockData blockData;
    public Image image;
    public GameObject unavailable;
    public TextMeshProUGUI countText;

    public void UpdateBlock(int count)
    {
        countText.text = count.ToString();
        if(count <= 0)
        {
            SetAvailable(false);
        }
        else
        {
            SetAvailable(true);
        }
    }

    public void Init(BlockData blockData)
    {
        this.blockData = blockData;
        image.sprite = blockData.thumbnail;
    }

    private void SetAvailable(bool available)
    {
        unavailable.SetActive(!available);
    }
}
