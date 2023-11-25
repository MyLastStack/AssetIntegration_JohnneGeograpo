using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Outline outline;

    public bool currentSelected;

    void Start()
    {
        currentSelected = false;
    }

    void Update()
    {
        if (currentSelected)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
