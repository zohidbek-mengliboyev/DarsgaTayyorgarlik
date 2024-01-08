namespace Events
{
    // delegat
    public delegate void TugmaDelegati();
    public class Tugma
    {
        // hodisani e'lon qilish
        public event TugmaDelegati Click;

        // tugma bosilish "simulyatsiya" si
        public void Simulation()
        {
            // tugmani bosish
            if (Click != null)
                Click();
        }
    }
}