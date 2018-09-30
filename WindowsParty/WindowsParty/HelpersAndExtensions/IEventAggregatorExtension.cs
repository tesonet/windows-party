using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsParty.HelpersAndExtensions
{
    public static class IEventAggregatorExtension
    {
        public static void ChangeScreen<TScreen>(this IEventAggregator eventAggregator)
            where TScreen : Screen
        {
            eventAggregator.PublishOnUIThread(typeof(TScreen));
        }
    }
}
