using UnityEngine;

[CreateAssetMenu(fileName = "Resource Data", menuName = "ScriptableObjects/ResourceData")]
public class ResourceData : ScriptableObject
{
    public int resourceDataId;
    public string resourceName;
    public int minResourceDrop;
    public int maxResourceDrop;
    public float baseHealthPoints;
}
