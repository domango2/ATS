using System;

namespace ATC
{
    class Program
    {
        static void Main(string[] args)
        {
            ATCompany company = new ATCompany();

            Client client = new Client(1, "Dima", "Ozheshko", "+375441234567");
            ATCompany.AddClient(client);
            Client.GetInfo(client);
        }
    }
}