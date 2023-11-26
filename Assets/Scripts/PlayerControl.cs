using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public GameObject selectedChar;
    [SerializeField] public NavMeshAgent agentChar;
    RaycastHit hit;

    public float maxDistanceFromPoint;
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
            if (Physics.Raycast(ray, out hit))
            {
                if (selectedChar == null)
                {
                    if (hit.transform.CompareTag("PlayerControlled"))
                    {
                        if (!selected)
                        {
                            CharSelect();
                        }
                    }
                }
                else if (selectedChar.transform.CompareTag("PlayerControlled") && selected)
                {
                    agentChar.SetDestination(hit.point);
                }
            }
        }
    }

    private void CharSelect()
    {
        selected = true;
        selectedChar = hit.transform.gameObject;
        agentChar = hit.transform.gameObject.GetComponent<NavMeshAgent>();

        if (hit.transform.CompareTag("PlayerControlled"))
        {
            selectedChar.gameObject.GetComponent<PlayerScript>().currentSelected = true;
        }
    }

    public void CharDeselect()
    {
        selected = false;

        if (selectedChar.GetComponent<PlayerScript>() != null)
        {
            selectedChar.gameObject.GetComponent<PlayerScript>().currentSelected = false;
        }

        selectedChar = null;
    }
}
