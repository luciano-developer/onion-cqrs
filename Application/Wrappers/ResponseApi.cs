namespace Application.Wrappers
{
    public class ResponseApi
    {
        public ResponseApi()
        {

        }

        public ResponseApi(object data)
        {
            Succeeded = true;
            Message = string.Empty;
            Erros = null;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public object Erros { get; set; }
        public object Data { get; set; }
    }
}
