using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class PlayerTankView : MonoBehaviour
    {
        [SerializeField] Slider healthSlider;
        [SerializeField] Image hSliderFillImage;
        [SerializeField] Color fullHealthColor = Color.green;
        [SerializeField] Color zeroHealthColor = Color.red;
        [SerializeField] GameObject explosionPrefab;


        private PlayerTankController playerTankController;
        private TankScriptableObject tankObject;
        private float movementInputValue;
        private float turnInputValue;
        private Rigidbody rigidBody;
        private ParticleSystem explosionEffect;

        internal void SetComponents(PlayerTankController _playerTankController)
        {
            playerTankController = _playerTankController;
        }

        private void Awake()
        {
            explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
            explosionEffect.gameObject.SetActive(false);
        }
        private void Start()
        {
            tankObject = playerTankController.GetTankModel().GetTankObject();
            rigidBody = GetComponent<Rigidbody>();
            playerTankController.SetTankView(this);
            SetHealthUI(tankObject.maxHealth);
        }

        private void FixedUpdate()
        {
            Move();
            Turn();

        }

        private void Move()
        {
            movementInputValue = Input.GetAxis("VerticalUI");
            if (movementInputValue != 0)
            {
                playerTankController.Movement(movementInputValue);

            }

        }
        private void Turn()
        {
            turnInputValue = Input.GetAxis("HorizontalUI");
            if (turnInputValue != 0)
            {
                playerTankController.Rotate(turnInputValue);

            }
        }

        public void TakeDamage(float amount)
        {
            playerTankController.TakeDamage(amount);
        }
        public void SetHealthUI(float health)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = health;
            hSliderFillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, health / tankObject.maxHealth);

        }

        public void OnDeath()
        {
            explosionEffect.gameObject.SetActive(true);
            explosionEffect.gameObject.transform.position = transform.position;
            explosionEffect.Play();
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        }
        public Rigidbody GetRigidbody()
        {
            return rigidBody;
        }

        public void SetTankController(PlayerTankController _controller)
        {
            playerTankController = _controller;
        }
    }
}