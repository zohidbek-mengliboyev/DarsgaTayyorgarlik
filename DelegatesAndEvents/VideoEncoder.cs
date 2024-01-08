public class VideoEventArgs : EventArgs
{
    public Video Video { get; set; }
}

public class VideoEncoder
{  
    // // 1) Define a delegate
    // public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args); 

    // // 2) Define an event based on that delegate
    // public event VideoEncodedEventHandler VideoEncoded;

    // EventHandler
    // EventHandler<TEventArgs>
    public event EventHandler<VideoEventArgs> VideoEncoded;
    // public event EventHandler VideoEncoded;

    public void Encode(Video video)
    {
        Console.WriteLine("Encoding video...");
        Thread.Sleep(3000);

        OnVideoEncoded(video);
    }

    // 3) Raise the event
    protected virtual void OnVideoEncoded(Video video)
    {
        if (VideoEncoded != null)
            VideoEncoded(this, new VideoEventArgs() { Video = video });
    }
}