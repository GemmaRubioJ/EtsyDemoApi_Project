using Api.Domain.Enum;
using Api.Infraestructura.Models;

namespace Api.Domain.Response
{
    public class Response
    {
        public StatusType Status { get; set; }
        public string? Message { get; set; }

    }

    public class Response<T> : Response
    {
        public T? Data { get; set; }
    }


    // Clase específica para respuestas que incluyen otros tipos de dato
    public class ResponseProducts : Response<IEnumerable<Product>> { }
    public class ResponseProduct : Response<Product> { }
    public class ResponseUsers : Response <IEnumerable<User>> { }
    public class ResponseUser : Response<User> { }

    public class ResponseUserToken : ResponseUser
    {
        public string Token { get; set; }
    }

    public class ResponseCart : Response<Cart> { }

}
