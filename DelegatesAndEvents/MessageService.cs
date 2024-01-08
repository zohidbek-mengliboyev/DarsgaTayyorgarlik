public class MessageService
{
    public void OnVideoEncoded(object source, VideoEventArgs e)
    {
        Console.WriteLine("MessageService: Sending a message..." + e.Video.Title);
    }
}