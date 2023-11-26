using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Outline outline;
    [SerializeField] GameObject playerCanvas;

    public bool hovered;
    public bool currentSelected;

    void Start()
    {
        hovered = false;
        currentSelected = false;
        outline.OutlineWidth = 0f;
        playerCanvas.SetActive(false);
    }

    void Update()
    {
        playerCanvas.SetActive(currentSelected);

        if (currentSelected)
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
