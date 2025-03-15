using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace traveless.Data
{
	public class ReservationManager
	{
		private List<Reservation> reservations;
		private string filePath = "reservations.json";
		private int nextReservationNumber;

		public ReservationManager()
		{
			LoadReservations();
			nextReservationNumber = 1000;
			if (reservations.Count > 0)
			{
				var reservationNumbers = reservations.Select(r =>
				{
					int parsedNumber = 0;
					if (r.ReservationCode.Length == 5)
					{
						bool isParsed = int.TryParse(r.ReservationCode.Substring(1), out parsedNumber);
						if (isParsed)
						{
							return parsedNumber;
						}
					}
					return 0;
				});
				int highestNumber = reservationNumbers.Max();
				nextReservationNumber = highestNumber + 1;
			}
		}

		public Reservation MakeReservation(Flight flight, string travellerName, string citizenship)
		{
			bool isFlightMissing = flight == null;
			if (isFlightMissing)
			{
				throw new Exception("No flight selected.");
			}

			bool isTravellerNameInvalid = string.IsNullOrWhiteSpace(travellerName);
			if (isTravellerNameInvalid)
			{
				throw new Exception("Traveller name cannot be empty.");
			}

			bool isCitizenshipInvalid = string.IsNullOrWhiteSpace(citizenship);
			if (isCitizenshipInvalid)
			{
				throw new Exception("Citizenship cannot be empty.");
			}

			bool isFlightFull = flight.IsFullyBooked();
			if (isFlightFull)
			{
				throw new Exception("Flight is fully booked.");
			}

			flight.BookedSeats++;
			string newReservationCode = GenerateReservationCode();
			Reservation newReservation = new Reservation(newReservationCode, flight, travellerName, citizenship);
			reservations.Add(newReservation);
			PersistReservations();
			return newReservation;
		}

		public List<Reservation> FindReservations(string reservationCode, string airline, string travellerName)
		{
			IEnumerable<Reservation> query = reservations.AsEnumerable();
			bool isReservationCodeProvided = !string.IsNullOrWhiteSpace(reservationCode);
			if (isReservationCodeProvided)
			{
				query = query.Where(r => r.ReservationCode.Equals(reservationCode, StringComparison.OrdinalIgnoreCase));
			}

			bool isAirlineProvided = !string.IsNullOrWhiteSpace(airline);
			if (isAirlineProvided)
			{
				query = query.Where(r => r.Flight.Airline.Equals(airline, StringComparison.OrdinalIgnoreCase));
			}

			bool isTravellerNameProvided = !string.IsNullOrWhiteSpace(travellerName);
			if (isTravellerNameProvided)
			{
				query = query.Where(r => r.TravellerName.IndexOf(travellerName, StringComparison.OrdinalIgnoreCase) >= 0);
			}

			List<Reservation> foundReservations = query.ToList();
			return foundReservations;
		}

		public void UpdateReservation(Reservation reservation, string newName, string newCitizenship, bool newStatus)
		{
			bool isReservationNull = reservation == null;
			if (isReservationNull)
			{
				throw new Exception("No reservation selected.");
			}

			reservation.Update(newName, newCitizenship, newStatus);
			PersistReservations();
		}

		private string GenerateReservationCode()
		{
			string generatedCode = $"R{nextReservationNumber:D4}";
			nextReservationNumber = nextReservationNumber + 1;
			return generatedCode;
		}

		public void PersistReservations()
		{
			try
			{
				JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
				serializerOptions.WriteIndented = true;
				string jsonData = JsonSerializer.Serialize(reservations, serializerOptions);
				File.WriteAllText(filePath, jsonData);
			}
			catch (Exception error)
			{
				throw new Exception("Error saving reservations: " + error.Message);
			}
		}

		private void LoadReservations()
		{
			bool fileExists = File.Exists(filePath);
			if (fileExists)
			{
				try
				{
					string fileContents = File.ReadAllText(filePath);
					List<Reservation> loadedReservations = JsonSerializer.Deserialize<List<Reservation>>(fileContents);
					if (loadedReservations != null)
					{
						reservations = loadedReservations;
					}
					else
					{
						reservations = new List<Reservation>();
					}
				}
				catch
				{
					reservations = new List<Reservation>();
				}
			}
			else
			{
				reservations = new List<Reservation>();
			}
		}
	}
}
