namespace BookNest.WebAPI.Models.Dtos.Response
{
    public class ServiceResponse<T>
    {
        public bool IsSucess { get; set; }
        public T ResponseData { get; set; }
        public string Error { get; set; }

        public static ServiceResponse<T> Success(T data)
        {
            return new ServiceResponse<T>
            {
                IsSucess = true,
                ResponseData = data

            };
        }

        public static ServiceResponse<T> Fail(string message)
        {
            return new ServiceResponse<T>
            {
                IsSucess = false,
                Error = message
            };
        }
    }

}
