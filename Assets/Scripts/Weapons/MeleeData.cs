using UnityEngine;

[CreateAssetMenu(fileName = "MeleeData", menuName = "Scriptable Objects/MeleeData")]
public class MeleeData : ScriptableObject
{
    [Header("Vars")]
    public string meleeName;
    public int meleeDmg;
}