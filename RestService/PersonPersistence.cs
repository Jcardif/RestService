using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using RestService.Models;

namespace RestService
{
    public class PersonPersistence
    {
        public List<Person> GetPersons()
        {
            MySqlConnection conn;
            string connectionString = ConfigurationManager.ConnectionStrings["persondbConnectionString"].ConnectionString;
            conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();

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
            catch (MySqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        public Person GetPerson(int ID)
        {
            MySqlConnection conn;
            string connectionString = ConfigurationManager.ConnectionStrings["persondbConnectionString"].ConnectionString;
            conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();

                Person ps = new Person();
                MySqlDataReader reader = null;
                string sqlString = $"SELECT * FROM persontbl WHERE ID = {ID}";
                MySqlCommand cmd = new MySqlCommand(sqlString, conn);
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
            catch (MySqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

        }
        public int SavePerson(Person person)
        {
            MySqlConnection conn;
            string connectionString = ConfigurationManager.ConnectionStrings["persondbConnectionString"].ConnectionString;
            conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();

                string sqlString = "INSERT INTO persontbl (ID, FirstName, LastName, PayRate, StartDate, EndDate) VALUES ( '" + person.ID + "', '" +
                               person.FirstName + "','" + person.LastName + "'," + person.PayRate + ",'" +
                               person.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                               person.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                //string sqlString =$"INSERT INTO `persondb`.`persontbl` (`FirstName`, `LastName`, `PayRate`, `StartDate`, `EndDate`) VALUES('{person.FirstName}', '{person.LastName}', '{person.PayRate}', '{person.StartDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{person.EndDate.ToString("yyyy-MM-dd HH:mm:ss")}')";
                MySqlCommand cmd = new MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                int Id = Convert.ToInt32(cmd.LastInsertedId);
                return Id;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool DeletePerson(int id)
        {
            MySqlConnection conn;
            string connectionString = ConfigurationManager.ConnectionStrings["persondbConnectionString"].ConnectionString;            
            conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();

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
            catch (MySqlException e)
            {
                throw e;
            }
        }
        public bool UpdatePerson(int id, Person person)
        {

            MySqlConnection conn;
            string connectionString = ConfigurationManager.ConnectionStrings["persondbConnectionString"].ConnectionString;
            conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                MySqlDataReader reader = null;
                string sqlString = $"SELECT * FROM persontbl WHERE ID = {id}";
                MySqlCommand cmd = new MySqlCommand(sqlString, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    sqlString = $"UPDATE `persondb`.`persontbl` SET `FirstName`= '{person.FirstName}', `LastName`= '{person.LastName}', `PayRate`= '{person.PayRate}', `StartDate`= '{person.StartDate.ToString("yyyy-MM-dd HH:mm:ss")}', `EndDate`= '{person.EndDate.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `ID`= {id}";
                    cmd = new MySqlCommand(sqlString, conn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}