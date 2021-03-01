namespace Olimpiad.Model
{
    partial class Client
    {
        // свойство возвращает ФИО клиента в окно записи клиента на услугу
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName + " " + Patronymic;
            }
        }
    }
}
