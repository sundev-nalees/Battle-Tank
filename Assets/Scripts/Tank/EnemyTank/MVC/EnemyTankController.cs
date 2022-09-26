namespace TankGame
{
    public class EnemyTankController
    {
        public EnemyTankModel tankModel;
        public EnemyTankView tankView;

        private TankScriptableObject tankObject;
        private float currentHealth;
        private bool isDead;


        public EnemyTankController(EnemyTankModel _tankModel)
        {
            this.tankModel = _tankModel;
            tankObject = tankModel.GetTankObject();
            currentHealth = tankObject.maxHealth;
            isDead = false;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            tankView.SetHealthUI(currentHealth);
            if (currentHealth <= 0 && !isDead)
            {
                isDead = true;
                OnDeath();

            }

        }

        public void OnDeath()
        {
            tankView.OnDeath();
            
        }

        public void SetTankView(EnemyTankView _tankView)
        {
            tankView = _tankView;
        }

        public EnemyTankModel GetEnemyModel()
        {
            return tankModel;
        }
    }
}