using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class BulletServicePool : GenericPool<BulletExplosion>
    {
        private BulletExplosion bulletPrefab;
        private Transform fireTransform;

        public BulletExplosion GetBullet(BulletExplosion _bulletPrefab,Transform _fireTranform)
        {
            bulletPrefab = _bulletPrefab;
            fireTransform = _fireTranform;
            BulletExplosion bulletInstance = GetItem();
            bulletInstance.gameObject.SetActive(true);
            bulletInstance.transform.SetPositionAndRotation(fireTransform.position, fireTransform.rotation);
            return bulletInstance;
        }

        public override BulletExplosion CreateItem()
        {
            return Instantiate(bulletPrefab).GetComponent<BulletExplosion>();       
        }
    }
}
