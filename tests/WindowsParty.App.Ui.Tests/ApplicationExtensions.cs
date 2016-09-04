﻿using System;
using WindowsParty.App.Ui.Tests.Views;
using WindowsParty.Infrastructure;
using TestStack.White;
using TestStack.White.Utility;

namespace WindowsParty.App.Ui.Tests
{
    public static class ApplicationExtensions
    {
        public static LoginView GetLoginView(this Application app)
        {
            var viewName = AppViews.LoginView;
            var view = Retry.For(() => app.GetWindow(viewName), TimeSpan.FromMilliseconds(500)) as LoginView;

            return view;
        }
    }
}
