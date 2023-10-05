﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public class ATScompany
    {
        public static ATScompany Instance { get; } = new ATScompany();
        public List<Client> Clients { get; set; }

        public List<Call> Calls { get; set; }

        public List<Invoice> Invoices { get; set; }

        private ATScompany() 
        {
            Clients = new List<Client>();
            Calls = new List<Call>();
            Invoices = new List<Invoice>();
        }

        public void AddClient(Client client)
        {
            Clients.Add(client);
        }

        public void AddCall(Call call)
        {
            Calls.Add(call);
        }

        public void SendInvoice()
        {
            DateTime currentDate = DateTime.Now;

            foreach (var client in Clients)
            {
                var callsAfterLastInvoice = client.CallHistory
                    .Where(call => call.CallDate > client.LastInvoiceDate)
                    .ToList();

                if (callsAfterLastInvoice.Count > 0)
                {
                    decimal callCost = callsAfterLastInvoice.Sum(call => call.Cost);

                    Invoice invoice = new Invoice(currentDate, currentDate.AddDays(10), callCost, client.Id);

                    client.Invoices.Add(invoice);
                    Invoices.Add(invoice);

                    client.LastInvoiceDate = currentDate;
                }
            }
        }



        public void PrintAllClients()
        {
            Console.WriteLine("\n\nСписок клиентов:");
            foreach (var client in Clients)
            {
                Console.WriteLine();

                Console.WriteLine(client.ToString()); 
            }
        }

        public void PrintAllCalls()
        {
            Console.WriteLine("\n\nСписок разговоров:");
            foreach (var call in Calls)
            {
                Console.WriteLine();

                Console.WriteLine(call.ToString());
            }
        }

        public void PrintAllInvoices()
        {
            Console.WriteLine("\n\nСписок счетов:");
            foreach (var invoice in Invoices)
            {
                Console.WriteLine();

                Console.WriteLine(invoice.ToString());
            }
        }

        public Client FindClientById(string clientId)
        {
            return Clients.FirstOrDefault(client => client.Id == clientId);
        }
    }
}
