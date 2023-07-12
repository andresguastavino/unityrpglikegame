using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    Mode currentMode;

    public CharacterController controller;
    public float animationCrossFadeTime; // .01
    public Transform cam;
    [SerializeField] public Animator animator;
    public PlayerStatsScript playerStats;

    public IInteractable interactableObject;
    public float walkSpeed = 6.0f;
    public float sprintSpeed = 15.0f;
    public float turnSmoothTime = 0.1f;
    public float range = 3f;
    public float turnSmoothVelocity;

    public LayerMask layer;
    public int woodAmount = 0;
    public int rockAmount = 0;
    float maxStamina = 100f;
    public float stamina = 100f;
    public float staminaRegenrationRate = 3f;
    float nextStaminaRegeneration = 0f;

    public float health = 100;

    public UnityEvent onInteractEvent;
    ResourceController farmingTarget;

    public List<ToolController> tools;
    ToolController toolForFarming;

    private void Start()
    {
        currentMode = new PlayerNormalMode(this);
        controller = gameObject.GetComponent<CharacterController>();
        if (tools.Count > 0)
        {
            foreach (ToolController tool in tools) 
            {
                tool.owner = this;
            }
        }
    }
    
    void UpdatePlayerStatsUI()
    {
        playerStats.SetMaxHealth(health);
        playerStats.SetHealth(health);
        playerStats.SetMaxStamina(maxStamina);
        playerStats.SetStamina(stamina);
    }

    void Update()
    {
        currentMode.Update();
        RegenerateStamina();
        UpdatePlayerStatsUI();
    }

    private void FixedUpdate()
    {
        currentMode.FixedUpdate();
    }


    public void CheckForMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.Play("Walk");
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
        } else
        {   
            //animator.CrossFade("Idle", animationCrossFadeTime);
        }
    }

    public void CheckForInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactableObject != null)
        {
            if (interactableObject.CanInteractWith(this))
            {
                interactableObject.InteractWith(this);
                animator.Play("Chop_Idle");
            }
        }
    }

    public void SearchForNearInteractables()
    {
        RaycastHit hit1, hit2;
        bool hitHigh = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit1, Mathf.Infinity, layer);
        bool hitLow = Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit2, Mathf.Infinity, layer);
        // Does the ray intersect any objects excluding the player layer
        if (hitHigh)
        {
            Color c = Color.yellow;
            if (hit1.distance < range)
            {
                c = Color.blue;
                IInteractable interactable = hit1.transform.gameObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactableObject = interactable;
                }
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit1.distance, c);
        }
        if (hitLow)
        {
            Color c = Color.yellow;
            if (hit2.distance < range)
            {
                c = Color.blue;
                IInteractable interactable = hit2.transform.gameObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactableObject = interactable;
                }
            }
            Debug.DrawRay(new Vector3(transform.position.x, 0.5f, transform.position.z), transform.TransformDirection(Vector3.forward) * hit2.distance, c);
        }
        if (!hitLow && !hitHigh)
        {
            interactableObject = null;
        }
    }

    public void ChargeStaminaCost()
    {
        float staminaCost = 10f;
        stamina -= staminaCost;
        if (stamina < 0)
        {
            stamina = 0;
        }
    }

    void RegenerateStamina()
    {
        if (stamina < 100f && Time.time > nextStaminaRegeneration)
        {
            stamina += staminaRegenrationRate;
            if (stamina > 100f)
            {
                stamina = 100f;
            }
            nextStaminaRegeneration = Time.time + 1f;
        }
        if (stamina == 100f)
        {
            nextStaminaRegeneration = 0f;
        }
    }

    public void Farm(ResourceController resource)
    {
        farmingTarget = resource;
        currentMode = new PlayerFarmingMode(this);
    }

    public void CheckForExitMode()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentMode = new PlayerNormalMode(this);
        }
    }

    public void SelectToolForFarming()
    {
        toolForFarming = null;
        foreach (ToolController tool in tools)
        {
            if (tool.CanFarm(farmingTarget))
            {
                toolForFarming = tool;
            }
        }
    }

    public void NullifyToolForFarming()
    {
        toolForFarming = null;
    }

    public void NullifyFarmingTarget()
    {
        farmingTarget = null;
    }

    public void FarmResource()
    {
        if (farmingTarget != null && toolForFarming != null && Input.GetMouseButtonDown(0))
        {
            toolForFarming.Farm(farmingTarget);
            animator.Play("Chop_Action", 0, 0);
            ChargeStaminaCost();
        }
    }

    public void Notify()
    {
        currentMode = new PlayerNormalMode(this);
        farmingTarget = null;
    }

}
