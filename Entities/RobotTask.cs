namespace RMSPrivateServerAPI.Entities
{
    /// <summary>
    /// ������������ ������, ����������� ������, ������� � ������������� � ������ ��������.
    /// </summary>
    public class RobotTask
    {
        /// <summary>
        /// ���������� ������������� ������, �������� ��������� ������.
        /// </summary>
        public int RobotId { get; set; }

        /// <summary>
        /// ���������� ������������� ������ � ������� RMS.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// ���������������� �������� ������.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ������ ��������, ������� ������ ���� ��������� � ������ ���� ������.
        /// </summary>
        public List<Action> Actions { get; set; }
    }

}
