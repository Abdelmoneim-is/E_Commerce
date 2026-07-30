using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public class Result
    {
        public bool IsSucess { get; }
        public IReadOnlyList<Error> Errors { get;}

        public Result(bool issucess , IReadOnlyList<Error> errors)
        {
            IsSucess = issucess;
            Errors = errors;
        }

        public static Result Ok => new Result(true, Array.Empty<Error>());
        public static Result Fail(Error error) => new Result(false, new[] { error });
        public static Result Fail (IReadOnlyList<Error> errors) => new Result(false , errors);
    }

    public class Result<Tvalue> : Result
    {
        private readonly Tvalue _value;
        public Tvalue data => IsSucess ? _value : throw new InvalidOperationException("Can Not Access Value Of Failed Result");
        private Result(Tvalue value) : base(true, Array.Empty<Error>())
        {
            _value = value;
        }

        private Result (Error error) : base(false , new[] { error})
        {
            _value = default!;
        }

        private Result(IReadOnlyList<Error> errors) : base(false , errors)
        {
            _value = default!;
        }

        public static Result<Tvalue> Ok(Tvalue value) => new Result<Tvalue>(value);
        public static Result<Tvalue> Fail(Error error) => new Result<Tvalue>(error);
        public static Result<Tvalue> Fail(IReadOnlyList<Error> errors) => new Result<Tvalue>(errors);


        public static implicit operator Result<Tvalue>(Tvalue value) => Ok(value);
        public static implicit operator Result<Tvalue>(Error error) => Fail(error);
    }
}
