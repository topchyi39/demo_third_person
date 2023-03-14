using System;
using Sirenix.OdinInspector;

namespace ItemsCore.Items.Data.Equip.WeaponsBuffs
{
    [HideLabel]
    [Serializable]
    public class FLameWeaponBuff : BaseWeaponBuff
    {
        public float FlameDamagePercent;
        public float FlameDuration;
    }
}