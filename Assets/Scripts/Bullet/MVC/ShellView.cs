using UnityEngine;

namespace TankGame
{
    public class ShellView : MonoBehaviour
    {
        [SerializeField] private LayerMask tankLayer;
        [SerializeField] private ParticleSystem shellExplosionParticle;

        private ShellController shellController;

        private void OnTriggerEnter(Collider other)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, shellController.GetShellModel.GetBulletObject.explosionRadius, tankLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody rb = colliders[i].gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    shellController.Explode(rb);
                }
            }
            ExplosionEffect();
        }

        public void ExplosionEffect()
        {
            ParticleSystem particleSystem = Instantiate(shellExplosionParticle, this.transform).GetComponent<ParticleSystem>();
            PlayExplosionSound();
            particleSystem.transform.parent = null;
            particleSystem.Play();
            ReturnShell(particleSystem);
        }

        private void PlayExplosionSound()
        {
            var instance = AudioManager.Instance;
            if (instance)
            {
                instance.PlaySound(SoundType.ShellExplode);
            }
        }
        private void ReturnShell(ParticleSystem particleSystem)
        {
            Destroy(particleSystem.gameObject, particleSystem.main.duration);
            ShellService.Instance.ReturnToPool(shellController);
            gameObject.SetActive(false);
        }

        public void SetShellController(ShellController _controller)
        {
            shellController = _controller;
        }
    }
}