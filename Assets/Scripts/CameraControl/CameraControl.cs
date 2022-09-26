using UnityEngine;
using System.Collections.Generic;
namespace TankGame
{
    public class CameraControl : MonoSingletonGeneric<CameraControl>
    {
        private List<Transform> targets = new List<Transform>();

        [SerializeField] private float dampTime = 0.2f;
        [SerializeField] private float targetAdditionalDampTime = 1.5f;
        [SerializeField] private float screenEdgeBuffer = 4f;
        [SerializeField] private float MinSize = 6.5f;
        [SerializeField] private float MaxSize = 10f;

        private Camera mainCamera;

        private float orginalDampTime;
        private float zoomSpeed;
        private Vector3 moveVelocity;
        private Vector3 desiredPosition;

        private bool isGameOver;

        protected override void Awake()
        {
            base.Awake();
            mainCamera = GetComponentInChildren<Camera>();
            orginalDampTime = dampTime;
            isGameOver = false;
        }

        private void Start()
        {
            EventManager.Instance.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnGameOver += GameOver;
        }

        private void FixedUpdate()
        {
            Move();
            Zoom();

            if (dampTime != orginalDampTime && !isGameOver)
            {
                ResetDampTime();
            }
        }

        private void GameOver()
        {
            isGameOver = true;
            RemoveAllCameraTargetPositions();
        }

        private void Move()
        {
            FindAveragePosition();
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);

        }

        private void FindAveragePosition()
        {
            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            for(int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    continue;
                }
                averagePos += targets[i].position;
                numTargets++;
            }

            if (numTargets > 0)
            {
                averagePos /= numTargets;
            }
            averagePos.y = transform.position.y;
            desiredPosition = averagePos;
        }

        private void Zoom()
        {
            float requiredSize = FindRequiredSize();
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, requiredSize, ref zoomSpeed, dampTime);

        }

        private float FindRequiredSize()
        {
            Vector3 desiredLocalPosition = transform.InverseTransformPoint(desiredPosition);
            float size = 0f;
            for(int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {

                    continue;
                }
                Vector3 targetLocalPosition = transform.InverseTransformPoint(targets[i].position);

                Vector3 desiredPositionToTarget = targetLocalPosition - desiredLocalPosition;

                size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.y));

                size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.x) / mainCamera.aspect);

            }

            size = Mathf.Min(size, MaxSize);
            size += screenEdgeBuffer;

            size = Mathf.Max(size, MinSize);
            return size;
        }

        public void AddCameraTargetPosition(Transform target)
        {
            dampTime = targetAdditionalDampTime;
            targets.Add(target);
        }
        
        public void RemoveCameraTargetPosition(Transform target)
        {
            if (!isGameOver)
            {
                dampTime = targetAdditionalDampTime;
            }
            targets.Remove(target);
        }
        
        public void RemoveAllCameraTargetPositions()
        {
            for(int i = 0; i < targets.Count; i++)
            {
                targets.Remove(targets[i]);
            }
        }
        
        private void ResetDampTime()
        {
            dampTime = Mathf.Lerp(dampTime, orginalDampTime, Time.deltaTime);
        }
    } 
}