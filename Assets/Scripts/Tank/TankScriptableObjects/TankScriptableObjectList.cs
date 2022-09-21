using UnityEngine;
using System.Collections.Generic;

namespace TankGame
{

    [CreateAssetMenu(fileName = "TankScriptableObjectList", menuName = "scriptableObject/ScriptableObjectTankList")]
    public class TankScriptableObjectList : ScriptableObject
    {
        public TankScriptableObject[] tankList;
    }
}