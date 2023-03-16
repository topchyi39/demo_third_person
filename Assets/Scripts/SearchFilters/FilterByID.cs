using System;
using Sirenix.OdinInspector;

namespace SearchFilters
{
    [Serializable]
    public struct FilterByID : ISearchFilterable
    {
        private int id;
        
        public FilterByID(int id)
        {
            this.id = id;
        }
        
        public bool IsMatch(string searchString)
        {
            return id == int.Parse(searchString);
        }
    }
}