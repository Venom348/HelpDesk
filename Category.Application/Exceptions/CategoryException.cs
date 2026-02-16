namespace Category.Application.Exceptions;

/// <summary>
///     Класс для вывода ошибки
/// </summary>
/// <param name="msg">Сообщение ошибки</param>
public class CategoryException(string msg = "") : Exception(msg);