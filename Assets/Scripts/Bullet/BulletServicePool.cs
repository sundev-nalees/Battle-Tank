using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class BulletServicePool : GenericPool<ShellController>
    {
        private ShellView bulletPrefab;
        private BulletScriptableObject bulletObject;

        private ShellController shellController;
        private ShellModel shellModel;
        private ShellView shellView;
        public ShellController GetBullet(ShellView _bulletPrefab,BulletScriptableObject _bulletObject)
        {
            bulletPrefab = _bulletPrefab;
            bulletObject = _bulletObject;
            shellController = GetItem();
            if (shellController != null && shellController.GetShellView != null)
            {
                shellController.GetShellView.gameObject.SetActive(true);
                return shellController;
            }
            return null;
        }

        public override ShellController CreateItem()
        {
            shellModel = new ShellModel(bulletObject);
            shellController = new ShellController(shellModel);
            shellView = Instantiate(bulletPrefab);
            SetReferences();
            return shellController;
        }

        private void SetReferences()
        {
            shellModel.SetShellController(shellController);
            shellView.SetShellController(shellController);
            shellController.SetShellView(shellView);
        }

      
    }
}
