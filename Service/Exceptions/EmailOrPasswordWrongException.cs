namespace Service.Exceptions
{
    public class EmailOrPasswordWrongException : Exception
    {
        public EmailOrPasswordWrongException() : base("Email or password wrong.")
        {

        }
    }
}
