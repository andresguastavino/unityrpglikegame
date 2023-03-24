using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour, IInteractable
{

    public ResourceData resourceData;
    public Canvas statsBillboard;
    public ObjectsStatsScript stats;

    float currentHealthPoints;

    PlayerController other;

    private void Start()
    {
        this.other = null;
        if (this.resourceData != null)
        {
            this.currentHealthPoints = resourceData.baseHealthPoints;
            this.stats = this.statsBillboard.GetComponent<ObjectsStatsScript>();
            this.stats.SetMaxHealth(resourceData.baseHealthPoints);
            this.stats.SetHealth(resourceData.baseHealthPoints);
            this.stats.SetObjectName(resourceData.name);
            this.statsBillboard.gameObject.SetActive(false);
        }
        else
        {
            Log();
        }
    }

    void Update()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= 10f) 
        {
            this.statsBillboard.gameObject.SetActive(true);
        } else
        {
            this.statsBillboard.gameObject.SetActive(false);
        }
    }

    public bool CanInteractWith(PlayerController other)
    {
        bool reqsChecked = CheckReqs(other);
        if (reqsChecked)
        {
            this.other = other;
        }
        return reqsChecked;
    }

    public void InteractWith(PlayerController other)
    {
        this.other = other;
        other.Farm(this);
    }

    public void TakeDamage(float damage)
    {
        this.currentHealthPoints -= damage;
        this.stats.SetHealth(this.currentHealthPoints);
        if (this.currentHealthPoints <= 0)
        {
            other.Notify();
            Destroy(gameObject);
        }
    }

    protected virtual bool CheckReqs(PlayerController other)
    {
        if (other != null)
        {
            foreach (ToolController toolCtrl in other.tools)
            {
                if (toolCtrl.CanFarm(this))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void Log()
    {
        Debug.LogError("No resource data assigned!");
    }

}
