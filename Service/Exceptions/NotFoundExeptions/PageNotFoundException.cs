namespace Service.Exceptions.NotFoundExeptions
{
    public class PageNotFoundException : Exception
    {
        public PageNotFoundException() : base("Sayfa Bulunamadı")
        {

        }
    }
}
