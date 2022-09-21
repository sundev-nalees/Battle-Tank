using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class AnyState : TankState
    {
        [SerializeField] private float chaseRadius;
        [SerializeField] private float attackDistance;
        [SerializeField] private LayerMask tankLayer;


        private bool isAttacking = false;
        private bool isChasing = false;
        private float distance;

        private void Update()
        {
            Transform target = TargetInRange();

            if (target != null)
            {
                if (!isChasing && !isAttacking)
                {
                    GetComponent<TankChaseState>().SetTarget(target);
                    tankView.ChangeState(GetComponent<TankChaseState>());
                    isChasing = true;
                }
                distance = Vector3.Distance(transform.position, target.position);
                if (distance < attackDistance && !isAttacking)
                {
                    GetComponent<TankAttackState>().SetTarget(target);
                    tankView.ChangeState(GetComponent<TankAttackState>());
                    isAttacking = true;
                    isChasing = false;
                }
            }
            if (distance > attackDistance && isAttacking)
            {
                tankView.ChangeState(GetComponent<TankChaseState>());
                isAttacking = false;
                isChasing = true;
            }
            if (target == null && isChasing)
            {
                tankView.ChangeState(GetComponent<TankPatrolState>());
                isChasing = false;
            }
        }



        private Transform TargetInRange()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, chaseRadius, tankLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<PlayerTankView>() != null)
                {
                    return colliders[i].transform;
                }
            }
            return null;
        }
    }
}