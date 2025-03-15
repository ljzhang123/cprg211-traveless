using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traveless.Data
{
	public class Flight
	{
		public string FlightCode { get; set; }
		public string Airline { get; set; }
		public string Origin { get; set; }
		public string Destination { get; set; }
		public string DayOfWeek { get; set; }
		public string Time { get; set; }       
		public decimal Cost { get; set; }
		public int TotalSeats { get; set; }
		public int BookedSeats { get; set; }

		public Flight(string flightCode, string airline, string origin, string destination, string dayOfWeek, string time, decimal cost, int totalSeats)
		{
			FlightCode = flightCode;
			Airline = airline;
			Origin = origin;
			Destination = destination;
			DayOfWeek = dayOfWeek;
			Time = time;
			Cost = cost;
			TotalSeats = totalSeats;
			BookedSeats = 0;
		}

		public bool IsFullyBooked()
		{
			return BookedSeats >= TotalSeats;
		}

		public override string ToString()
		{
			return $"{FlightCode} | {Airline} | {Origin} -> {Destination} | {DayOfWeek} {Time} | Cost: {Cost:C}";
		}
	}
}
