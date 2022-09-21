using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TankGame
{
    public class TankPatrolState : TankState
    {
        private NavMeshAgent agent;
        private Transform[] wayPoints;
        private int wayPointIndex = 0;
        private Transform target;

        private void OnEnable()
        {

            agent = GetComponent<NavMeshAgent>();

        }
       
        public override void OnEnterState()
        {
            base.OnEnterState();
            wayPoints = tankView.GetWayPoints();

            Patrol();
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }

        private void FixedUpdate()
        {
            if (agent.remainingDistance < 2f)
            {
                tankView.ChangeState(GetComponent<TankIdleState>());
            }
            else if (agent.remainingDistance < 5f && agent.isStopped == true)
            {
                agent.ResetPath();
                agent.SetDestination(target.position);
            }
        }

        public void Patrol()
        {
            IterateWayPointIndex();
            target = wayPoints[wayPointIndex];
            agent.SetDestination(target.position);
        }

        void IterateWayPointIndex()
        {
            int temp;
            do
            {

                temp = Random.Range(0, wayPoints.Length);

                if (tankView == null)
                {
                    Debug.Log("Umbii");
                }
            }
            while (temp == wayPointIndex);
            wayPointIndex = temp;
        }

    }
}