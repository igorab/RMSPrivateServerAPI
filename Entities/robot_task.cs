using RMSPrivateServerAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS1591, IDE1006

namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// ������������ ������, ����������� ������, ������� � ������������� � ������ ��������.
/// </summary>
[Table("robot_task")]
public class robot_task
{
    /// <summary>
    /// ���������� ������������� ������ � ������� RMS.
    /// </summary>
    [Key]
    public string? task_id { get; set; }

    /// <summary>
    /// ���������� ������������� ������, �������� ��������� ������.
    /// </summary>
    [ForeignKey("robot_id")]
    public string? robot_id { get; set; }
    
    /// <summary>
    /// ���������������� �������� ������.
    /// </summary>
    public string? title { get; set; }
    
}
