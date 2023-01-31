namespace Blog.API.ViewModels;

public class Response<T>
{
    public T Data { get; private set; }

    public List<string> Errors { get; private set; } //= new(); OU = new List<string>();
                                                     //posso inicializar assim ao inves de usar o construtor

    public Response(List<string> errors, T data)
    {
        Errors = errors;
        Data = data;
    }

    public Response(List<string> errors)
    {
        Errors = errors;
    }

    public Response(T data)
    {
        Data = data;
    }
    
    public Response(string error)
    {
        Errors.Add(error);
    }
}
