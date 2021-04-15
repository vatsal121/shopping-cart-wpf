using System;

namespace Utility.Monads
{
    public class Result
    {
        public bool Successful => ErrorMessage is null;
        public string ErrorMessage { get; }

        protected Result(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return (Successful) ? nameof(Successful) : $"Error: {ErrorMessage}";
        }

        public static Result Success() => new Result(null);

        public static Result Error(string errorMessage)
        {
            if (errorMessage is null)
                throw new ArgumentNullException(nameof(errorMessage), "Error message must not be null in an Error Result.");
            return new Result(errorMessage);
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; }

        protected Result(T data, string errorMessage)
            : base(errorMessage)
        {
            Data = data;
        }

        public override string ToString()
        {
            return (Successful) ? $"{nameof(Successful)}: {Data}" : $"Error: {ErrorMessage}";
        }

        public static Result<T> Success(T data)
            => new Result<T>(data, null);

        public new static Result<T> Error(string errorMessage)
        {
            if (errorMessage is null)
                throw new ArgumentNullException(nameof(errorMessage), "Error message must not be null in an Error Result.");
            return new Result<T>(default, errorMessage);
        }
    }
}
