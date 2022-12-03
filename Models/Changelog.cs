using System.ComponentModel.DataAnnotations;

namespace RDPDocumentationWebAPI.Models
{
    public class Changelog
    {
        public long Id { get; set; }

        private string m_release;

        [Required]
        private List<ChangeLineItem>? m_changeLineItems;

        private TeamEnum? m_team;

        public string Release { get { return m_release; } set { m_release = value; } }

        [Required]
        public List<ChangeLineItem>? ChangeLineItems { get { return m_changeLineItems; } set { m_changeLineItems = value; } }

        public TeamEnum? Team { get { return (TeamEnum)m_team; } set { m_team = value; } }

        public Changelog()
        {
            m_release = "";
            m_changeLineItems = new List<ChangeLineItem>();
            m_team = TeamEnum.Zillion;
        }

        public Changelog(string release, List<ChangeLineItem> changeLineItems, TeamEnum? team)
        {
            Release = release;
            ChangeLineItems = changeLineItems;
            Team = team;
        }

        public void AddChangeLineItem()
        {
            ChangeLineItems.Add(new ChangeLineItem());
        }

        public void AddChangeLineItem(ChangeLineItem c)
        {
            ChangeLineItems.Add(c);
        }
    }
}
