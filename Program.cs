using System;

namespace ATS
{
    class Program
    {
        //ATScompany company = new ATScompany();

        //static void ShowMainMenu()
        //{
        //    Console.WriteLine("1. Добавить клиента.");
        //}

        //static void StartProgram()
        //{
        //    while (true)
        //    {
        //        Run();
        //    }
        //}
        //static void Run()
        //{
        //    ShowMainMenu();
        //    Console.WriteLine("Какое действие выполнить?");
        //    int answer = int.Parse(Console.ReadLine());
        //    switch (answer)
        //    {
        //        case 1: InputClientInfo(); break;
        //        case 2: OutputClientInfo() break;
        //        default: Console.WriteLine("Неверный ввод");
        //    }
        //}

        //static void InputClientInfo()
        //{
        //    Client client = new Client();
        //    Console.Write("Имя клиента: ");
        //    client.Name = Console.ReadLine();
        //    Console.Write("Адрес клиента: ");
        //    client.Address = Console.ReadLine();
        //    company.AddClient(client);
        //}

        //static void OutputClientInfo() 
        //{
        //    Client.GetInfo;
        //}
        static void Main(string[] args)
        {
            // Создаем экземпляр ATScompany
            ATScompany company = ATScompany.Instance;

            // Создаем несколько клиентов
            Client client1 = new Client("Иван", "ул. Пушкина 10", new DateTime(1990, 5, 15));
            Client client2 = new Client("Мария", "ул. Лермонтова 5", new DateTime(1985, 8, 20));

            // Добавляем клиентов в компанию
            company.AddClient(client1);
            company.AddClient(client2);

            // Используем метод MakeCall для создания звонков
            client1.MakeCall(DateTime.Now, 300, false, client1.Id); // Исходящий звонок клиента 1
            client2.MakeCall(DateTime.Now, 180, true, client2.Id); // Входящий звонок клиента 2

            // Отправляем счета клиентам
            company.SendInvoice();

            // Выводим информацию о клиентах
            company.PrintAllClients();

            // Выводим информацию о звонках
            company.PrintAllCalls();

            // Выводим информацию о счетах
            company.PrintAllInvoices();

        }



        //(int callId, DateTime startTime, TimeSpan duration, decimal ratePerMinute, Client client)

        //StartProgram();

        //ATCompany.CreateCall(
        //    1,
        //    new DateTime(2015, 7, 20, 18, 30, 25), 
        //    new TimeSpan(0, 0, 3, 02), 1.15m , 
        //    client
        //    );

    }
    
}