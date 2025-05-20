public class StatusFilter
{
    public string? SelectedStatus { get; set; }
    public List<string> StatusOptions { get; set; } = new() { "Paid", "Pending" };
}
