using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public interface IAvailableClientActions
    {
        public void MakeCall(DateTime callDate, int duration, bool isIncoming, string clientId);
        public void ReceiveInvoice();
        public void PayInvoice(decimal payment);
        public void AddOverpayment(decimal amount);
    }
}
