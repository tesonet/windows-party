using System;

namespace ServerLocator.Client
{
    public class ChangeViewMessage
    {
        public Type ViewModelType { get; private set; }

        public ChangeViewMessage(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }
}
