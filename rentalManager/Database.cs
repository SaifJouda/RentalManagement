//Add MySql Library
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

/*
* Database Class
* Author: Luke Edward, 251107324
* Connect to the database with auth credentials and run appropriate queries
*/
class Database
{
    private MySqlConnection connection;
    private string server;
    private string database1;
    private string uid;
    private string password;

    //Constructor
    public Database()
    {
        Initialize();
    }

    //Initialize values
    private void Initialize()
    {
        server = "cs4471db.cek2uenwafvt.us-east-2.rds.amazonaws.com";
        database1 = "cs4471Project";
        //or it could be css4471db
        uid = "admin";
        password = "cs4471rocks";
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
		database1 + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        connection = new MySqlConnection(connectionString);
    }

        //open connection to database
    private bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            switch (ex.Number)
            {
                case 0:
                    Console.WriteLine("Cannot connect to server.  Contact administrator");
                    break;

                case 1045:
                    Console.WriteLine("Invalid username/password, please try again");
                    break;
            }
            return false;
        }
    }

    //Close connection
    private bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
        
    // --------------------------- Insert Functions ------------------------------ //
    //Insert statement
    public void InsertCustomer(string customerID, string name)
    {
        string query = $"INSERT INTO customers (customer_id, customer_name, balance, amount_paid) VALUES('{customerID}','{name}', '0', '0')";

        //open connection
        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            
            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    public void InsertItem(string name, double cost, int stock)
    {
        string query = $"INSERT INTO items (item_name, cost, stock) VALUES('{name}', '{cost}', '{stock}')";
        //open connection
        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            
            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    public void RentItem(string customerName, string itemName, double cost)
    {
        string query = $"INSERT INTO rents (customer_name, item_name, rent_cost, date_rented) VALUES('{customerName}', '{itemName}', '{cost}',CURDATE())";
        this.DecreaseStock(itemName);

        //open connection
        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            
            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Return Item statement
    public void ReturnItem(string customerName, string itemName)
    {
        string query = $"DELETE FROM rents WHERE item_name='{itemName} AND customer_name='{customerName}'";
        this.IncreaseStock(itemName);
        if (this.OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            this.CloseConnection();
        }
    }

    // --------------------------- Update Data ------------------------------ //

    //Update statement
    public void IncreaseStock(string itemName)
    {
        string query = $"UPDATE items SET stock = stock + 1 WHERE item_name ='{itemName}'";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Update statement
    public void DecreaseStock(string itemName)
    {
        string query = $"UPDATE items SET stock = stock - 1 WHERE item_name ='{itemName}'";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Update statement
    public void CustomerPayBalance(string customerName, double payment)
    {
        string query = $"UPDATE customers SET amount_paid = amount_paid + {payment} WHERE customer_name ='{customerName}'";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    public void CustomerSetBalance(string customerName, double newBalance)
    {
        string query = $"UPDATE customers SET balance = {newBalance} WHERE customer_name ='{customerName}'";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    // --------------------------- Delete Data ------------------------------ //

    //Delete Item statement
    public void DeleteItem(string itemName)
    {
        string query = $"DELETE FROM items WHERE item_name='{itemName}'";

        if (this.OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            this.CloseConnection();
        }
    }

    //Delete Customer statement
    public void DeleteCustomer(string customerID)
    {
        string query = $"DELETE FROM customers WHERE customer_id='{customerID}'";

        if (this.OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            this.CloseConnection();
        }
    }

    

    //Select statement
    public List< string >[] GetCustomerList()
    {
        string query = "SELECT customers.customer_id, customers.customer_name, customers.balance, customers.amount_paid, items.item_name, rents.rent_cost, rents.date_rented FROM customers JOIN rents ON (rents.item_id = customers.customer_id) JOIN items ON (items.item_id = rents.customer_id)";

        //Create a list to store the result
        List< string >[] list = new List< string >[7];
        list[0] = new List< string >();
        list[1] = new List< string >();
        list[2] = new List< string >();
        list[3] = new List< string >();
        list[4] = new List< string >();
        list[5] = new List< string >();
        list[6] = new List< string >();


        //Open connection
        if (this.OpenConnection() == true)
        {
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["customerID"] + "");
                list[1].Add(dataReader["customerName"] + "");
                list[2].Add(dataReader["balance"] + "");
                list[3].Add(dataReader["amountPaid"] + "");
                list[4].Add(dataReader["itemName"] + "");
                list[5].Add(dataReader["rentalCost"] + "");
                list[6].Add(dataReader["dateRented"] + "");


            }

            //close Data Reader
            dataReader.Close();

            //close Connection
            this.CloseConnection();

            //return list to be displayed
            return list;
        }
        else
        {
            return list;
        }
    }
    
    //Select statement
    public List< string >[] GetItemList()
    {
        string query = "SELECT items.item_id, items.item_name, items.cost, items.stock, customers.customer_name FROM items JOIN rents ON (rents.item_id = items.item_id) JOIN customers ON (customers.customer_id = rents.customer_id)";

        //Create a list to store the result
        List< string >[] list = new List< string >[5];
        list[0] = new List< string >();
        list[1] = new List< string >();
        list[2] = new List< string >();
        list[3] = new List< string >();
        list[4] = new List< string >();

        //Open connection
        if (this.OpenConnection() == true)
        {
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["itemID"] + "");
                list[1].Add(dataReader["itemName"] + "");
                list[2].Add(dataReader["itemCost"] + "");
                list[3].Add(dataReader["itemStock"] + "");
                list[4].Add(dataReader["customerName"] + "");

            }

            //close Data Reader
            dataReader.Close();

            //close Connection
            this.CloseConnection();

            //return list to be displayed
            return list;
        }
        else
        {
            return list;
        }
    }



    //Backup
    public void Backup()
    {
    }

    //Restore
    public void Restore()
    {
    }
}