namespace Service.Exceptions.NotFoundExeptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException() : base("Data bulunamadı")
        {

        }
    }
}
