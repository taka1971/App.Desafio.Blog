using App.Desafio.Blog.Domain.Enums;

namespace App.Desafio.Blog.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainErrorCode ErrorCode { get; }

        public DomainException(DomainErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
