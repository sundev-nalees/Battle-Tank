using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObjectList",menuName ="scriptableObject/ScriptableObjectTankList")]
public class TankScriptableObjectList : ScriptableObject
{
    public TankScriptableObject[] tankList;
}
