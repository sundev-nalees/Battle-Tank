using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController 
{
    private ShellModel shellModel;
    private ShellView shellView;

    public ShellController(ShellModel _model)
    {
        shellModel = _model;
    }
    public void Explode(Rigidbody rb)
    {
        if (rb.gameObject.CompareTag("Player"))
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
    }

    float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explotionToTarget = targetPosition - shellView.transform.position;
        float explotionMagnitude = explotionToTarget.magnitude;
        float relativeDamage = (shellModel.GetBulletObject.explosionRadius - explotionMagnitude)/shellModel.GetBulletObject.explosionRadius;
        float damage = relativeDamage * shellModel.GetBulletObject.maxDamage;
        damage = Mathf.Max(0, damage);
        return damage;
    }

    public void SetShellView(ShellView _view)
    {
        shellView = _view;
    }
    public ShellView GetShellView { get { return shellView; } }

    public ShellModel GetShellModel { get { return shellModel; } }

}
