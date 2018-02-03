using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using RestService.Models;

namespace RestService.App_Code
{
    public class PersonPersistene
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public PersonPersistene()
        {
            string connectionString;
            connectionString = "server=127.0.0.1;uid=root;pwd=data;database=persondb";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public long SavePerson(Person person)
        {
            string sqlString = "INSERT INTO persontbl (FirstName, LastName, PayRate, StartDate, EndDate) VALUES ('" +
                               person.FirstName + "','" + person.LastName + "'," + person.PayRate + ",'" +
                               person.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                               person.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            MySqlCommand cmd= new MySqlCommand(sqlString,conn);
            long Id = cmd.LastInsertedId;
            return Id;
        }
    }
}