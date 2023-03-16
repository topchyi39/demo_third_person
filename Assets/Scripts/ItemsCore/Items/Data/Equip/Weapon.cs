using System;
using System.Collections.Generic;
using System.Linq;
using ItemsCore.Items.Data.Base;
using ItemsCore.Items.Data.Equip.Types;
using ItemsCore.Items.Data.Equip.WeaponsBuffs;
using Sirenix.OdinInspector;

namespace ItemsCore.Items.Data.Equip
{
    [HideLabel]
    [Serializable]
    public class Weapon : BaseItemData
    {
        [BoxGroup("Stats", CenterLabel = true)] 
        [LabelWidth(100)] 
        [ShowInInspector]
        public float damage { get; private set; }
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        [ShowInInspector]
        public float balanceDamage { get; private set; }
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        [ShowInInspector]
        public float attackSpeed { get; private set; }

        [BoxGroup("Stats")] 
        [LabelWidth(100)]
        [ShowInInspector]
        public WeaponType type { get; private set; }

        [BoxGroup("Stats")] 
        [LabelWidth(100)]
        [ShowInInspector]
        public WeaponHandedType hand { get; private set; }

        [BoxGroup("Stats")]
        [LabelWidth(100)]
        [HideLabel, Title("CriticalDamage (%)")]
        [PropertyRange(100, 150)]
        [ShowInInspector]
        public float criticalDamage { get; private set; } = 100;

        [TypeFilter("GetFilteredTypeList")]
        [ShowInInspector]
        public BaseWeaponBuff[] itemBuff { get; private set; }

        [OnInspectorInit]
        private void Init()
        {
            itemBuff ??= Array.Empty<BaseWeaponBuff>();
        }
        
        public IEnumerable<Type> GetFilteredTypeList()
        {
            var q = typeof(BaseWeaponBuff).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(BaseWeaponBuff).IsAssignableFrom(x)); 
            
            return q;
        }
    }
}