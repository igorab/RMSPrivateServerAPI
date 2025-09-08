using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS1591, IDE1006

namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// ������������ ������, ����������� ������, ������� � ������������� � ������ ��������.
/// </summary>
[Table("RobotTask")]
public class robot_task
{
    /// <summary>
    /// ���������� ������������� ������ � ������� RMS.
    /// </summary>
    [Key]
    public Guid TaskId { get; set; }

    /// <summary>
    /// ���������� ������������� ������, �������� ��������� ������.
    /// </summary>
    //[ForeignKey("RobotId")]
    public Guid RobotId { get; set; }
    
    /// <summary>
    /// ���������������� �������� ������.
    /// </summary>
    public string? Title { get; set; }
    
}
