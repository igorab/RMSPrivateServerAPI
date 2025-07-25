using RMSPrivateServerAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Entities;

/// <summary>
/// ������������ ������, ����������� ������, ������� � ������������� � ������ ��������.
/// </summary>
public class robot_task
{
    /// <summary>
    /// ���������� ������������� ������ � ������� RMS.
    /// </summary>
    [Key]
    public string task_id { get; set; }

    /// <summary>
    /// ���������� ������������� ������, �������� ��������� ������.
    /// </summary>
    [ForeignKey("robotid")]
    public string robot_id { get; set; }
    
    /// <summary>
    /// ���������������� �������� ������.
    /// </summary>
    public string title { get; set; }
    
}
