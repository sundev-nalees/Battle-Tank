using UnityEngine;
using UnityEngine.AI;

namespace TankGame {
    public class TankAttackState : TankState
    {
        [SerializeField] private float bulletSpeed;
        [SerializeField] private Transform fireTransform;
        [SerializeField] private BulletExplosion shell;
        [Range(0, 5)]
        [SerializeField] private float timeBtwFire;
        [SerializeField] private BulletScriptableObject bulletObject;
        [SerializeField] private float facePlayerSmoothness;


        private BulletServicePool bulletServicePool;
        private NavMeshAgent agent;
        private Vector3 agentVelocity;
        private float timer;
        private Transform target;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            bulletServicePool = GetComponent<BulletServicePool>();
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            agentVelocity = agent.velocity;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            
        }
        private void Start()
        {
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
            BulletExplosion shellInstance = bulletServicePool.GetBullet(shell, fireTransform);
            shellInstance.SetComponents(bulletObject,this.gameObject,bulletServicePool);
            shellInstance.GetComponent<Rigidbody>().velocity = bulletSpeed * fireTransform.forward;
        }
        

        public void SetTarget(Transform _target)
        {
            target = _target;
        }
    }
}