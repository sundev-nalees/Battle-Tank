using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(fileName = "BulletObject", menuName = "ScriptableObject/BulletObject")]
    public class BulletScriptableObject : ScriptableObject
    {

        public TankType color;
        public float maxChargeTime;
        public float minLaunchForce;
        public float maxLaunchForce;
        public float nextFireDelay;
        public float maxDamage;
        public float explosionRadius;
        public float maxLifeTime;
        public float maximumExplosionForce;

    }
}