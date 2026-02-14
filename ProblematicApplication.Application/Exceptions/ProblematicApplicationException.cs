namespace ProblematicApplication.Application.Exceptions;

/// <summary>
///     Класс для вывода ошибки
/// </summary>
/// <param name="msg">Сообщение ошибки</param>
public class ProblematicApplicationException(string msg = "") :  Exception(msg);