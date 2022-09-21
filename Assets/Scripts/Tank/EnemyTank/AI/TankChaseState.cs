using UnityEngine;
using UnityEngine.AI;

namespace TankGame
{
    public class TankChaseState : TankState
    {
        private Transform target;

        public override void OnEnterState()
        {
            base.OnEnterState();
            GetComponent<NavMeshAgent>().SetDestination(target.position);
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }

        public void SetTarget(Transform _target)
        {
            target = _target;
        }
    }
}