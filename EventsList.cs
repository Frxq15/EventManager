using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager
{
    public class EventDetails 
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int AmountTickets { get; set; }
        public float PricePerTicket { get; set; }
        public string DateTime { get; set; }
        public List<BookingDetails> BookingDetails = new List<BookingDetails>();
    }
}
