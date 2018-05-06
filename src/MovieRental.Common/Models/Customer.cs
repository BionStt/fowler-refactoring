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
            var result = $"Rental Record for {Name}{Environment.NewLine}";

            foreach(var rental in _rentals)
            {
                //show figures for this rental
                result += $"\t{rental.Movie.Title}\t{rental.GetCharge()}{Environment.NewLine}";
            }

            //add footer lines
            result += $"Amount owed is {GetTotalCharge()}{Environment.NewLine}";
            result += $"You earned {GetTotalFrequentRenterPoints()} frequent renter points";
        
            return result;
        }

        public string HtmlStatement()
        {
            var result = $"<H1>Rentals for <EM>{Name}</EM></H1><P>{Environment.NewLine}";

            foreach(var rental in _rentals)
            {
                //show figures for each rental
                result += $"{rental.Movie.Title}: {rental.GetCharge()}<BR>{Environment.NewLine}";
            }

            //add footer lines
            result += $"<P>You owe <EM>{GetTotalCharge()}</EM></P>";
            result += $"On this rental you earned <EM>{GetTotalFrequentRenterPoints()}</EM> frequent renter points <P>";

            return result;
        }

        private int GetTotalFrequentRenterPoints()
        {
            return _rentals.Sum(r => r.FrequentRenterPoints);
        }

        private double GetTotalCharge()
        {
            return _rentals.Sum( r => r.GetCharge());
        }
    }
}
