using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TheHaveFunApp.Extensions
{
    public static class ObservableCollectionExtensions
    {
        //public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> collectionToAdd)
        //{
        //    foreach (var item in collectionToAdd)
        //    {
        //        collection.Add(item);
        //    }
        //}

        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);
            sortableList.Sort(comparison);

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }
    }
}
