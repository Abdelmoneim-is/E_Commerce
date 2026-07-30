using System.Text.Json.Serialization;

namespace E_Commerce.Application.Common
{
    public sealed record Error (string code , string descriptipn , ErrorType errorType = ErrorType.Failure)
    {
        public static Error Failure(string code = "General.Failure", string description = "General Failure Has Occured") => new Error(code, description, ErrorType.Failure);
        public static Error Validation(string code = "General.Validation", string description = "General Validation Has Occured") => new Error(code, description, ErrorType.Validation);
        public static Error NotFound(string code = "General.NotFound", string description = "Resource Not Found") => new Error(code, description, ErrorType.NotFound);
        public static Error Confilict(string code = "General.Confilict", string description = "General Confilict Has Occured") => new Error(code, description, ErrorType.Confilict);
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "Access Denied Due To Bad Request") => new Error(code, description, ErrorType.Unauhorized);
        public static Error Forbidden(string code = "General.Forbidden", string description = "This Operation Is Forbidden") => new Error(code, description, ErrorType.Forbidden);
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "Provided Credentials Are Invalid") => new Error(code, description, ErrorType.InvalidCredentials);






    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Confilict = 3,
        Unauhorized = 4,
        Forbidden = 5,
        InvalidCredentials = 6
    }
}