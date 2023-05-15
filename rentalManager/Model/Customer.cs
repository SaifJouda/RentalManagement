using System;
using System.Collections.Generic;
using System.Linq;
/*
* Customer Class
* Author: Navjeeven Mann, 251036059
* Customer Class
*/

class Customer {


        // Define & Initialize Variables
        string customerID;
        string customerName;
        List<RentedItem> rentedItems = new List<RentedItem>();
        double balance = 0.0;
        double amountPaid = 0.0;

    // Constructor
    public Customer(string customerName, string customerID) {
        this.customerName = customerName;
        this.customerID = customerID;
    }

    // Get Balance for Customer
    public double getBalance()
    {
        // Generate and Return the Customers Balance
        this.generateBalance();
        return this.balance;
    }

    public void generateBalance()
    {
        var balance = 0.0;

        // Go through each item being rented and calculate the rent
         foreach (var i in rentedItems) {
            balance += i.calculateRent();
        }

        // Substract the amount paid from the calculated balance
        this.balance = balance - amountPaid;
    }

public void addRentalItem(RentedItem rentedItem) {
    // Add the rental item to the list of rentalItems
     rentedItems.Add(rentedItem);
}

public void removeRentalItem(RentedItem toRemove) {
    // Remove the rental item with the matching date
    var itemToRemove = rentedItems.SingleOrDefault(x => x.getDateRented() == toRemove.getDateRented());

    // If the item to be removed is found, remove it from the list
    if (itemToRemove != null)
        rentedItems.Remove(itemToRemove);
}

public List<RentedItem> getRentalItems() {
    // Return the list of rentalItems
    return rentedItems;
}

public string getCustomerID() {
    // Get the customerID
    return this.customerID;
}

public string getCustomerName() {
    // Get the customer name
    return this.customerName;
}

public void payBalance(double amountPaid) {
    // Increase the customers amound paid variable
    this.amountPaid += amountPaid;
}

}