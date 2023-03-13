using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoopBlock : CodeBlock
{
    public RectTransform connector;
    public TextMeshProUGUI timeText;

    [Header("Loop Config")]
    public GameObject loopEnd;
    public int maxTimes = 10;
    public int times = 1;

    private void Start()
    {
        connector.GetComponent<Image>().color = blockData.backgroundColor;
        times = 1;
    }

    private void Update()
    {
        float loopStartY = GetComponent<RectTransform>().position.y;
        float loopEndY = loopEnd.transform.GetComponent<RectTransform>().position.y;
        float height = loopEndY - loopStartY;
        connector.position = new Vector2(connector.position.x, loopStartY + height / 2);
        connector.sizeDelta = new Vector2(connector.sizeDelta.x, Mathf.Abs(height));
        timeText.text = times.ToString();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        float interval = Time.time - currentTime;
        if (interval < 0.1f)
        {
            if (DragManager.Instance.dragInto == DragManager.DragInto.LoopCount)
            {
                times++;
                if (times > maxTimes)
                {
                    times = 1;
                }
            }
            else
            {
                for (int i = transform.GetSiblingIndex(); i <= loopEnd.transform.GetSiblingIndex(); i++)
                {
                    Transform block = transform.parent.GetChild(i);
                    BlockData data = block.GetComponent<CodeBlock>().blockData;
                    BlockBarItemList.Instance.Add(data);
                    Destroy(block.gameObject);
                }
            }
        }
        else
        {
            if (SideBarManager.targetIndex < loopEnd.transform.GetSiblingIndex() && SideBarManager.targetIndex != -1)
            {
                transform.SetSiblingIndex(SideBarManager.targetIndex);
            }
        }
        SideBarManager.targetIndex = -1;
    }
}
