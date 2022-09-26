using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TankGame
{
    public class InGameHandler : MonoBehaviour
    {
        [SerializeField] private int killScore;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private TextMeshProUGUI achievementDescription;
        [SerializeField] private GameObject achievementPanel;
        [SerializeField] private float achievementShowTimer;

        //pause
        [SerializeField] private GameObject pausePanel;
        private SceneManager gameManager;
        private AudioManager audioManager;

        //GameOver
        [SerializeField] private GameObject gameOverPanel;

        private int CurrentScore;
        private const string defaultText = "SCORE :";
        private const string defaultAchievemenText = "Achievement Unlocked :";

      
        private void OnEnable()
        {
            EventManager.Instance.OnEnemyDeath += UpdateScore;  
            EventManager.Instance.OnGameOver += OnGameOver;
        }
        private void Start()
        {
            achievementPanel.SetActive(false);
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            IntializeScore();
            gameManager = SceneManager.Instance;
            audioManager = AudioManager.Instance;
            Time.timeScale = 1f;
            
        }

        private void IntializeScore()
        {
            CurrentScore = 0;
            score.text = defaultText + CurrentScore;
            
        }

        private void UpdateScore()
        {
            CurrentScore += killScore;
            score.text = defaultText + CurrentScore;
        }

        

        private void PlayAchievementSounnd()
        {
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.Achievement);
            }
        }

        public void OnAchievementUnlocked(AchievementScriptableObject achievement)
        {
            achievementText.text = defaultAchievemenText + achievement.name;
            achievementDescription.text = achievement.achievementDescription;
            achievementPanel.SetActive(true);
            PlayAchievementSounnd();
            Invoke(nameof(DeactivateUi), achievementShowTimer);
        }

        private void DeactivateUi()
        {
            achievementPanel.SetActive(false);
        }

        private void OnDisable()
        {
            EventManager.Instance.OnEnemyDeath -= UpdateScore;
            EventManager.Instance.OnGameOver -= OnGameOver;
        }

        public void OnButtonClick()
        {
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.ButtonClick);
            }
        }

        public void OnGameOver()
        {
            gameOverPanel.SetActive(true);
            StopSounds();
            
        }

        public void StopSounds()
        {
            if (audioManager)
            {
                audioManager.StopAudio(AudioSourceType.background);
                audioManager.StopAudio(AudioSourceType.game);
            }
        }

        public void OnPauseButtonPress()
        {
            if (pausePanel.activeInHierarchy)
            {
                Resume();
                return;
            }
            pausePanel.SetActive(true);
            if (audioManager)
            {
                audioManager.Mute();
            }
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            pausePanel.SetActive(false);
            if (audioManager)
            {
                audioManager.ResetSounds();
            }
            Time.timeScale = 1f;
        }

        public void LoadMainMenu()
        {
            if (gameManager)
            {
                gameManager.LoadMainMenu();
                audioManager.StopAudio(AudioSourceType.game);
            }
        }

        public void Restart()
        {
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.BackgroundMusic);
                audioManager.PlaySound(SoundType.TankIdel);
                audioManager.ResetSounds();
            }
            if (gameManager)
            {
                gameManager.Restart();    
            }
        }
        public void Quit()
        {
            if (gameManager)
            {
                gameManager.Quit();
            }
        }
    }
}
