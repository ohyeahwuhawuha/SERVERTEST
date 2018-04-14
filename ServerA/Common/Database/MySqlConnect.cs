using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

//base from https://www.codeproject.com/articles/43438/connect-c-to-mysql

namespace Common.Database
{
    public class MySqlConnect
    {
        public MySqlConnect()
        {
        }

        private string server = "localhost";
        private string database = "fish";
        private string user = "root";
        private string password = "";
        private string port = "3306";
        private MySqlConnection connection;
        private List<List<string>> selectResult = new List<List<string>>();

        //Initialize values
        public void Initialize()
        {
            string connectionString;
            connectionString = "server=" + server + ";" + "user=" + user + ";" + "database=" +
            database + ";" + "port" + port + ";" + "password=" + password + ";";

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
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
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

        //Insert or Update or Delete statement
        public void NotSelectAction(string sql)
        {
            //string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }            
        }

        //Delete statement
        //string query = "DELETE FROM tableinfo WHERE name='John Smith'";

        //Select statement
        public List<List<string>> SelectAction(string sql)
        {
            //string query = "SELECT * FROM tableinfo";

            //Create a list to store the result
            selectResult.Clear();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    List<string> oneRowResult = new List<string>();
                    for (int index = 0; index < dataReader.FieldCount;++index)
                    {
                        oneRowResult.Add(dataReader[index].ToString());
                    }
                    selectResult.Add(oneRowResult);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            //return list to be displayed
            return selectResult;
        }

//        //Count statement
//        public int Count()
//        {
//            string query = "SELECT Count(*) FROM tableinfo";
//            int Count = -1;
//
//            //Open Connection
//            if (this.OpenConnection() == true)
//            {
//                //Create Mysql Command
//                MySqlCommand cmd = new MySqlCommand(query, connection);
//
//                //ExecuteScalar will return one value
//                Count = int.Parse(cmd.ExecuteScalar() + "");
//
//                //close Connection
//                this.CloseConnection();
//
//                return Count;
//            }
//            else
//            {
//                return Count;
//            }
//        }
    }
}
