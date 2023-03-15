using UnityEngine;

namespace Player.Components.MovementComponents.Data
{
    [CreateAssetMenu(menuName = "Cossack/Movement/MovementData", fileName = "MovementData", order = 0)]
    public class MovementData : ScriptableObject
    {
        [SerializeField] private float baseSpeed;
        [SerializeField] private float maxMagnitudeForWalk;
        [SerializeField] private AnimationCurve slope;
        
        [SerializeField] private GroundedData.GroundedData groundedData;
        [SerializeField] private AirborneData.AirborneData airborneData;
        
        public float BaseSpeed => baseSpeed;
        public float MaxMagnitudeForWalk => maxMagnitudeForWalk;
        public AnimationCurve Slope => slope;
        
        public GroundedData.GroundedData GroundedData => groundedData;
        public AirborneData.AirborneData AirborneData => airborneData;
    }
}