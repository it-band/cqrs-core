namespace CQRS.Models
{
    public class Result
    {
        public bool IsSuccess => Failure == null;

        public Failure Failure { get; }        

        public Result(Failure failure)
        {     
            Failure = failure;
        }

        public Result()
        {
        }

        public static Failure Fail(string message)
        {
            return new Failure(message);
        }

        public static Failure NotFound(string message)
        {
            return new NotFoundFailure(message);
        }

        public static Failure Forbidden(string message)
        {
            return new ForbiddenFailure(message);
        }

        public static Failure ValidationError(ValidationError validationError)
        {
            return new ValidationFailure(validationError);
        }

        public static Failure ValidationError(ValidationError[] validationErrors)
        {
            return new ValidationFailure(validationErrors);
        }

        public static implicit operator Result(Failure failure) => new Result(failure);
    }

    public class Result<TObject> : Result
    {
        public TObject Data { get; set; }

        public Result()
        {
        }

        public Result(TObject data)
        {
            Data = data;
        }

        public Result(Failure failure) : base(failure)
        {
        }

        public static implicit operator Result<TObject>(TObject value)
        {
            return new Result<TObject>(value);
        }

        public static implicit operator Result<TObject>(Failure failure) => new Result<TObject>(failure);
    }
}
