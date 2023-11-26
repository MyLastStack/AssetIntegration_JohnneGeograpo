using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Outline outline;
    [SerializeField] GameObject playerCanvas;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    public bool hovered;
    public bool currentSelected;
    public float huh;

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

        AgentMovementAnimation(agent.velocity.magnitude);
    }

    private void AgentMovementAnimation(float mag)
    {
        animator.SetFloat("Running", mag);
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
