using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{

    public int currentLevel;
    public PlayerController owner;
    public ToolData toolData;

    private void Start()
    {
        this.currentLevel = 0;
    }

    public bool CanFarm(ResourceController resource)
    {
        if (toolData != null)
        {
            return toolData.farmeableResources.Contains(resource.resourceData);
        }

        Log();
        return false;
    }

    public virtual void Farm(ResourceController resource)
    {
        resource.TakeDamage(GetDamage());
    }

    float GetDamage()
    {
        if (toolData != null)
        {
            return toolData.damagePerLvlArr[currentLevel];
        }

        Log();
        return 0f;
    }

    void Log()
    {
        Debug.LogError("No tool data assigned!");
    }

}
