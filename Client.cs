using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class Client
    {
        // Поля
        private int _clientId;        // Идентификатор клиента
        private string _name;         // Имя клиента
        private string _address;      // Адрес клиента
        private string _phoneNumber;  // Телефонный номер клиента
        private decimal _debtAmount;  // Задолженность клиента

        // Свойства
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public decimal DebtAmount
        {
            get { return _debtAmount; }
            set { _debtAmount = value; }
        }

        // Конструктор для инициализации клиента
        public Client(int clientId, string name, string address, string phoneNumber)
        {
            _clientId = clientId;
            _name = name;
            _address = address;
            _phoneNumber = phoneNumber;
            _debtAmount = 0; // Начальная задолженность равна нулю
        }

        // Метод для рассчета стоимости услуг для клиента
        public decimal CalculateBill()
        {
            // Реализуйте здесь логику расчета стоимости услуг
            // в зависимости от данных о разговорах клиента и тарифов
            decimal totalCost = 0;

            // Пример: totalCost = CalculateTotalCost();

            return totalCost;
        }

        // Метод для отправки счета клиенту
        public void SendInvoice()
        {
            // Реализуйте здесь логику отправки счета клиенту
            // Можете использовать _debtAmount и результат расчета стоимости услуг
        }

        public static void GetInfo(Client client)
        {
            Console.Write($"Идентификатор клиента {client._clientId}\nИмя клиента {client._name }\nАдрес клиента {client._address }\nТелефонный номер клиента {client._phoneNumber }\nЗадолженность клиента {client._debtAmount } ");
        }
    }

}
