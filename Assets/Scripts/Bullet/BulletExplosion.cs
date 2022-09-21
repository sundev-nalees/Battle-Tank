using UnityEngine;

namespace TankGame
{
    public class BulletExplosion : MonoBehaviour
    {
        public LayerMask tankLayer;
        public ParticleSystem shellExplosionParticle;

        private BulletServicePool bulletServicePool;
        private BulletScriptableObject bulletObject;
        private GameObject parent;
        

        

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Tank"))
            {
                ExplosionEffect();
            }
            Collider[] colliders = Physics.OverlapSphere(transform.position, bulletObject.explosionRadius, tankLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
                if (!rb || Vector3.Distance(transform.position, rb.transform.position) > 2f)
                {
                    continue;
                }
                else 
                {
                    
                    Explode(rb);
                }
            }
           
        }

        private void Explode(Rigidbody rb)
        {
            if (rb.gameObject.tag == "Player")
            {
                PlayerTankView playerTankView = rb.gameObject.GetComponent<PlayerTankView>();
                playerTankView.TakeDamage(CalculateDamage(rb.position));
            }
            EnemyTankView enemyTankView = rb.GetComponent<EnemyTankView>();
            if (enemyTankView)
            {
                float damage = CalculateDamage(rb.position);
                enemyTankView.TakeDamage(damage);
            }

            ExplosionEffect();
        }

        private void ExplosionEffect()
        {
            ParticleSystem particleSystem = Instantiate(shellExplosionParticle, this.transform).GetComponent<ParticleSystem>();
            particleSystem.transform.parent = null;
            particleSystem.Play(); 
            Destroy(particleSystem.gameObject, particleSystem.main.duration);
            ReturnToPool();
        }
        float CalculateDamage(Vector3 targetPosition)
        {
            Vector3 explotionToTarget = targetPosition - transform.position;
            float explotionMagnitude = explotionToTarget.magnitude;
            float relativeDamage = (bulletObject.explosionRadius - explotionMagnitude) / bulletObject.explosionRadius;
            float damage = relativeDamage * bulletObject.maxDamage;
            damage = Mathf.Max(0, damage);
            return damage;
        }
        private void ReturnToPool()
        {
            bulletServicePool.ReturnItem(this);
            this.gameObject.SetActive(false);
        }
        public void SetComponents(BulletScriptableObject _object, GameObject _parent,BulletServicePool _bulletServicePool)
        {
            bulletObject = _object;
            parent = _parent;
            bulletServicePool = _bulletServicePool;
        }

    }
}