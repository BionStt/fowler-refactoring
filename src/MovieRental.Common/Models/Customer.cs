using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.Common.Models
{
    public class Customer
    {
        public string Name { get; private set; }
        private ICollection<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            Name = name;
        }

        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }    

        public string Statement()
        {
            int frequentRenterPoints = 0;
            string result = $"Rental Record for {Name}{Environment.NewLine}";

            foreach(var rental in _rentals)
            {
                // add frequent renter points
                frequentRenterPoints += rental.FrequentRenterPoints;

                //show figures for this rental
                result += $"\t{rental.Movie.Title}\t{rental.GetCharge()}{Environment.NewLine}";
            }

            //add footer lines
            result += $"Amount owed is {GetTotalCharge()}{Environment.NewLine}";
            result += $"You earned {frequentRenterPoints} frequent renter points";
        
            return result;
        }

        private double GetTotalCharge()
        {
            return _rentals.Sum( r => r.GetCharge());
        }
    }
}
