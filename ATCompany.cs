using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class ATCompany
    {
        // Список клиентов
        private static List<Client> _clients = new List<Client>();

        // Список выставленных счетов
        private static List<Invoice> _invoices = new List<Invoice>();

        // Метод для подключения нового клиента
        public static void AddClient(Client client)
        {
            // Добавляем клиента в список клиентов
            _clients.Add(client);
        }

        // Метод для создания разговора
        public Call CreateCall(int callId, DateTime startTime, TimeSpan duration, decimal ratePerMinute, Client client)
        {
            // Создаем новый объект разговора
            Call call = new Call(callId, startTime, duration, ratePerMinute);

            // Устанавливаем связь с клиентом
            call.Client = client;

            return call;
        }

        // Метод для выставления счета клиенту
        public void GenerateInvoice(Invoice invoice)
        {
            // Добавляем счет в список выставленных счетов
            _invoices.Add(invoice);
        }

        // Другие методы и операции, связанные с управлением АТС
    }

}
