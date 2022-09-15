using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class ShellService : monoSingletonGeneric<ShellService>
{
    [SerializeField] ShellView shellPrefab;
    //private ShellServicePool shellServicePool;

    private void OnEnable()
    {
        //shellServicePool = GetComponent<ShellServicePool>();
    }

   /* public Rigidbody GetShell(BulletScriptableObject shellObject)
    {
        ShellController shellController = shellServicePool.GetBullet(shellPrefab, shellObject);
        if (shellController != null)
        {
            ShellView shellView = shellController.GetShellView;
            
            return shellView.GetComponent<Rigidbody>();
        }
        return null;
    }

    
    public void ReturnToPool(ShellController shellController)
    {
        shellServicePool.ReturnItem(shellController);
    }
}*/
