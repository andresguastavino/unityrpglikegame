using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool Data", menuName = "ScriptableObjects/ToolData")]
public class ToolData : ScriptableObject
{

    public int toolDataId;
    public string toolName;
    public List<float> damagePerLvlArr;
    public List<ResourceData> farmeableResources;
}
