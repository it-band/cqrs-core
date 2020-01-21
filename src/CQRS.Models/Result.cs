using Microsoft.AspNetCore.Http;

namespace CQRS.Models
{
    public class Result
    {
        public bool IsSuccess => Failure == null;

        public Failure Failure { get; set; }        

        public Result(Failure failure)
        {     
            Failure = failure;
        }

        public Result()
        {
        }

        public static Result Success()
        {
            return new Result();
        }

        public static Failure Fail(string message)
        {
            return new Failure(message);
        }

        public static Failure NotFound(string message)
        {
            return new NotFoundFailure(message);
        }

        public static Failure Unauthorized(string message)
        {
            return new UnauthorizedFailure(message);
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

    public static class ResultExtensions
    {
        public static int GetStatusCode(this Result result)
        {
            if (result.IsSuccess)
            {
                return StatusCodes.Status200OK;
            }

            switch (result.Failure)
            {
                case ExceptionFailure _:
                    return StatusCodes.Status500InternalServerError;
                case UnauthorizedFailure _:
                    return StatusCodes.Status401Unauthorized;
                case ForbiddenFailure _:
                    return StatusCodes.Status403Forbidden;
                case NotFoundFailure _:
                    return StatusCodes.Status404NotFound;
                default:
                    return StatusCodes.Status400BadRequest;
            }
        }
    }
}
