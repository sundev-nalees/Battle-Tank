using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] private GameObject settings;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private int firstLevel;

        [SerializeField] private Slider backgroundSlider;
        [SerializeField] private Slider gameSlider;
        [SerializeField] private Slider sfxSlider;

        private const string BACKGROUND_VOLUME = "backgroundVolume";
        private const string GAME_VOLUME = "gameVolume";
        private const string SFX_VOLUME = "sfxVolume";


        private SceneManager gameManager;
        private AudioManager audioManager;

        private void Start()
        {
            settings.SetActive(false);
            mainMenuPanel.SetActive(true);
            gameManager = SceneManager.Instance;
            audioManager = AudioManager.Instance;
            SetSliderValue();
        }

        public void StartGame()
        {
            if (gameManager)
            {
                gameManager.LoadLevel(firstLevel);
            }
            
        }

        public void OnButtonPress()
        {
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.ButtonClick);
            }
        }

        public void OnSettingButtonPress()
        {
            mainMenuPanel.SetActive(false);
            settings.SetActive(true);
        }

        public void OnSettingsClose()
        {
            mainMenuPanel.SetActive(true);
            settings.SetActive(false);
        }
        public void Quit()
        {
            gameManager.Quit();
        }

        //settings

        public void SetSliderValue()
        {
            if (PlayerPrefs.HasKey(BACKGROUND_VOLUME))
            {
                backgroundSlider.value = PlayerPrefs.GetFloat(BACKGROUND_VOLUME);
            }
            else if (PlayerPrefs.HasKey(SFX_VOLUME))
            {
                sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME);
            }
            else if (PlayerPrefs.HasKey(GAME_VOLUME))
            {
                gameSlider.value = PlayerPrefs.GetFloat(GAME_VOLUME);
            }
        }

        public void OnBackgroundSliderChanged()
        {
            if (audioManager)
            {
                audioManager.SetVolume(AudioSourceType.background, backgroundSlider.value);
                PlayerPrefs.SetFloat(BACKGROUND_VOLUME, backgroundSlider.value);
                PlayerPrefs.Save();
            }
        }

        public void OnGameSliderChanged()
        {
            if (audioManager)
            {
                audioManager.SetVolume(AudioSourceType.game, gameSlider.value);
                PlayerPrefs.SetFloat(GAME_VOLUME, gameSlider.value);
                PlayerPrefs.Save();
            }
        }

        public void OnSfxSliderChanged()
        {
            if (audioManager)
            {
                audioManager.SetVolume(AudioSourceType.Sfx, sfxSlider.value);
                PlayerPrefs.SetFloat(SFX_VOLUME, sfxSlider.value);
                PlayerPrefs.Save();
            }
        }
    }
}
