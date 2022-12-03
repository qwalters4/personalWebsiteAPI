using System.ComponentModel.DataAnnotations;

namespace RDPDocumentationWebAPI.Models
{
    public enum ChangeType
    {
        Add = 0,
        Modify = 1,
        Delete = 2,
    }

    public class ChangeLineItem
    {
        [Required]
        public long Id { get; set; }

        private ChangeType m_type;
        private string m_serviceName;
        private string m_description;
        private string m_details;

        public ChangeType Type { get { return m_type; } set { m_type = value; } }
        public string ServiceName { get { return m_serviceName; } set { m_serviceName = value; } }
        public string Description { get { return m_description; } set { m_description = value; } }
        public string Details { get { return m_details; } set { m_details = value; } }

        public ChangeLineItem()
        {
            m_type = ChangeType.Add;
            m_serviceName = "Default Service";
            m_description = "Placeholder for brief description of the change";
            m_details = "More detailed explanation or more granular list of specific changes involved";
        }

        public ChangeLineItem(ChangeType type, string serviceName, string description, string details)
        {
            m_type = type;
            m_serviceName = serviceName;
            m_description = description;
            m_details = details;
        }
    }
}
