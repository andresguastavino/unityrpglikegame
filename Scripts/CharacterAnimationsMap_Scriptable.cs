using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CharactersStats_Scriptable",
    menuName = "Sriptable Objects/CharacterAnimationsMap_Scriptable",
    order = 0
)]
public class CharacterAnimationsMap_Scriptable : ScriptableObject {

    Dictionary<string, string> fromAnimationToAnimationMap;

}