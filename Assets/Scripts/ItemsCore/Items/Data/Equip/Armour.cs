using System;
using System.Collections.Generic;
using System.Linq;
using ItemsCore.Items.Data.Base;
using ItemsCore.Items.Data.Equip.ArmourBuffs;
using ItemsCore.Items.Data.Equip.Types;
using Sirenix.OdinInspector;

namespace ItemsCore.Items.Data.Equip
{
    public class Armour : BaseItemData
    {
        [BoxGroup("Stats", CenterLabel = true)] 
        [LabelWidth(100)] 
        public float armourValue;
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        public float coldResist;
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        public float flameResist;
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        public float poisonResist;
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        public float physicalResist;
        
        [BoxGroup("Stats")] 
        [LabelWidth(100)] 
        public ArmourType type;
        
        [TypeFilter("GetFilteredTypeList")] 
        public BaseArmourBuff[] itemBuff = Array.Empty<BaseArmourBuff>();

        public IEnumerable<Type> GetFilteredTypeList()
        {
            var q = typeof(BaseArmourBuff).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition) 
                .Where(x => typeof(BaseArmourBuff).IsAssignableFrom(x));               
            return q;
        }
    }
}