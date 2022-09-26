using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public sealed class EventManager : MonoSingletonGeneric<EventManager>
    {
        public event Action OnBulletFired;
        public event Action OnEnemyDeath;
        public event Action OnGameOver;

        public void InvokeOnBulletFired()
        {
            OnBulletFired?.Invoke();
        }

        public void InvokeOnEnemyDeath()
        {
            OnEnemyDeath?.Invoke();
        }

        public void InvokeOnGameOver()
        {
            OnGameOver?.Invoke();
        }
    }
}
