namespace Shop_Models.Dto
{
    public class ReponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "Thành công";
        public int Count { get; set; } = 0 ;
    }
}
