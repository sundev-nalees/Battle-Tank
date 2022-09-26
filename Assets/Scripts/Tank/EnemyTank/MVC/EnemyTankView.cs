using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TankGame
{
    public class EnemyTankView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image hSliderFillImage;
        [SerializeField] private Color fullHealthColor = Color.green;
        [SerializeField] private Color zeroHealthColor = Color.red;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private StateType defaultState;
        [SerializeField] private GameObject[] tankBody;
        [SerializeField] private List<State> states;


        private ParticleSystem explosionEffect;
        private EnemyTankController controller;
        private TankScriptableObject tankObject;
        private TankState currentState;
        private StateType currentStateType;
        private bool isAssigned = false;

       
        void Start()
        {
            controller.SetTankView(this);
            tankObject = controller.GetEnemyModel().GetTankObject();
            SetColor();
            SetHealthUI(tankObject.maxHealth);
            ChangeState(defaultState);
            explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
            explosionEffect.gameObject.SetActive(false);
        }

        public void TakeDamage(float amount)
        {
            controller.TakeDamage(amount);
        }


        void SetColor()
        {
            for(int i = 0; i < tankBody.Length; i++)
            {
                tankBody[i].GetComponent<Renderer>().material.color = tankObject.tankColor;
            }
        }

        public void SetHealthUI(float _health)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = _health;
            hSliderFillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, _health / tankObject.maxHealth);
            Invoke("SetUiInactive", tankObject.healthSliderTimer);
        }

        public void ChangeState(StateType type)
        {
            State state = GetState(type);
            if (state.tankState != null)
            {
                if (currentState != null)
                {
                    currentState.OnExitState();
                }

                state.tankState.OnEnterState();
                currentState = state.tankState;
                currentStateType = state.stateType;
            }

            if ((currentStateType == StateType.Chase || currentStateType == StateType.Attack) && !isAssigned)
            {
                isAssigned = true;
                CameraControl.Instance.AddCameraTargetPosition(this.transform);
            }
            if ((currentStateType != StateType.Chase && currentStateType != StateType.Attack) && isAssigned)
            {
                isAssigned = false;
                CameraControl.Instance.RemoveCameraTargetPosition(transform);
            }

        }

        private State GetState(StateType type)
        {
            return states.Find(i => i.stateType == type);
        }
        private void SetUiInactive()
        {
            healthSlider.gameObject.SetActive(false);
            explosionEffect.gameObject.SetActive(false);
        }
        public void OnDeath()
        {
            EventManager.Instance.InvokeOnEnemyDeath();
            if (isAssigned)
            {
                CameraControl.Instance.RemoveCameraTargetPosition(transform);
            }
            if (explosionEffect)
            {
                explosionEffect.gameObject.SetActive(true);
                explosionEffect.gameObject.transform.position = transform.position;
                explosionEffect.Play();
            }
            PlayDeathSound();
            Destroy(gameObject);
            
        }

        private void PlayDeathSound()
        {
            AudioManager audioManager = AudioManager.Instance;
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.TankExplode);
            }
        }

       public void SetController(EnemyTankController _controller)
        {
            controller = _controller;
        }
     
    }

    public enum StateType
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }
    [System.Serializable]
    public struct State
    {
        public StateType stateType;
        public TankState tankState;
    }
}