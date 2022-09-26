using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
namespace TankGame
{
    public class PlayerTankController
    {
        private PlayerTankModel tankModel;
        private PlayerTankView tankView;
        private Rigidbody rigidBody;
        private TankScriptableObject tankObject;
        private float currentHealth;
        private bool isDead;



        public PlayerTankController(PlayerTankModel Model)
        {
            tankModel = Model;
            tankModel.SetTankController(this);
            tankObject = tankModel.GetTankObject();
            currentHealth = tankObject.maxHealth;
            isDead = false;

        }
        public void Movement(float movementInputValue)
        {
            if (!rigidBody)
            {
                rigidBody = tankView.GetRigidBody;
            }
            Vector3 movement = tankView.gameObject.transform.forward * movementInputValue * tankObject.movementSpeed * Time.deltaTime;

            rigidBody.MovePosition(rigidBody.position + movement);
        }
        public void Rotate(float turnInputValue)
        {
            if (!rigidBody)
            {
                rigidBody = tankView.GetRigidBody;
            }
            float turn = turnInputValue * tankObject.turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
        }
        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            tankView.SetHealthUI(currentHealth);
            if (currentHealth <= 0 && !isDead)
            {
                EventManager.Instance.InvokeOnGameOver();
                isDead = true;
                OnDeath();
            }
        }
        async private void OnDeath()
        {
            await DestroyAllEnemies();
            await Task.Delay(TimeSpan.FromSeconds(1f));

        }
        async private Task DestroyAllEnemies()
        {
            List<EnemyTankController> enemies = EnemyTankService.Instance.GetEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].OnDeath();
            }
            await Task.Yield();
        }
        public void Fire(Vector3 velocity)
        {
            EventManager.Instance.InvokeOnBulletFired();
            ShellService.Instance.GetShell(tankView.GetBulletScriptableObject, tankView.GetFireTransform, velocity);

        }
        public PlayerTankModel GetTankModel()
        {
            return tankModel;
        }

        public void SetPlayerTankView(PlayerTankView _view)
        {
            tankView = _view;
        }
        
    }
}