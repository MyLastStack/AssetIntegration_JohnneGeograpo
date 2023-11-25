using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public GameObject selectedChar;
    RaycastHit hit;

    public bool selected;

    void Start()
    {
        selected = false;
        selectedChar = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && selectedChar == null)
            {
                Debug.Log("Hit object name: " + hit.transform.name);
                Debug.Log("Hit object tag: " + hit.transform.tag);

                if (hit.transform.CompareTag("PlayerControlled"))
                {
                    if (!selected)
                    {
                        CharSelect();
                    }
                }
            }
        }
    }

    private void CharSelect()
    {
        Debug.Log("Selected");
        selected = true;
        selectedChar = hit.transform.gameObject;

        if (hit.transform.CompareTag("PlayerControlled"))
        {
            selectedChar.gameObject.GetComponent<PlayerScript>().currentSelected = true;
        }
    }

    public void CharDeselect()
    {
        Debug.Log("Deselected");
        selected = false;

        if (selectedChar.GetComponent<PlayerScript>() != null)
        {
            selectedChar.gameObject.GetComponent<PlayerScript>().currentSelected = false;
        }

        selectedChar = null;
    }
}
