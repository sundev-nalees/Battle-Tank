using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAttackState : TankState
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Rigidbody shell;
    [Range(0, 5)]
    [SerializeField] private float timeBtwFire;
    [SerializeField] private BulletScriptableObject bulletObject;
    [SerializeField] private float facePlayerSmoothness;

    private NavMeshAgent agent;
    private Vector3 agentVelocity;
    private float timer;
    private Transform target;


    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agentVelocity = agent.velocity;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        timer = 0f;
    }
    public override void OnExitState()
    {
        agent.isStopped = false;
        agent.velocity = agentVelocity;
        base.OnExitState();
    }

    private void Update()
    {
        transform.LookAt(target);
        if (timer <= Time.time)
        {
            Fire();
            timer = Time.time + timeBtwFire;
        }

    }

    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(shell,fireTransform.position,fireTransform.rotation);
        shellInstance.GetComponent<BulletExplosion>().SetComponents(bulletObject, this.gameObject);
        shellInstance.velocity = bulletSpeed * fireTransform.forward;
    }
    private void FacePlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0, direction.y, 0));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, facePlayerSmoothness * Time.deltaTime);
    }


    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
