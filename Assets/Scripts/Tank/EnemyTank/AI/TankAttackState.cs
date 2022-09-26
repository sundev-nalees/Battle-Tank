using UnityEngine;
using UnityEngine.AI;

namespace TankGame {
    public class TankAttackState : TankState
    {
        [SerializeField] private float bulletSpeed;
        [SerializeField] private Transform fireTransform;
        [Range(0, 5)]
        [SerializeField] private float timeBtwFire;
        [SerializeField] private BulletScriptableObject bulletObject;
        [SerializeField] private float facePlayerSmoothness;


        
        private NavMeshAgent agent;
        private Vector3 agentVelocity;
        private float timer;
        private Transform target;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
           
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
            Vector3 velocity = bulletObject.minLaunchForce * fireTransform.forward;
            ShellService.Instance.GetShell(bulletObject, fireTransform, velocity);
        }
        
        public void SetTarget(Transform _target)
        {
            target = _target;
        }
    }
}