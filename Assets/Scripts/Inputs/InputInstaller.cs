using UnityEngine;
using Zenject;

namespace Inputs
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private Input input;
        [SerializeField] private InGameInput inGameInput;
        
        
        public override void InstallBindings()
        {
            Container.Bind<IInput>().FromInstance(input).AsSingle();
            Container.Bind<IInGameInput>().FromInstance(inGameInput).AsSingle();
        }
    }
}