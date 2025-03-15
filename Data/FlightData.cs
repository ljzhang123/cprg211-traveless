using System.Collections.Generic;

namespace traveless.Data
{
	public static class FlightData
	{
		public static List<Flight> GetFlights()
		{
			// Sample flight data for demonstration
			return new List<Flight>
			{
				new Flight("GA-1234", "Garuda", "Jakarta", "Bali", "Monday", "08:00", 1500000, 100),
				new Flight("AA-5678", "AirAsia", "Jakarta", "Singapore", "Tuesday", "09:30", 1200000, 80),
				new Flight("BA-9012", "Batik", "Bali", "Jakarta", "Wednesday", "14:00", 1600000, 90),
				new Flight("CA-3456", "Citilink", "Jakarta", "Surabaya", "Thursday", "17:00", 1300000, 75)
			};
		}
	}
}
