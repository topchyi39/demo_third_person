using System;
using System.Collections.Generic;
using System.Linq;
using ItemsCore.Items.Data.Base;
using ItemsCore.Items.Data.Equip.Types;
using ItemsCore.Items.Data.Equip.WeaponsBuffs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ItemsCore.Items.Data.Equip
{
    [HideLabel]
    [Serializable]
    public class Weapon : BaseItemData
    {
        [BoxGroup("Stats", CenterLabel = true)] 
        [LabelWidth(100)] 
        public float damage;
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        public float balanceDamage;
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        public float attackSpeed;

        [BoxGroup("Stats")] 
        [LabelWidth(100)]
        public WeaponType type;

        [BoxGroup("Stats")] 
        [LabelWidth(100)]
        public WeaponHandedType hand;

        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        [HideLabel, Title("CriticalDamage (%)")]
        [Range(100, 150)]
        public float criticalDamage = 100;

        [TypeFilter("GetFilteredTypeList")] 
        public BaseWeaponBuff[] itemBuff = Array.Empty<BaseWeaponBuff>();

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