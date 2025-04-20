namespace NSE.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public int ErroStatus { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }

    public class ErrorResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
    }

    public class ResponseErrorMessages
    {
        public List<string> Mensagens { get; set; }
    }
}
