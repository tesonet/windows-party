using System.Collections.ObjectModel;
using TheHaveFunApp.Enums;
using TheHaveFunApp.Extensions;
using TheHaveFunApp.Models;

namespace TheHaveFunApp.Collections
{
    public class ServersList : ObservableCollection<ServerModel>
    {
        private SortType _sortedByDistance = SortType.Default;
        private SortType _sortedByName = SortType.Default;

        public void SortByProperty(string column)
        {
            if (column == "name")
            {
                if (_sortedByName == SortType.Default || _sortedByName == SortType.Desc)
                {
                    this.Sort((x, y) => x.Name.CompareTo(y.Name));
                    _sortedByName = SortType.Asc;
                }
                else
                {
                    this.Sort((x, y) => y.Name.CompareTo(x.Name));
                    _sortedByName = SortType.Desc;
                }
            }
            else
            {
                if (_sortedByDistance == SortType.Default || _sortedByDistance == SortType.Desc)
                {
                    this.Sort((x, y) => x.Distance.CompareTo(y.Distance));
                    _sortedByDistance = SortType.Asc;
                }
                else
                {
                    this.Sort((x, y) => y.Distance.CompareTo(x.Distance));
                    _sortedByDistance = SortType.Desc;
                }
            }
        }
    }
}
