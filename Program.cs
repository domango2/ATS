using System;
using System.Text.Json;

namespace ATS
{
    class Program
    {
        static void Main(string[] args)
        {
            ATScompany company = ATScompany.Instance;


            company.LoadAll();

            company.Authorization();

            //company.PrintAllClients();

            //company.PrintAllCalls();

            //company.PrintAllInvoices();


            //Client client1 = new Client();
            //client1.PrintClientInfo();
            //client1.PrintClientInfo();
            //Client client2 = new Client("Chelovek", "ul. Dubko", DateTime.Now);
            //company.AddClient(client2);
            //client2.ReceiveInvoice();
            //company.AddClient(client2);
            //company.SaveAll();
            //company.LoadAll();

        }
    }
}