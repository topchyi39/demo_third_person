using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Containers
{
    [InlineEditor]
    [Serializable]
    public abstract class DataContainer<T> : ScriptableObject
    {
        [TypeFilter("GetFilteredTypeList")] 
        [OnCollectionChanged("BeforeListChanges","AfterListChanges")]
        [Searchable]
        [ListDrawerSettings(ShowItemCount = true)]
        [SerializeField] protected List<T> elements;

        public List<T> Elements => elements;

        public virtual void AddElement(T element)
        {
            if(elements.Contains(element)) return;
            
            elements.Add(element);
        }

        public virtual void RemoveElement(T element)
        {
            if(!elements.Contains(element)) return;
            
            elements.Remove(element);
        }

        [OnInspectorInit]
        protected abstract void Initialize();
        
        [OnInspectorDispose]
        protected abstract void Disable();
        
        public abstract void AfterListChanges(CollectionChangeInfo info);
        public abstract void BeforeListChanges(CollectionChangeInfo info);
        
        public IEnumerable<Type> GetFilteredTypeList()
        {
            var q = typeof(T).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(T).IsAssignableFrom(x));
            
            return q;
        }
    }
}