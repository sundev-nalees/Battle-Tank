using UnityEngine;
using System.Collections.Generic;

namespace TankGame
{
    public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
    {
        [SerializeField] private EnemyTankView enemyPrefab;
        [SerializeField] private TankScriptableObjectList enemyList;
        [SerializeField] private int enemyCount;
        [SerializeField] private Transform[] wayPoint;
        [SerializeField] private float timeBtwSpawns;
        [SerializeField] private float spawnDelay;


        private float timer;
        private int spawnPointIndex;
        private List<EnemyTankController> enemyControllers = new List<EnemyTankController>();

        protected override void Awake()
        {
            base.Awake();
            timer = timeBtwSpawns;
            spawnPointIndex = 0;
            for(int i = 0; i < enemyCount; i++)
            {
                SpawnTank();
            }
        }

        private void OnEnable()
        {
            EventManager.Instance.OnEnemyDeath += OnEnemyDeath;
            EventManager.Instance.OnGameOver += OnGameOver;
        }

        private void Update()
        {
            if (timer < Time.time)
            {
                SpawnTank();
                timer = Time.time + timeBtwSpawns;
            }
        }

        private void SpawnTank()
        {
            if (spawnPointIndex == wayPoint.Length - 1)
            {
                spawnPointIndex = 0;
            }
            spawnPointIndex++;
            int index = Random.Range(0, enemyList.tankList.Count);
            EnemyTankController controller = new EnemyTankController(new EnemyTankModel(enemyList.tankList[index]));
            enemyControllers.Add(controller);
            EnemyTankView enemyView = Instantiate(enemyPrefab, wayPoint[spawnPointIndex].position, wayPoint[spawnPointIndex].rotation);
            enemyView.SetController(controller);
            controller.SetTankView(enemyView);
        }

        public Transform[] GetPatrolPoints()
        {
            return wayPoint;
        }

        private void OnEnemyDeath()
        {
            Invoke(nameof(SpawnTank), spawnDelay);
        }

        private void OnGameOver()
        {
            enemyControllers.Clear();
        }

        private void OnDisable()
        {
            EventManager.Instance.OnEnemyDeath -= OnEnemyDeath;
            EventManager.Instance.OnGameOver -= OnGameOver;
        }

        public void RemoveEnemy(EnemyTankController controller)
        {
            _ = enemyControllers.Remove(controller);
        }

        public List<EnemyTankController> GetEnemies()
        {
            return enemyControllers;
        }
    }
}