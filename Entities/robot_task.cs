using RMSPrivateServerAPI.Models;
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
    public string? TaskId { get; set; }

    /// <summary>
    /// ���������� ������������� ������, �������� ��������� ������.
    /// </summary>
    [ForeignKey("RobotId")]
    public string? RobotId { get; set; }
    
    /// <summary>
    /// ���������������� �������� ������.
    /// </summary>
    public string? Title { get; set; }
    
}
