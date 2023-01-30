using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ADODZ1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = ConfigurationManager.ConnectionStrings["fandv"].ConnectionString;

            SqlConnection connection = new SqlConnection(connString);
            Random random = new Random();
            try { 
                connection.Open();


                SqlCommand insertcommand = new SqlCommand();
                insertcommand.Connection = connection;
                for (int i = 1; i <= 25; i++)
                {
                    insertcommand.CommandText += "insert into fandv " +
                        "(Id,Name,Type,Color,Calories) " +
                        "values (" + i + ",'name" + i + "','vegetables','red'," + random.Next(100, 500) + ");";
                }
                for (int i = 26; i <= 50; i++)
                {
                    insertcommand.CommandText += "insert into fandv " +
                        "(Id,Name,Type,Color,Calories) " +
                        "values ("+i+ ",'name"+i+"','fruits','yellow',"+random.Next(100,500)+");";
                }
                Console.WriteLine("Было добавлено " + insertcommand.ExecuteNonQuery() + " строк");
                /*SqlCommand command = new SqlCommand("select * from fandv", connection);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read()) 
                {
                    int line = 0;
                    if (line == 0)
                    {
                        Console.WriteLine($"{reader.GetString(0)} | {reader.GetString(1)} " +
                            $"| {reader.GetString(2)} | {reader.GetString(3)} " +
                            $"| {reader.GetString(4)}");
                    }
                    else 
                    {
                        Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} " +
                            $"| {reader.GetString(2)} | {reader.GetString(3)} " +
                            $"| {reader.GetInt32(4)}");
                    }
                }*/
                Console.WriteLine("Подключение выполнено успешно.");
            }
            catch ( Exception ex )
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            {
                connection.Close();
                Console.WriteLine("Подключение было закрыто.");
            }
            Console.ReadLine();
        }
    }
}

/*
Добавьте к приложению из второго задания следующую функциональность:
■ Отображение всей информации из таблицы с овощами и фруктами;
■ Отображение всех названий овощей и фруктов;
■ Отображение всех цветов;
Домашнее задание
1
■ Показать максимальную калорийность;
■ Показать минимальную калорийность;
■ Показать среднюю калорийность.
*/