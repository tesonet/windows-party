// <copyright file="AppWindowManager.cs" company="Steve Martin">
// Copyright (c) Steve Martin, 2012. All Right Reserved.
// </copyright>
// <license Name="Modified MIT License"
// Source="http://kihonkai.com/content/software-license-sample-code">
// Full text in LICENSE.TXT file.
// </license>
namespace CaliburnMicro.LoginTestExternal.Framework
{
    using System.Collections.Generic;
    using System.Windows;

    using Caliburn.Micro;

    /// <summary>
    /// Extends the Caliburn.Micro.WindowManager class to force all windows
    /// to use SizeToContent.Manual.
    /// </summary>
    public class AppWindowManager : WindowManager
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppWindowManager"/> class.
        /// </summary>
        public AppWindowManager()
            : base()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Shows a window for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The optional window settings.</param>
        public override void ShowWindow(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            base.ShowWindow(rootModel, context, settings);
        }

        /// <summary>
        /// Makes sure the view is a window is is wrapped by one.
        /// </summary>
        /// <param name="model">The view model.</param>
        /// <param name="view">The view.</param>
        /// <param name="isDialog">Whethor or not the window is being shown as a dialog.</param>
        /// <returns>
        /// The window configured window or a simple wrapper window.
        /// </returns>
        protected override Window EnsureWindow(object model, object view, bool isDialog)
        {
            Window window = base.EnsureWindow(model, view, isDialog);

            window.SizeToContent = SizeToContent.Manual;

            return window;
        }

        #endregion Methods
    }
}