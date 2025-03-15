using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traveless.Data
{
	[Serializable]
	public class Reservation
	{
		public string ReservationCode { get; set; }  // Format: LDDDD (e.g., R1234)
		public Flight Flight { get; set; }
		public string TravellerName { get; set; }
		public string Citizenship { get; set; }
		public bool IsActive { get; set; }

		public Reservation(string reservationCode, Flight flight, string travellerName, string citizenship)
		{
			ReservationCode = reservationCode;
			Flight = flight;
			TravellerName = travellerName;
			Citizenship = citizenship;
			IsActive = true;
		}

		public void Update(string newName, string newCitizenship, bool newStatus)
		{
			if (string.IsNullOrWhiteSpace(newName))
				throw new Exception("Traveller name cannot be empty.");
			if (string.IsNullOrWhiteSpace(newCitizenship))
				throw new Exception("Citizenship cannot be empty.");

			TravellerName = newName;
			Citizenship = newCitizenship;
			IsActive = newStatus;
		}

		public override string ToString()
		{
			return $"{ReservationCode} | Flight: {Flight.FlightCode} | {Flight.Airline} | {TravellerName} | {(IsActive ? "Active" : "Inactive")}";
		}
	}
}
