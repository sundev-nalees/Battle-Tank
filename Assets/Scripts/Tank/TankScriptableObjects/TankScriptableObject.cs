using UnityEngine;
namespace TankGame
{
    public enum TankType
    {
        None,
        Red,
        Blue,
        Green
    }

    [CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObject/TankScriptableObject")]
    public class TankScriptableObject : ScriptableObject
    {
        public Color tankColor;
        public float movementSpeed;
        public float turnSpeed;
        public float maxHealth = 100f;
        public float healthSliderTimer = 2f;
    }
}