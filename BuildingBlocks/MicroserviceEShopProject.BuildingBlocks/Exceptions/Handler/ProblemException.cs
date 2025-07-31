namespace MicroserviceEShopProject.BuildingBlocks.Exceptions.Handler
{
    [Serializable]
    public class ProblemException(string error, string message) : Exception(message)
    {
        public string Error { get; } = error;
        public string Message { get; } = message;
    }
}
