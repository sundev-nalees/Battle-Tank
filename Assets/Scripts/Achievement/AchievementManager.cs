using UnityEngine;
using TMPro;

namespace TankGame
{
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField] private AchievementList achievementList;
        [SerializeField] private InGameHandler gameHandler;

        private int bulletCount;

        private void Start()
        {
            EventManager.Instance.OnBulletFired += CheckForAchievement;
        }
        
        

        private void CheckForAchievement()
        {
            bulletCount++;
            AchievementScriptableObject achievementObject = null;
            if (achievementList.List != null)
            {
                if (bulletCount == 10)
                {
                    Debug.Log("Rookie");
                    achievementObject = UnlockAchievement(AchievementType.Rookie);
                }
                else if (bulletCount == 25)
                {
                    achievementObject = UnlockAchievement(AchievementType.Proficient);
                }
                else if (bulletCount == 50)
                {
                    achievementObject = UnlockAchievement(AchievementType.Expert);
                }

            }
            if (achievementObject != null && gameHandler)
            {
                gameHandler.OnAchievementUnlocked(achievementObject);
            }           
        }
        private AchievementScriptableObject UnlockAchievement(AchievementType _type)
        {
            AchievementScriptableObject achievementObject = FindAchievementObject(_type);
            if (achievementObject&&!IsUnlocked(achievementObject))
            {
                PlayerPrefs.SetInt(achievementObject.name, (int)AchievementStatus.Unlocked);
                return achievementObject;
            }
            return null;
        }

        private bool IsUnlocked(AchievementScriptableObject achivementObject)
        {
            if (PlayerPrefs.HasKey(achivementObject.name))
            {
                if (PlayerPrefs.GetInt(achivementObject.name) == (int)AchievementStatus.Unlocked)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        private AchievementScriptableObject FindAchievementObject(AchievementType _type)
        {
            return achievementList.List.Find(ach => ach.type == _type);
        }

        private void OnDisable()
        {
            EventManager.Instance.OnBulletFired -= CheckForAchievement;
        }
    }
}
public enum AchievementStatus
{
    Locked,
    Unlocked
}