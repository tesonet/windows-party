namespace tesonet.windowsparty.logging
{
    public interface ILogger
    {
        void Info<T0>(string message, T0 propertyValue0);

        void Info<T0, T1>(string message, T0 propertyValue0, T1 propertyValue1);

        void Info<T0, T1, T2>(string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);

        void Error<T0>(string message, T0 propertyValue0);
    }
}
