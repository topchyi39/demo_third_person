using MobsCore.Mobs.Data.Base;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(fileName = "DataContainer/MobsDataContainer", menuName = "DataContainer/MobsDataContainer", order = 0)]
    public class MobsDataContainer : DataContainer<BaseMobData>
    {
        public override void AfterListChanges(CollectionChangeInfo info)
        {
        }

        public override void BeforeListChanges(CollectionChangeInfo info)
        {
        }
    }
}