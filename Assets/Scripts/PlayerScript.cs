using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] InputAction walkAction;
    [SerializeField] InputAction spellAction;
    [SerializeField] InputAction meleeAction;

    [SerializeField] Outline outline;
    [SerializeField] GameObject playerCanvas;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    public bool hovered;
    public bool currentSelected;
    public bool moving;

    public float maxDistanceTravel; //spells

    bool onWalk;
    bool onSpell;
    bool onMelee;

    public int turnCount;

    void Start()
    {
        hovered = false;
        currentSelected = false;
        outline.OutlineWidth = 0f;

        playerCanvas.SetActive(false);

        onWalk = true;
        onSpell = false;
        onMelee = false;
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

    private void CurrentKey()
    {
        if (walkAction.WasPressedThisFrame())
        {
            onWalk = true;
            onSpell = false;
            onMelee = false;
        }
        if (spellAction.WasPressedThisFrame())
        {
            onWalk = false;
            onSpell = true;
            onMelee = false;
        }
        if (meleeAction.WasPressedThisFrame())
        {
            onWalk = false;
            onSpell = false;
            onMelee = true;
        }
    }

    private void TurnSelect()
    {
        if ()
        {

        }
    }

    private void AgentMovementAnimation(float mag)
    {
        animator.SetFloat("Running", mag);
        if (mag > 0) { moving = true; }
        else { moving = false; }
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

    private void OnEnable()
    {
        walkAction.Enable();
        spellAction.Enable();
        meleeAction.Enable();
    }
    private void OnDisable()
    {
        walkAction.Disable();
        spellAction.Disable();
        meleeAction.Disable();
    }
}
