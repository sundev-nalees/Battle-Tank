using UnityEngine;
using System;
namespace TankGame
{
    public class PlayerTankService : MonoSingletonGeneric<PlayerTankService>
    {
        [SerializeField] private PlayerTankView playerView;
        [SerializeField] private TankScriptableObjectList tankSList;
        [SerializeField] private TankType tankType;

        private TankScriptableObject playerObject;
        private PlayerTankController playerController;
        private PlayerTankModel playerModel;

        protected override void Awake()
        {
            base.Awake();
            CreatePlayerTank();
        }
        
        void CreatePlayerTank()
        {
            
            playerObject = tankSList.tankList.Find(i => i.tankType == tankType);
            if (playerObject)
            {
                
                playerModel = new PlayerTankModel(playerObject);
                playerController = new PlayerTankController(playerModel);
                playerModel.SetTankController(playerController);
                GenerateTankview();
            }
        }

        private void GenerateTankview()
        {
            
            playerView = Instantiate(playerView);
            playerView.SetTankController(playerController);
            playerController.SetPlayerTankView(playerView); 
        }

        public TankScriptableObject GetPlayerObject()
        {
            return playerObject;
        }
    }
}