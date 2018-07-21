using System.Windows.Controls;

namespace WinPartyArs.Views
{
    public partial class ServerList : UserControl
    {
        public ServerList() => InitializeComponent();

        //private void ServersHeaderSort_Click(object sender, RoutedEventArgs e)
        //{
        //    if (e.OriginalSource is GridViewColumnHeader h && h.Column?.DisplayMemberBinding is Binding b && DataContext is ServerListViewModel vm)
        //    {
        //        var direction = ListSortDirection.Ascending;
        //        if (vm.Servers?.SortDescriptions.Count == 1)
        //        {
        //            var oldSd = vm.Servers?.SortDescriptions[0];
        //            if (oldSd.HasValue && oldSd.Value.PropertyName == b.Path.Path && oldSd.Value.Direction == ListSortDirection.Ascending)
        //                direction = ListSortDirection.Descending;
        //        }
        //        vm.SortCommand?.Execute(new SortDescription(b.Path.Path, direction));
        //    }
        //}
    }
}
