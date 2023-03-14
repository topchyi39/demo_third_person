using UnityEngine;

namespace MobsCore.Mobs.Data.Base
{
    public abstract class BaseMobData : ScriptableObject
    {
        public int id;
        public int name;
        public int description;
    }
}