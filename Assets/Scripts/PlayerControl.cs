using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Cooldown cooldown;

    [SerializeField] public GameObject selectedChar;
    [SerializeField] public NavMeshAgent agentChar;
    private NavMeshPath path;
    RaycastHit hit;

    public bool selected;

    void Start()
    {
        selected = false;
        selectedChar = null;

        path = new NavMeshPath();
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
                    if (hit.transform.CompareTag("PlayerControlled") || hit.transform.CompareTag("AIControlled"))
                    {
                        if (!selected)
                        {
                            CharSelect();
                        }
                    }
                }
                else if (selectedChar.transform.CompareTag("PlayerControlled") && selected)
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        if (!selectedChar.GetComponent<PlayerScript>().moving)
                        {
                            if (selectedChar.GetComponent<PlayerScript>().onWalk)
                            {
                                agentChar.stoppingDistance = 0f;
                                agentChar.SetDestination(hit.point);
                            }
                            else if (selectedChar.GetComponent<PlayerScript>().onSpell)
                            {
                                if (cooldown.IsCoolingDown) return;
                                else
                                {
                                    selectedChar.GetComponent<PlayerScript>().SpellCast(hit.point);
                                    cooldown.StartCooldown();
                                }
                            }
                            else if (selectedChar.GetComponent<PlayerScript>().onMelee)
                            {
                                agentChar.stoppingDistance = 1.0f;
                                if (cooldown.IsCoolingDown) return;
                                else if (hit.transform.CompareTag("AIControlled"))
                                {
                                    selectedChar.GetComponent<PlayerScript>().MeleeSwing(hit.point);
                                    cooldown.StartCooldown();
                                }
                            }
                        }
                    }
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
        else if (hit.transform.CompareTag("AIControlled"))
        {
            selectedChar.gameObject.GetComponent<SkellyScript>().currentlySelected = true;
        }
    }

    public void CharDeselect()
    {
        selected = false;

        if (selectedChar.GetComponent<PlayerScript>() != null)
        {
            selectedChar.gameObject.GetComponent<PlayerScript>().currentSelected = false;
        }
        else if (selectedChar.GetComponent<SkellyScript>() != null)
        {
            selectedChar.gameObject.GetComponent<SkellyScript>().currentlySelected = false;
        }


        selectedChar = null;
    }
}
