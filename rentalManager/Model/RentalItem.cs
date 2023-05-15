using System;
using System.Collections.Generic;
using System.Linq;

class RentalItem {

    // Define Variables
        string itemName;
        double itemCost;
        int inStock;
        List<Customer> customerList = new List<Customer>();  

// Constructor
public RentalItem(string itemName, double itemCost, int inStock) {
    this.itemName = itemName;
    this.itemCost = itemCost;
    this.inStock = inStock;
}

// Decrease the stock quantity
 public void itemRented() {
    inStock -= 1;
}

// Increase the stock quantity
 public void itemReturned() {
    this.inStock = this.inStock + 1;
}

// Add a customer to the list of customers
public void addCustomer(Customer newCustomer) {
    customerList.Add(newCustomer);
}


public void removeCustomer(Customer toRemove) {
    // Remove a customer by the customerID
    var customerToRemove = customerList.SingleOrDefault(x => x.getCustomerID() == toRemove.getCustomerID());

    // If we find the customer to remove, remove it
    if (customerToRemove != null) 
        customerList.Remove(customerToRemove);
}

// Get the list of customers
public List<Customer> getCustomers() {
    return this.customerList;
}

// Get the amount in stock
public int getItemsInStock() {
    return this.inStock;
}

// Get the cost of the item
public double getItemCost() {
    return this.itemCost;
}

// Get the name of the item
public string getItemName() {
    return this.itemName;
}

// Set the cost of the item
public void setItemCost(double newCost) {
    this.itemCost = newCost;
}

// Set the amount of items in stock
public void setItemsInStock(int newStock) {
    this.inStock = newStock;
}

// Create a rental agreement with a customer and a rental item
public void createRentalAgreement(Customer withCustomer, RentalItem rentalItem) {

    // Create a rented item from the rentalItem
    RentedItem rentedItem = new RentedItem(DateTime.Now, rentalItem);

    // Add the rented item to the customer
    withCustomer.addRentalItem(rentedItem);

    // Add the customer to the list of customers in the Rental Item
    // Decrement the amount of items in stock
    rentalItem.addCustomer(withCustomer);
    rentalItem.itemRented();
}

}

 