﻿using System;

namespace ATS
{
    public class Call
    {
        public DateTime CallDate { get; set; } 
        public int Duration { get; set; } 
        public bool IsIncoming { get; set; } 
        public string ClientId { get; set; } 
        public decimal Cost { get; set; } 

        public Call(DateTime callDate, int duration, bool isIncoming, string clientId)
        {
            CallDate = callDate;
            Duration = duration;
            IsIncoming = isIncoming;
            ClientId = clientId;

            Cost = CalculateCallCost();
        }


        private decimal CalculateCallCost()
        {
            if (!IsIncoming)
            {
                double minutes = (double)Duration / 60.0; 
                decimal cost = (decimal)(minutes * 0.10); 
                return cost;
            }
            else
            {
                return 0.0m;
            }
        }


        public override string ToString()
        {
            string callType = IsIncoming ? "входящий" : "исходящий";
            return $"{CallDate:dd.MM.yyyy HH:mm:ss} - {callType} звонок для клиента {ATScompany.Instance.FindClientById(ClientId).Name}. " +
                $"\nПродолжительность: {Duration} сек. Стоимость: {Cost:C}";
        }
    }
}
