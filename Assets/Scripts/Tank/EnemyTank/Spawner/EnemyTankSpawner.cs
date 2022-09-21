using UnityEngine;

namespace TankGame
{
    public class EnemyTankSpawner : MonoBehaviour
    {
        public EnemyTankView view;
        public TankScriptableObjectList enemyObjects;
        public int enemyCount;
        public Transform[] wayPoint;


        private EnemyTankController controller;
        private int scriptableObjectIndex;

        private void Start()
        {
            SpawnTank();
        }

        private void SpawnTank()
        {
            for (int i = 0, point = 0; i <= enemyCount; i++, point++)
            {
                if (point == wayPoint.Length)
                {
                    point = 0;
                }
                scriptableObjectIndex = Random.Range(0, enemyObjects.tankList.Length);
                EnemyTankModel model = new EnemyTankModel(enemyObjects.tankList[scriptableObjectIndex], wayPoint);

                controller = new EnemyTankController(model);

                view = GameObject.Instantiate(view, wayPoint[point].position, wayPoint[point].rotation);

                view.SetComponents(controller, enemyObjects.tankList[scriptableObjectIndex], wayPoint);
                controller.SetTankView(view);
            }
        }

    }
}