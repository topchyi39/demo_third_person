using UnityEngine;

namespace Player.Components.MovementComponents.States.Data
{
    [CreateAssetMenu(menuName = "Cossack/Movement/MovementData", fileName = "MovementData", order = 0)]
    public class MovementData : ScriptableObject
    {
        [SerializeField] private float baseSpeed;
        [SerializeField] private float maxMagnitudeForWalk;
        
        [SerializeField] private MovementStateData walkData;
        [SerializeField] private MovementStateData jogData;


        public float BaseSpeed => baseSpeed;
        public float MaxMagnitudeForWalk => maxMagnitudeForWalk;
        public MovementStateData WalkData => walkData;
        public MovementStateData JogData => jogData;
    }
}