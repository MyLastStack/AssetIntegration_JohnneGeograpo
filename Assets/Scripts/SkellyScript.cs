using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyScript : MonoBehaviour
{
    [SerializeField] Outline outline;
    [SerializeField] GameObject skellyCanvas;

    public bool hovered;
    public bool currentlySelected;

    void Start()
    {
        hovered = false;
        currentlySelected = false;
        outline.OutlineWidth = 0f;
    }

    void Update()
    {
        skellyCanvas.SetActive(currentlySelected);

        if (currentlySelected)
        {
            outline.OutlineWidth = 1.8f;
        }
        else if (!hovered)
        {
            outline.OutlineWidth = 0f;
        }
    }

    private void OnMouseEnter()
    {
        hovered = true;
        outline.OutlineWidth = 1.8f;
    }
    private void OnMouseExit()
    {
        hovered = false;
    }
}
