namespace Domain.Common.Constants;

public class ReplyMessage
{
    private ReplyMessage() { } // Prevent instantiation

    public static class Success
    {
        public const string Query = "Consulta exitosa";
        public const string Save = "Se registró correctamente";
        public const string Update = "Se actualizó correctamente";
        public const string Delete = "Se eliminó correctamente";
        public const string Activate = "El registro ha sido activado";
        public const string Token = "Token generado correctamente";
    }

    public static class Error
    {
        public const string QueryEmpty = "No se encontraron registros";
        public const string Exists = "El registro ya existe";
        public const string Failed = "Operación fallida";
        public const string TokenError = "El usuario y/o contraseña es incorrecta, compruébala";
        public const string InternalServerError = "Error interno del servidor";
        public const string NotFound = "Recurso no encontrado";
        public const string BadRequest = "Solicitud incorrecta";
        public const string Unauthorized = "Acceso no autorizado";
        public const string Forbidden = "Acción no permitida";
    }

    public static class Validate
    {
        public const string ValidateEmail = "El email ya existe";
        public const string ValidatePassword = "Las contraseñas no coinciden";
        public const string ValidateError = "Errores de validación";
    }

    public static class Info
    {
        public const string Logout = "Sesión cerrada correctamente";
        public const string Login = "Sesión iniciada correctamente";
    }
}
