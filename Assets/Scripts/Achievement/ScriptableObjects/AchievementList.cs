using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{


    [CreateAssetMenu(fileName = " AchievementList", menuName = "ScriptableObject/Achievement/AchievementList")]
    public class AchievementList : ScriptableObject
    {
        public List<AchievementScriptableObject> List;
    }

}
