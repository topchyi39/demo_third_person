using UnityEngine;

namespace Player.Utility.Collider
{
    public class ResizableCapsuleCollider : MonoBehaviour
    {
        [SerializeField] private DefaultColliderData defaultColliderData;
        [SerializeField] private SlopeData slopeData;
        [SerializeField] private PlayerTriggerColliderData triggerColliderData;
        

        public CapsuleColliderData CapsuleColliderData { get; private set; }

        public DefaultColliderData DefaultColliderData => defaultColliderData;
        public SlopeData SlopeData => slopeData;
        public PlayerTriggerColliderData PlayerTriggerColliderData => triggerColliderData;

        public Vector3 WorldCenter => CapsuleColliderData.Collider.bounds.center;
        public Vector3 LocalCenter => CapsuleColliderData.ColliderCenterInLocalSpace;
        private void Awake()
        {
            Resize();
        }

        private void OnValidate()
        {
            Resize();
        }

        public void Resize()
        {
            Initialize(gameObject);

            CalculateCapsuleColliderDimensions();
        }

        public void Initialize(GameObject gameObject)
        {
            if (CapsuleColliderData != null) return;

            CapsuleColliderData = new CapsuleColliderData();

            CapsuleColliderData.Initialize(gameObject);
            triggerColliderData.Initialize();
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        public void CalculateCapsuleColliderDimensions()
        {
            SetCapsuleColliderRadius(defaultColliderData.Radius);

            SetCapsuleColliderHeight(defaultColliderData.Height * (1f - slopeData.StepHeightPercentage));

            RecalculateCapsuleColliderCenter();

            RecalculateColliderRadius();

            CapsuleColliderData.UpdateColliderData();
        }

        public void SetCapsuleColliderRadius(float radius)
        {
            CapsuleColliderData.Collider.radius = radius;
        }

        public void SetCapsuleColliderHeight(float height)
        {
            CapsuleColliderData.Collider.height = height;
        }

        public void RecalculateCapsuleColliderCenter()
        {
            var colliderHeightDifference = defaultColliderData.Height - CapsuleColliderData.Collider.height;

            var newColliderCenter = new Vector3(0f, defaultColliderData.CenterY + colliderHeightDifference / 2f, 0f);

            CapsuleColliderData.Collider.center = newColliderCenter;
        }

        public void RecalculateColliderRadius()
        {
            var halfColliderHeight = CapsuleColliderData.Collider.height / 2f;

            if (halfColliderHeight >= CapsuleColliderData.Collider.radius) return;

            SetCapsuleColliderRadius(halfColliderHeight);
        }
    }
}