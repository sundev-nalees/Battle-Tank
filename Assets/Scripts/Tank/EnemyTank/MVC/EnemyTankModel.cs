using UnityEngine;
namespace TankGame
{
    public class EnemyTankModel
    {
        private TankScriptableObject tankObject;
        private EnemyTankController controller;

        public EnemyTankModel(TankScriptableObject tankScriptableObject)
        {
            tankObject = tankScriptableObject;
        }

        public TankScriptableObject GetTankObject()
        {
            return tankObject;
        }
        public void SetTankController(EnemyTankController _controller)
        {
            controller = _controller;
        }

    }
}