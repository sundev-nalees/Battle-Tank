
namespace TankGame
{
    public class ShellModel
    {
        private BulletScriptableObject bulletObject;
        private ShellController shellController;

        public ShellModel(BulletScriptableObject _object)
        {
            bulletObject = _object;
        }

        public BulletScriptableObject GetBulletObject { get { return bulletObject; } }

        public void SetShellController(ShellController _conroller)
        {
            shellController = _conroller;
        }
    }
}