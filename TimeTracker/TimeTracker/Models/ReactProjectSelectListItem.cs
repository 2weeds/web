namespace TimeTracker.Models
{
    public class ReactProjectSelectListItem : ReactSelectListItem
    {
        public string projectMemberId { get; set; }
        public bool isProjectManager { get; set; }
    }
}