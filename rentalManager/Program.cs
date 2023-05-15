//Saif Jouda

using System;
using System.Collections.Generic;
using System.Linq;



namespace rentalManager
{
    class Program
    {

        static Functions function = new Functions();

   
        ///////////////////////////////////////////////////////////////////
        ////////////////////////// ITEM MENU //////////////////////////////

        //Displays the list of items
        //returns false if list is empty, true otherwise
        private static bool writeItemList()
        {
            List<RentalItem> rentItems = function.returnItemList();
            if(rentItems.Count==0) 
            {
                Console.Clear();
                Console.WriteLine("List is Empty");
                return false;
            }

            for(int i=0; i< rentItems.Count; i++){
                Console.WriteLine("{0,-0}{1,-0}{2,-10} {3,-1} {4,-7}{5,10} {6,1}",(i+1),". ",rentItems[i].getItemName(),"Cost:",rentItems[i].getItemCost(),"Stock:",rentItems[i].getItemsInStock());
            }
            return true; 
        }

        //Item Menu function
        private static bool ItemMenu()
        {
            Console.WriteLine("\n||| ITEM MENU |||\n"+
            "1. Item List\n"+
            "2. Add Item\n"+
            "3. Delete Item\n"+
            "4. Back");

            switch(Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    writeItemList();
                    return true;
                case "2":
                    Console.WriteLine("Enter Item Name:");
                    String iname = Console.ReadLine();
                    double cost;
                    int stock;
                    Console.WriteLine("Enter cost of renting the item bi-weekly");
                    while(true)
                    {
                        try
                        {
                            cost = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Please Enter a Valid Input");
                        }
                    }
                    Console.WriteLine("Enter stock amount:");
                    while(true)
                    {
                        try
                        {
                            stock = Int32.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Please Enter a Valid Input");
                        }
                    }
                    //Add Item to database function
                    function.addItem(iname, cost, stock);
                    Console.Clear();
                    Console.WriteLine("Item Added");
                    return true;
                case "3":
                    Console.WriteLine("Choose Item to Delete:");
                    if(writeItemList())
                    {
                        int arrLoc;
                        while(true)
                        {
                            try
                            {
                                arrLoc = Int32.Parse(Console.ReadLine());
                                //attempt to delete item
                                function.deleteItem(arrLoc-1);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Please Enter a Valid Input");
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("Item Deleted");
                    }
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }

        ///////////////////////////////////////////////////////////////////
        ////////////////////////// CUSTOMER MENU //////////////////////////

        //Prints the Customer List
        // returns false if list is empty, true otherwise
        private static bool writeCustomerList()
        {
            List<Customer> custList = function.returnCustomerList();
            if(custList.Count==0)
            {
                Console.Clear();
                Console.WriteLine("List is Empty");
                return false;
            }
            else
            {
                for(int i=0; i< custList.Count; i++){
                    Console.WriteLine((i+1)+". "+custList[i].getCustomerName());
                }
                return true; 
            }
        }


        //This is the customer menu function, allows you to add and delete customers
        private static bool CustomerMenu()
        {
            Console.WriteLine("\n||| CUSTOMER MENU |||\n"+
            "1. Customer List\n"+
            "2. Add Customer\n"+
            "3. Delete Customer\n"+
            "4. Back");

            switch(Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    writeCustomerList();
                    return true;
                case "2":
                    Console.WriteLine("Enter Customer Name:");
                    String cname = Console.ReadLine();
                    //Add customer to database function/command
                    function.addCustomer(cname);
                    Console.Clear();
                    Console.WriteLine("Customer Added");
                    return true;
                case "3":
                    Console.WriteLine("Choose Customer From List:");
                    if(writeCustomerList())
                    {
                        int arrLoc;
                        while(true)
                        {
                            try
                            {
                                arrLoc = Int32.Parse(Console.ReadLine());
                                //attempt to delete customer
                                function.deleteCustomer(arrLoc-1);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Please Enter a Valid Input");
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("Customer Deleted");
                    }
                   
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }

        }


        ///////////////////////////////////////////////////////////////////
        ////////////////////////// RENTAL MENU //////////////////////////////

        //prints the list of a customer's items
        //customer the Customer datatype 
        //returns false if the list is empty, true otherwise
        private static bool writeCustomersItems(Customer customer)
        {
            List<RentedItem> itemList = customer.getRentalItems();
            if(itemList.Count==0) 
            { 
                Console.WriteLine("List is Empty");
                return false;
            }
            else
            {
                for(int i=0; i< itemList.Count; i++){
                    Console.WriteLine((i+1)+". "+itemList[i].getItemRented());
                }
                return true; 
            }
        }

        //the rental menu function
        private static bool RentalMenu()
        {
            Console.WriteLine("\n||| RENTAL MENU |||\n"+
            "1. Rent Item\n"+
            "2. Return Item\n"+
            "3. Back");

            switch(Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Choose Customer:");
                    if(writeCustomerList())
                    {
                        Customer cusToRent;
                        while(true)
                        {
                            try
                            {
                                int cusLoc = Int32.Parse(Console.ReadLine());
                                cusToRent=function.returnCustomerList()[cusLoc-1];
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Please Enter a Valid Input");
                            }
                        }
                        Console.WriteLine("Choose Item To Rent:");
                        if(writeItemList())
                        {
                            RentalItem itemToRent;
                            while(true)
                            {
                                try
                                {
                                    int iLoc = Int32.Parse(Console.ReadLine());
                                    itemToRent=function.returnItemList()[iLoc-1];
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Please Enter a Valid Input");
                                }
                            }
                            //ADD item to customer's item list
                            function.rentItem(cusToRent,itemToRent);
                            Console.Clear();
                            Console.WriteLine("Item Added to Customer's Item List");
                        }
                    }
                    return true;
                case "2":
                    Console.WriteLine("Choose Customer:");
                    if(writeCustomerList())
                    {
                        Customer cusToRentR;
                        while(true)
                        {
                            try
                            {
                                int cusLoc = Int32.Parse(Console.ReadLine());
                                cusToRentR=function.returnCustomerList()[cusLoc-1];
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Please Enter a Valid Input");
                            }
                        }
                        Console.WriteLine("Choose Item To Remove:");
                        if(writeCustomersItems(cusToRentR))
                        {
                            RentedItem itemToRentR;
                            while(true)
                            {
                                try
                                {
                                    int iLoc = Int32.Parse(Console.ReadLine());
                                    itemToRentR=cusToRentR.getRentalItems()[iLoc-1];
                                    //remove item from customer's list
                                    function.returnItem(cusToRentR,itemToRentR);
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Please Enter a Valid Input");
                                }
                            }
                            Console.Clear();
                            Console.WriteLine("Item Removed to Customer's Item List");
                        }
                        
                    }
                    
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }

        }

        /////////////////////////////////////////////////////////////////////
        ////////////////////////// PAYMENT MENU //////////////////////////////

        //payment menu function
        private static bool PaymentMenu()
        {
            Console.WriteLine("\n||| PAYMENT MENU |||\n"+
            "1. View Customer's Item List and Amount Owed\n"+
            "2. Make Customer Payment\n"+
            "3. Back");

            switch(Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Choose Customer:");
                    if(writeCustomerList())
                    {
                        Customer cusToShow;
                        while(true)
                        {
                            try
                            {
                                int cusLoc = Int32.Parse(Console.ReadLine());
                                cusToShow=function.returnCustomerList()[cusLoc-1];
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Please Enter a Valid Input");
                            }
                        }
                        //Print Customer's List with Amount Owing
                        Console.Clear();
                        Console.WriteLine("Customer: "+ cusToShow.getCustomerName());
                        Console.WriteLine("Amount Owed: "+ cusToShow.getBalance());
                        writeCustomersItems(cusToShow);
                    }
                    return true;
                case "2":
                    Console.WriteLine("Choose Customer:");
                    if(writeCustomerList())
                    {
                        Customer cusToPay;
                        int cusToPayLoc;
                        while(true)
                        {
                            try
                            {
                                cusToPayLoc = Int32.Parse(Console.ReadLine());
                                cusToPay=function.returnCustomerList()[cusToPayLoc-1];
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Please Enter a Valid Input");
                            }
                        }
                        Console.WriteLine("Enter Amount Paid:");
                        double amountPayed; 
                        while(true)
                        {
                            try
                            {
                                amountPayed = Convert.ToDouble(Console.ReadLine());
                                //attempt to deduct amount
                                function.customerPay(cusToPayLoc-1,amountPayed);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Please Enter a Valid Input");
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("Amount Deducted from Amount Owed");
                    }
                

                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }

        }

        ///////////////////////////////////////////////////////////////////
        ////////////////////////// MAIN MENU /////////////////////////////

        //Prints the Main Menu
        public static void writeMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n||| RENTAL MANAGER MAIN MENU |||\n"+
            "Choose Item From List:\n"+
            "1. Customer Menu\n"+
            "2. Item Menu\n"+
            "3. Rental Menu\n"+
            "4. Payment Menu\n"+
            "5. Exit");
        }

        //the main menu function/interface
        private static bool MainMenu()
        {
            bool inSideMenu=true;
            writeMainMenu();
            switch(Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    while(inSideMenu){inSideMenu=CustomerMenu();}
                    return true;
                case "2":
                    Console.Clear();
                    while(inSideMenu){inSideMenu=ItemMenu();}
                    return true;
                case "3":
                    Console.Clear();
                    while(inSideMenu){inSideMenu=RentalMenu();}
                    return true;
                case "4":
                    Console.Clear();
                    while(inSideMenu){inSideMenu=PaymentMenu();}
                    return true;
                case "5":
                    Console.Clear();
                    return false;
                default:
                    Console.WriteLine("Invalid Input");
                    return true;
            }
        }


        ///////////////////////////////////////////////////////////////////////
        ////////////////////////// MAIN FUNCTION //////////////////////////////

        //the main
        static void Main(string[] args)
        {
            bool inMenu =true;
            while(inMenu)
            {
                inMenu=MainMenu();
            }
        }
    }


}
