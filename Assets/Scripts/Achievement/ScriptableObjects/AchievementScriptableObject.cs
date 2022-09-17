using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public enum AchievementType
    {
        Rookie,
        Proficient,
        Expert,
    }

    [CreateAssetMenu(fileName = "NewAchivement", menuName = "ScriptableObject/Achivement/AchivementObject")]
    public class AchievementScriptableObject : ScriptableObject
    {
        public AchievementType type;
        public string achievementName;
        public string achievementDescription;
        public bool isAchieved;
    }
}
