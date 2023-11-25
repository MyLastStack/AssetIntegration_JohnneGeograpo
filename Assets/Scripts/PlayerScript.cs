using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Outline outline;

    public bool hovered;
    public bool currentSelected;

    void Start()
    {
        hovered = false;
        currentSelected = false;
        outline.OutlineWidth = 0f;
    }

    void Update()
    {
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
