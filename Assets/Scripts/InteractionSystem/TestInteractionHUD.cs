using DefaultNamespace.InteractionSystem.Subjects;
using UnityEngine;

namespace InteractionSystem
{
    public class TestInteractionHUD : MonoBehaviour, IInteractionSubject
    {
        public void Interact()
        {
            Debug.Log("Interact");
        }
    }
}