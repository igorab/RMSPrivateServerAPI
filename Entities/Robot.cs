namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// Справочник роботов (id и прочие данные, характеризующие конкретный экземпляр робота)
/// </summary>
public class Robot
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // Другие свойства, характеризующие робота >>

    // Другие свойства, характеризующие робота <<
}
