using UnityEngine;

namespace TankGame
{
    public class SceneManager : MonoSingletonGeneric<SceneManager>
    {
        [SerializeField] private int mainMenuIndex;
        
        public void LoadLevel(int index)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        }

        public void LoadMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuIndex);
        }

        public void Quit()
        {
            Application.Quit();    
        }

        public void Restart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
           
    }
}
