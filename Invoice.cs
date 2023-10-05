using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public class Invoice
    {
        public DateTime InvoiceDate { get; set; } // Дата выставления счета
        public DateTime DueDate { get; set; } // Дата, до которой необходимо оплатить счет
        public decimal Amount { get; set; } 
        public bool IsPaid { get; set; } 
        public decimal PaidAmount { get; set; } 
        public string ClientId { get; set; } 

        public Invoice(DateTime invoiceDate, DateTime dueDate, decimal amount, string clientId)
        {
            InvoiceDate = invoiceDate;
            DueDate = dueDate;
            Amount = amount;
            ClientId = clientId;
            IsPaid = false;
            PaidAmount = 0;


            Client existingClient = ATScompany.Instance.FindClientById(ClientId);

            if (existingClient != null)
            {
                if (existingClient.Overpayment > 0)
                {
                    decimal remainingPayment = amount - existingClient.Overpayment;
                    if (remainingPayment <= 0)
                    {
                        IsPaid = true;
                        PaidAmount = amount;
                        existingClient.Overpayment -= amount;
                    }
                    else
                    {
                        IsPaid = false;
                        PaidAmount = 0;
                    }
                }
            }
            else
            {
                Console.WriteLine("Клиент с указанным идентификатором не найден");
            }
        }

        public override string ToString()
        {
            return $"Счет от {InvoiceDate:dd.MM.yyyy} на сумму {Amount:C} {(IsPaid ? "оплачен" : "не оплачен")}";
        }

        public static decimal CalculateLateFee(List<Invoice> Invoices)
        {
            decimal lateFee = 0;
            foreach (Invoice invoice in Invoices)
            {
                if (!invoice.IsPaid && DateTime.Now > invoice.DueDate)
                {
                    TimeSpan overdueDays = DateTime.Now - invoice.DueDate;
                    lateFee += invoice.Amount * (decimal)(overdueDays.TotalDays * 0.01);
                }
            }
            return lateFee;
        }
    }
}
