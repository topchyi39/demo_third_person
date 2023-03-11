using UnityEngine;

namespace Player
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private float dampedTime = 0.25f;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
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