using UnityEngine;
using System.Collections.Generic;



    [CreateAssetMenu(fileName = "TankScriptableObjectList", menuName = "scriptableObject/ScriptableObjectTankList")]
    public class TankScriptableObjectList : ScriptableObject
    {
        public TankScriptableObject[] tankList;
    }
