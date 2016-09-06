using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PartyApp.Test.Framework
{
	public class PropertyChangeTracker
	{
		private List<string> m_Notifications = new List<string>();

		public PropertyChangeTracker(
			INotifyPropertyChanged changer,
			string propertyName,
			Action changeAction)
		{
			changer.PropertyChanged += (o, e) =>
			{
				if (propertyName == e.PropertyName)
					m_Notifications.Add(e.PropertyName);
			};

			changeAction();
			if (m_Notifications.Contains(propertyName))
				HasChanged = true;
		}

		public bool HasChanged { get; }
	}
}
