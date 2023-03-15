using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private float dampedTime = 0.25f;
        [SerializeField] private string groundedParameter = "Grounded";
        [SerializeField] private string rightFootValueParameter = "RightFootValue";
        [SerializeField] private string leftFootOnGroundParameter = "IsLeftFootOnGround";
        
        private Animator _animator;
        
        private int _groundedKey;
        private int _rightFootValueKey;
        private int _leftFootOnGroundKey;
        
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            
            _groundedKey = Animator.StringToHash(groundedParameter);
            _rightFootValueKey = Animator.StringToHash(rightFootValueParameter);
            _leftFootOnGroundKey = Animator.StringToHash(leftFootOnGroundParameter);
        }

        private void Update()
        {
            
            if(_animator.GetBool(_groundedKey))
            {
                var value = _animator.GetFloat(_rightFootValueKey);
                _animator.SetBool(_leftFootOnGroundKey, value < 0.5f);
            }
                
        }

        public void SetBool(int key, bool value)
        {
            _animator.SetBool(key, value);
        }

        public void SetFloat(int key, float value)
        {
            _animator.SetFloat(key, value);
        }
        
        public void SetFloatSmooth(int key, float value, float? dampTime = null)
        {
            dampTime ??= dampedTime;
            _animator.SetFloat(key, value, dampTime.Value, Time.deltaTime);
        }

        public bool IsInTransition(int layerIndex)
        {
            return _animator.IsInTransition(layerIndex);
        }
    }
}