namespace Exceptions
{
    [Serializable]
    public class InvalidIPAdress : Exception
    {
        public InvalidIPAdress():base("Invalid IP adress")
        {
        }


        public InvalidIPAdress(string text):base(text)
        {
            
        }
    }
}