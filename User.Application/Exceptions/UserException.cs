namespace User.Application.Exceptions;

/// <summary>
///     Класс для вывода ошибки
/// </summary>
/// <param name="msg">Сообщение ошибки</param>
public class UserException(string msg = "") : Exception(msg);