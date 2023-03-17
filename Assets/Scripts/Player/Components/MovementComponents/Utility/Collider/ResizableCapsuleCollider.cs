﻿using UnityEngine;

namespace Player.Components.MovementComponents.Utility.Collider
{
    public class ResizableCapsuleCollider : MonoBehaviour
    {
        [SerializeField] private DefaultColliderData defaultColliderData;
        [SerializeField] private SlopeData slopeData;
        
        public DefaultColliderData DefaultColliderData => defaultColliderData;
        public SlopeData SlopeData => slopeData;
        public CapsuleColliderData CapsuleColliderData { get; private set; }
        
        public Vector3 WorldCapsuleCentre => CapsuleColliderData.Collider.bounds.center;
        public Vector3 LocalCapsuleCentre => CapsuleColliderData.ColliderCenterInLocalSpace;
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
            if (CapsuleColliderData != null)
            {
                return;
            }

            CapsuleColliderData = new CapsuleColliderData();

            CapsuleColliderData.Initialize(gameObject);

            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        public void CalculateCapsuleColliderDimensions()
        {
            if(DefaultColliderData == null) return;
            
            SetCapsuleColliderRadius(DefaultColliderData.Radius);

            SetCapsuleColliderHeight(DefaultColliderData.Height * (1f - SlopeData.StepHeightPercentage));

            RecalculateCapsuleColliderCenter(DefaultColliderData.Height, DefaultColliderData.CenterY);

            RecalculateColliderRadius();

            CapsuleColliderData.UpdateColliderData();
        }

        public void UpdateSetting(CapsuleSettings settings)
        {
            SetCapsuleColliderHeight(settings.Height * (1f - SlopeData.StepHeightPercentage));
            RecalculateCapsuleColliderCenter(settings.Height, settings.Center);
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

        public void RecalculateCapsuleColliderCenter(float height, float center)
        {
            float colliderHeightDifference = height - CapsuleColliderData.Collider.height;

            Vector3 newColliderCenter = new Vector3(0f, center + (colliderHeightDifference / 2f), 0f);

            CapsuleColliderData.Collider.center = newColliderCenter;
        }

        public void RecalculateColliderRadius()
        {
            float halfColliderHeight = CapsuleColliderData.Collider.height / 2f;

            if (halfColliderHeight >= CapsuleColliderData.Collider.radius)
            {
                return;
            }

            SetCapsuleColliderRadius(halfColliderHeight);
        }
        
    }
}