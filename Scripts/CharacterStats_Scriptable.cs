using UnityEngine;

[CreateAssetMenu(
    fileName = "CharactersStats_Scriptable",
    menuName = "Sriptable Objects/CharacterStats_Scriptable",
    order = 0
)]
public class CharacterStats_Scriptable : ScriptableObject {

    public float walkSpeed;
    public float runSpeed;

    public float baseHP;
    public float baseStamina;

    public float staminaRegenrationRate;

}