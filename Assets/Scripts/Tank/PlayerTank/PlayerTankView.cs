using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class PlayerTankView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image hSliderFillImage;
        [SerializeField] private Color fullHealthColor = Color.green;
        [SerializeField] private Color zeroHealthColor = Color.red;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject[] tankBody;
        [SerializeField] private Slider aimSlider;
        [SerializeField] private Transform fireTransform;
        [SerializeField] private BulletScriptableObject bulletObject;


        private PlayerTankController playerTankController;
        private TankScriptableObject tankObject;
        private float movementInputValue;
        private float turnInputValue;
        private Rigidbody rigidBody;
        private ParticleSystem explosionEffect;

        private BulletServicePool bulletServicePool;
        private float chargingSpeed;
        private float fireTimer = 0f;
        private float currentLaunchForce;
        private bool fired = false;

        private void OnEnable()
        {
            EventManager.Instance.OnGameOver += OnDeath;
        }

        private void Start()
        {
            SetTankObject();
            SetColor();
            SetHealthUI(tankObject.maxHealth);
            explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
            explosionEffect.gameObject.SetActive(false);
            currentLaunchForce = bulletObject.minLaunchForce;
            bulletServicePool = GetComponent<BulletServicePool>();
            aimSlider.value = currentLaunchForce;
            chargingSpeed = (bulletObject.maxLaunchForce - bulletObject.minLaunchForce) / bulletObject.maxChargeTime;
            PlayGameSound(SoundType.TankIdel);
            CameraControl.Instance.AddCameraTargetPosition(transform);
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            aimSlider.value = bulletObject.minLaunchForce;
            FireCheck();
            turnInputValue = Input.GetAxis("HorizontalUI");
            movementInputValue = Input.GetAxis("VerticalUI");
           
        }

        private void FireCheck()
        {
            if (fireTimer < Time.time)
            {
                if (currentLaunchForce >= bulletObject.maxLaunchForce && !fired)
                {
                    currentLaunchForce = bulletObject.maxLaunchForce;
                    Fire();
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    fired = false;
                    currentLaunchForce = bulletObject.minLaunchForce;
                }
                else if (Input.GetKey(KeyCode.Mouse0))
                {
                    currentLaunchForce += chargingSpeed * Time.deltaTime;
                    aimSlider.value = currentLaunchForce;
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0) && !fired)
                {
                    Fire();
                }
            }
        }

        private void Fire()
        {
            Vector3 velocity = currentLaunchForce * fireTransform.forward;
            playerTankController.Fire(velocity);
            fired = true;
            fireTimer = Time.time + bulletObject.nextFireDelay;
            currentLaunchForce = bulletObject.minLaunchForce;
        }

        private void FixedUpdate()
        {
            Move();
            Turn();

        }

        private void Turn()
        {
            
            if (turnInputValue != 0)
            {
                playerTankController.Rotate(turnInputValue);

            }
        }

        private void Move()
        {

            if (movementInputValue != 0)
            {
                playerTankController.Movement(movementInputValue);

            }

        }

        private void PlayGameSound(SoundType type)
        {
            var instance = AudioManager.Instance;
            if (instance)
            {
                instance.PlaySound(type);
            }
        }

        public void TakeDamage(float amount)
        {
            playerTankController.TakeDamage(amount);
        }

        private void SetColor()
        {
            for(int i = 0; i < tankBody.Length; i++)
            {
                tankBody[i].GetComponent<Renderer>().material.color = tankObject.tankColor;
            }
        }

        public void SetHealthUI(float health)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = health;
            hSliderFillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, health / tankObject.maxHealth);
            Invoke(nameof(SetUiInactive), tankObject.healthSliderTimer);

        }

        private void SetUiInactive()
        {
            healthSlider.gameObject.SetActive(false);
            explosionEffect.gameObject.SetActive(false);
        }

        public void OnDeath()
        {
            explosionEffect.gameObject.SetActive(true);
            explosionEffect.gameObject.transform.position = transform.position;
            explosionEffect.Play();
            gameObject.SetActive(false);
        }

        public void SetTankController(PlayerTankController _controller)
        {
            playerTankController = _controller;
        }
        
        private void SetTankObject()
        {
            tankObject = playerTankController.GetTankModel().GetTankObject();
        }

        public Rigidbody GetRigidBody { get { return rigidBody; } }

        public Transform GetFireTransform { get { return fireTransform; } }

        public BulletScriptableObject GetBulletScriptableObject { get { return bulletObject; } }

        public TankScriptableObject GetTankObject()
        {
            return tankObject;
        }
        

        private void OnDisable()
        {
            EventManager.Instance.OnGameOver -= OnDeath;
        }
        
    }
}