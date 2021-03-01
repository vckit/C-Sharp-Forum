namespace Olymp2021.Model
{
    partial class Client
    {
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName + " " + Patronymic;
            }
        }
    }
}
