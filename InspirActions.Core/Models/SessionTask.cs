namespace InspirActions.Core.Models
{
    public record SessionTask
    {
        public AvailableTask Task { get; set; }
        public string Picture { get; set; }
    }
}
