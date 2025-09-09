namespace FilesAndStreams.UserInterface;

public class ProgressBar
{
    public string TaskName { get; set; }
    public int Progress { get; set; }
    public long ElapsedTime { get; set; }
    public bool IsStatic { get; set; }
    public int LineIndex { get; set; }
}
