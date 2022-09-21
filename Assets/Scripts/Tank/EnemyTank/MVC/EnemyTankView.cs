using UnityEngine;
using UnityEngine.UI;
using System;

namespace TankGame
{
    public class EnemyTankView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image hSliderFillImage;
        [SerializeField] private Color fullHealthColor = Color.green;
        [SerializeField] private Color zeroHealthColor = Color.red;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private TankState defaultState;


        private ParticleSystem explosionEffect;
        private Transform[] wayPoint;
        private EnemyTankController controller;
        private TankScriptableObject tankObject;
        private TankState currentState;

        public static Action OnEnemyDeath;

        private void Awake()
        {
            explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
            explosionEffect.gameObject.SetActive(false);
        }
        void Start()
        {
            controller.SetTankView(this);
            SetColor();
            SetHealthUI(tankObject.maxHealth);
            currentState = defaultState;
            currentState.OnEnterState();
        }

        public void TakeDamage(float amount)
        {
            controller.TakeDamage(amount);
        }


        void SetColor()
        {
            Transform tankTurrent = gameObject.transform.Find("TankRenderers/TankTurret");
            Transform tankChassis = gameObject.transform.Find("TankRenderers/TankTurret");
            tankTurrent.gameObject.GetComponent<Renderer>().material.color = tankObject.tankColor;
            tankChassis.gameObject.GetComponent<Renderer>().material.color = tankObject.tankColor;
        }

        public void SetHealthUI(float _health)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = _health;
            hSliderFillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, _health / tankObject.maxHealth);
            Invoke("SetUiInactive", tankObject.healthSliderTimer);
        }

        public void ChangeState(TankState newState)
        {
            currentState.OnExitState();
            newState.OnEnterState();
            currentState = newState;

        }
        private void SetUiInactive()
        {
            healthSlider.gameObject.SetActive(false);
            explosionEffect.gameObject.SetActive(false);
        }
        public void OnDeath()
        {
            OnEnemyDeath?.Invoke();
            explosionEffect.gameObject.SetActive(true);
            explosionEffect.gameObject.transform.position = transform.position;
            explosionEffect.Play();

        }

        public void SetComponents(EnemyTankController _controller, TankScriptableObject _tank, Transform[] _points)
        {
            controller = _controller;
            tankObject = _tank;
            wayPoint = _points;
        }

        public Transform[] GetWayPoints()
        {
            return wayPoint;
        }
        public Rigidbody GetRigidbody { get { return GetComponent<Rigidbody>(); } }
    }
}