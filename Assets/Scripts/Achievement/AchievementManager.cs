using UnityEngine;
using TMPro;

namespace TankGame
{
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private TextMeshProUGUI achievementDescription;
        [SerializeField] private GameObject achievementPanel;
        [SerializeField] private float uiTimer;
        [SerializeField] private AchievementList achievementList;

        private const string defaltText = "Achievement Unlocked : ";
        

        private void OnEnable()
        {
            Shooting.OnBulletFired += CheckForAchievement;
            
        }
        private void Start()
        {
            achievementPanel.SetActive(false);
            
        }

        private void CheckForAchievement(int bulletCount)
        {
            AchievementScriptableObject achievementObject = null;
            if (achievementList.List != null)
            {
                if (bulletCount == 10)
                {
                    Debug.Log("Rookie");
                    achievementObject = UnlockAchievement(AchievementType.Rookie);
                }
                else if (bulletCount == 20)
                {
                    achievementObject = UnlockAchievement(AchievementType.Proficient);
                }
                else if (bulletCount == 30)
                {
                    achievementObject = UnlockAchievement(AchievementType.Expert);
                }

            }
            if (achievementObject != null)
            {
                ShowAchievementUi(achievementObject);
            }
        }
        private AchievementScriptableObject UnlockAchievement(AchievementType _type)
        {
            AchievementScriptableObject achievementObject = FindAchievementObject(_type);
            if (achievementObject)
            {
                achievementObject.isAchieved = true;
                return achievementObject;
            }
            return null;
        }

        private void ShowAchievementUi(AchievementScriptableObject achievement)
        {
            achievementText.text = defaltText + achievement.name;
            achievementDescription.text = achievement.achievementDescription;
            achievementPanel.SetActive(true);
            Invoke("DeactivateUi", uiTimer);
        }

        private AchievementScriptableObject FindAchievementObject(AchievementType _type)
        {
            return achievementList.List.Find(ach => ach.type == _type);
        }

        private void DeactivateUi()
        {
            achievementPanel.SetActive(false);
        }

        private void OnDisable()
        {
            Shooting.OnBulletFired -= CheckForAchievement;       
        }
    }
}
