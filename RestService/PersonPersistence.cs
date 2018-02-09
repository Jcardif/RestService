using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using RestService.Models;

namespace RestService
{
    public class PersonPersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public PersonPersistence()
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

        public List<Person> GetPersons()
        {
            List<Person> personList = new List<Person>();
            MySqlDataReader reader = null;
            string sqlString = $"SELECT * FROM persontbl";
            MySqlCommand cmd = new MySqlCommand(sqlString, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Person ps = new Person();
                ps.ID = reader.GetInt32(0);
                ps.FirstName = reader.GetString(1);
                ps.LastName = reader.GetString(2);
                ps.PayRate = reader.GetDouble(3);
                ps.StartDate = reader.GetDateTime(4);
                ps.EndDate = reader.GetDateTime(5);
                personList.Add(ps);
            }

            return personList;
        }
        public Person GetPerson(int ID)
        {
            Person ps =new Person();
            MySqlDataReader reader = null;
            string sqlString = $"SELECT * FROM persontbl WHERE ID = {ID}";
            MySqlCommand cmd=new MySqlCommand(sqlString, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ps.ID = reader.GetInt32(0);
                ps.FirstName = reader.GetString(1);
                ps.LastName = reader.GetString(2);
                ps.PayRate = reader.GetDouble(3);
                ps.StartDate = reader.GetDateTime(4);
                ps.EndDate = reader.GetDateTime(5);

                return ps;
            }
            else
            {
                return null;
            }

        }
        public int SavePerson(Person person)
        {
            //string sqlString = "INSERT INTO persontbl (FirstName, LastName, PayRate, StartDate, EndDate) VALUES ('" +
            //                   person.FirstName + "','" + person.LastName + "'," + person.PayRate + ",'" +
            //                   person.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
            //                   person.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            string sqlString = $"INSERT INTO `persontbl` (`FirstName`, `LastName`, `PayRate`, `StartDate`, `EndDate`) VALUES( '{person.FirstName}', '{person.LastName}', '{person.PayRate}', '{person.StartDate}', '{person.EndDate}')";

            MySqlCommand cmd = new MySqlCommand(sqlString, conn);
            int Id =Convert.ToInt32(cmd.LastInsertedId);
            return Id;
        }
        public bool DeletePerson(int id)
        {
            MySqlDataReader reader = null;
            string sqlString = $"SELECT * FROM persontbl WHERE ID = {id}";
            MySqlCommand cmd = new MySqlCommand(sqlString, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                sqlString = $"DELETE FROM persontbl WHERE ID = {id}";
                cmd = new MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}