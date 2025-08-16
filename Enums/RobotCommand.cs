namespace RMSPrivateServerAPI.Enums;

/// <summary>
/// список комманд,
/// которые может отправлять сервер в ответ на завершение операции робота
/// </summary>
public enum RobotCommand
{
    /// <summary>
    /// Перейти к следующей операции
    /// </summary>
    next,
    /// <summary>
    /// Повторить последнюю операцию
    /// </summary>
    repeat,
    /// <summary>
    /// Ждать синхронизации
    /// </summary>
    wait,
    /// <summary>
    /// Отменить текущую задачу
    /// </summary>
    abort,
    /// <summary>
    /// Выполнить непосредственную операцию
    /// </summary>
    exec
}

