using System;
using System.Globalization;

/*
* RentedItem Class
* Author: Navjeeven Mann, 251036059
* Rented Item Class
*/

class RentedItem {

    // Define Variables
    double rentCost;
    DateTime dateRented;
    string itemRented;


    // Get the cost of renting the item
    public double getRentCost()
    {
        return this.rentCost;
    }

    // Get the date the item has been rented from
    public DateTime getDateRented()
    {
        return this.dateRented;
    }

    // Get the item name that has been rented
    public string getItemRented()
    {
        return this.itemRented;
    }

    public double calculateRent() {
            var cal = new System.Globalization.GregorianCalendar();
            var weekNum = cal.GetWeekOfYear(this.dateRented, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            // Get the difference between the dateRented and the current date
            TimeSpan tt = this.dateRented - DateTime.Now;
            double days = tt.Days/7;
            // Get the total amount of weeks floored
            double totalWeeks = Math.Floor(days);

            // Mod by 2 to get the bi-weekly amount of weeks
            var biweeklyAmount = (totalWeeks % 2);

            // If we aren't in the first week, subtract one to account for the first bi-weekly payment
            if (biweeklyAmount > 0) {
                biweeklyAmount -= 1;
            }

            return (biweeklyAmount * rentCost) + (rentCost);

    }

    // Constructor
    public RentedItem(DateTime dateRented, RentalItem itemRented) {
        this.dateRented = dateRented;
        this.rentCost = itemRented.getItemCost();
        this.itemRented = itemRented.getItemName();
    }



}