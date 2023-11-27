using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Outline outline;
    [SerializeField] GameObject playerCanvas;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    public bool hovered;
    public bool currentSelected;
    public bool moving;

    public float maxDistanceTravel; // DISTANCE OF CASTING SPELL
    [SerializeField] Transform spawner;
    [SerializeField] GameObject projectile;
    // MELEE FX and SCRIPT
    [SerializeField] GameObject meleeScript;
    [SerializeField] GameObject splashScript;
    [SerializeField] ParticleSystem splashFX;
    float fxTimer;

    [SerializeField] GameObject tooFarText;

    [Header("Character Status")]
    public bool onWalk;
    public bool onSpell;
    public bool onMelee;
    public int turnCount;

    void Start()
    {
        hovered = false;
        currentSelected = false;
        outline.OutlineWidth = 0f;

        playerCanvas.SetActive(false);
        splashScript.SetActive(false);
        fxTimer = splashFX.duration + splashFX.startLifetime;

        meleeScript.SetActive(false);

        onWalk = true;
        onSpell = false;
        onMelee = false;

        tooFarText.SetActive(false);
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

    public void OnWalkActivate()
    {
        onWalk = true;
        onSpell = false;
        onMelee = false;
    }
    public void OnSpellActivate()
    {
        onWalk = false;
        onSpell = true;
        onMelee = false;
    }
    public void OnMeleeActivate()
    {
        onWalk = false;
        onSpell = false;
        onMelee = true;
    }

    public void SpellCast(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        if (distance <= maxDistanceTravel)
        {
            transform.LookAt(target);
            spawner.LookAt(target);

            Instantiate(projectile, spawner.position, spawner.rotation);

            animator.Play("LevelUp_Battle_SwordAndShield", 0);
        }
        else
        {
            tooFarText.SetActive(true);
            Invoke("TooFarMsg", 2);
        }
    }

    public void MeleeSwing(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        if (distance <= maxDistanceTravel)
        {
            agent.SetDestination(target);
            if (distance <= agent.stoppingDistance + 3.0f)
            {
                transform.LookAt(target);

                animator.Play("Attack02_SwordAndShiled", 0);

                splashScript.SetActive(true);
                splashFX.Play();
                meleeScript.SetActive(true);

                Invoke("FXDeactivate", fxTimer);
            }
        }
        else
        {
            Invoke("TooFarMsg", 2);
            tooFarText.SetActive(false);
        }
    }
    private void FXDeactivate()
    {
        splashScript.SetActive(false);
        meleeScript.SetActive(false);
    }

    private void TooFarMsg()
    {
        tooFarText.SetActive(false);
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
}
