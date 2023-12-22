public class PhotoProcessor
{
    // public delegate void PhotoFilterHandler(Photo photo);
    public void Process(string path, Action<Photo> filterHandler)
    {
        var photo = Photo.Load(path);

        // var filters = new PhotoFilters();
        // filters.ApplyBrightness(photo);
        // filters.ApplyContrast(photo);
        // filters.Resize(photo);

        filterHandler(photo);

        photo.Save();
    }
}