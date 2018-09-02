
namespace CaliburnMicro.LoginTestExternal.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Caliburn.Micro;

    /// <summary>
    /// Event for a login action
    /// </summary>
    public class LogoutEvent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutEvent"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public LogoutEvent(Screen source)
        {
            this.Source = source;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public Screen Source
        {
            get; set;
        }

        #endregion Properties
    }
}