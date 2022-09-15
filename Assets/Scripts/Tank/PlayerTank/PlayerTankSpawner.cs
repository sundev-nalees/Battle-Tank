using UnityEngine;
 public class PlayerTankSpawner : MonoBehaviour
 {
   [SerializeField] private PlayerTankView playerTankView;
       
  [SerializeField] TankScriptableObject playerObject;

        

        

        private void OnEnable()
        {
            CreateTank();
        }

        void CreateTank()
        {
            PlayerTankModel playerTankModel = new PlayerTankModel(playerObject);
            PlayerTankController playerTankController = new PlayerTankController(playerTankModel);
            playerTankView = Instantiate(playerTankView, transform.position, transform.rotation);
            playerTankView.SetComponents(playerTankController);
            playerTankController.SetTankView(playerTankView);
        }
 }
