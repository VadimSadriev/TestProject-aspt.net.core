namespace TestProject.Core
{
    public class ApiResponse
    {
        public bool Success => ErrorMessage == null;

        public string ErrorMessage { get; set; }
        public string Message { get; set; }

        public object Response { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public new T Response
        {
            get => (T)base.Response;
            set => base.Response = value;
        }
    }
}
