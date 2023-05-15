using System;
using System.Collections.Generic;

/*
* Functions Class
* Author: Shoumik Shill, 250935418
* Controller Class that handles the modifying of the Model (Customer lists, Rental and Rented Item lists)
*/

class Functions {

    // ------------ Attributes ------------ 

    // List data structures that contain lists of Rental Items and Customers
    List<RentalItem> itemList = new List<RentalItem>();
    List<Customer> customerList = new List<Customer>();
    

    /* Initialize database */
    static Database db = new Database();

    // ------------ Item Functions ------------ 

    // Return a list of the Item List
    public List<RentalItem> returnItemList() {


        List<string>[] dbItemList = new List< string>[5];

        try {
            dbItemList = db.GetItemList();
        }
        catch (Exception ex) {
        }

        
        return itemList;
    }

    // Add Item to the database and return true if successful
    public void addItem(string name, double cost, int stock) {
        
        RentalItem newItem = new RentalItem(name, cost, stock);
        // Add to local DB
        itemList.Add(newItem);

        // Code to add to database
        try {
            db.InsertItem(name, cost, stock);
        }
        catch (Exception ex) {
        }
        // Placeholder
    }

    // Delete Item from database and return true if successful
    public Boolean deleteItem(int itemArrayPosition) {
        

        // Database Code 
        string toDelete = itemList[itemArrayPosition].getItemName();
        
        // Code to add to database
        try {
            db.DeleteItem(toDelete);
        }
        catch (Exception ex) {
        }
        // Remove from localDB
        itemList.RemoveAt(itemArrayPosition);

        // Placeholder
        return false;
    }

    // ------------ Customer List Methods ------------ 

    // Return a list of the Customer List
    public List<Customer> returnCustomerList() { // Change string to Customer.cs?
        
        List<string>[] dbCustomerList = new List< string>[7];
        
        try {
            dbCustomerList = db.GetCustomerList();
        }
        catch (Exception ex) {
        }

        return customerList;
    }

    // Add Customer to the database and return true if successful
    public void addCustomer(string name) {

        // Generate customer UUID
        Guid customerUUID = Guid.NewGuid();

        // Add customer to list
        string custID = customerUUID.ToString();
        Customer newCust = new Customer(name, custID);
        customerList.Add(newCust);

        // Database code
        try {
            db.InsertCustomer(custID, name);

        }
        catch (Exception ex) {
        }
        // Placeholder
    }

    // Delete Customer from database and return true if successful
    public Boolean deleteCustomer(int customerArrayPosition) {

        //db code
        string toDelete = customerList[customerArrayPosition].getCustomerID();
        
        try {
            db.DeleteCustomer(toDelete);
        }
        catch (Exception ex) {
        }

        // Remove from localDB
        customerList.RemoveAt(customerArrayPosition);

        return false;
    }

    // ------------ Rental Management Methods ------------ 

    // Adds the item to the customer's list
    public void rentItem(Customer cust, RentalItem item) {

        // Convert from Rental Item to Rented Item
        RentedItem rentedItem = new RentedItem(DateTime.Now, item);

        // Add the converted Rented Item to customer's list.
        cust.addRentalItem(rentedItem);

        // Decrease Stock
        item.itemRented();

        // db code
        string name = cust.getCustomerName();
        string itemName = item.getItemName();
        double cost = item.getItemCost();
        try {
            db.RentItem(name, itemName, cost);
        }
        catch (Exception ex) {
        }
        
    }

    // Removes item from the customer's list

    public void returnItem(Customer cust, RentedItem item) {
        
        //db code
        string name = cust.getCustomerName();
        string itemName = item.getItemRented();
        try {
            db.ReturnItem(name, itemName);
        }
        catch (Exception ex) {
        }

        // cust list
        cust.removeRentalItem(item);

        // Increase stock
        foreach (var i in itemList) {
            if (i.getItemName() == item.getItemRented()) {
                i.itemReturned();
            }
        }

        
    }

    // ------------ Customer Payment Methods ------------ 

    // Return total amount a customer owes
    public double customerOwe(Customer cust) {

        // db code
        string name = cust.getCustomerName();
        double balance = cust.getBalance();
        

        try {
            db.CustomerSetBalance(name, balance);
        }
        catch (Exception ex) {
        }

        return cust.getBalance();
    }

    // Deducts a payment of money from the amount they owe
    public void customerPay(int customerIndex, double pay) {
        
        //db code
        string name = customerList[customerIndex].getCustomerName();
        try {
            db.CustomerPayBalance(name, pay);
        }
        catch (Exception ex) {
        }
        //list code
        this.customerList[customerIndex].payBalance(pay);
    }

}