namespace ErrorHandling;

public class Task5
{
    public void Run()
    {
        Task4 task4 = new Task4();
        try
        {
            task4.Run();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
