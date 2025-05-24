namespace Battle.Units.Interactors
{
    public struct Response
    {
        public bool Success;
        public string Message;

        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}