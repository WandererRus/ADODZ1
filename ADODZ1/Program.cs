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
                Console.WriteLine("Подключение выполнено успешно.");

                /*SqlCommand insertcommand = new SqlCommand();
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
                Console.WriteLine("Было добавлено " + insertcommand.ExecuteNonQuery() + " строк");*/

                SqlCommand command = new SqlCommand("select * from fandv", connection);
                SqlDataReader reader = command.ExecuteReader();

                int line = 0;
                while (reader.Read()) 
                {                    
                    if (line == 0)
                    {
                        Console.WriteLine($"{reader.GetName(0)} | {reader.GetName(1)} " +
                            $"| {reader.GetName(2)} | {reader.GetName(3)} " +
                            $"| {reader.GetName(4)}");                       
                    }
                    Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} " +
                        $"| {reader.GetString(2)} | {reader.GetString(3)} " +
                        $"| {reader.GetInt32(4)}");
                    line++;
                }
                reader.Close();
                Console.WriteLine("\n-----------------------------------------------------------\n");
                command.CommandText = "select Name from fandv";
                reader = command.ExecuteReader();

                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.WriteLine(reader.GetName(0));
                    }
                    Console.WriteLine(reader.GetString(0));
                    line++;
                }
                reader.Close();
                Console.WriteLine("\n-----------------------------------------------------------\n");
                command.CommandText = "select DISTINCT(Color) from fandv";
                reader = command.ExecuteReader();

                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.WriteLine(reader.GetName(0));
                    }
                    Console.WriteLine(reader.GetString(0));
                    line++;
                }
                reader.Close();
                Console.WriteLine("\n-----------------------------------------------------------\n");
                command.CommandText = "select MAX(Calories) from fandv";
                Console.WriteLine("Максимальная калорийность: " + (int)command.ExecuteScalar());
                Console.WriteLine("\n-----------------------------------------------------------\n");
                command.CommandText = "select MIN(Calories) from fandv";
                Console.WriteLine("Минимальная калорийность: " + (int)command.ExecuteScalar());
                Console.WriteLine("\n-----------------------------------------------------------\n");
                command.CommandText = "select AVG(Calories) from fandv";
                Console.WriteLine("Средняя калорийность: " + (int)command.ExecuteScalar());
                Console.WriteLine("\n-----------------------------------------------------------\n");
                Console.WriteLine("\n-----------------------------------------------------------\n");
                Console.WriteLine("\n-----------------------------------------------------------\n");

                command.CommandText = "select COUNT(*) from fandv WHERE Type = 'vegetables'";
                Console.WriteLine("Количество овощей: " + (int)command.ExecuteScalar());
                Console.WriteLine("\n-----------------------------------------------------------\n");
                command.CommandText = "select COUNT(*) from fandv WHERE Type = 'fruits'";
                Console.WriteLine("Количество фруктов: " + (int)command.ExecuteScalar());
                Console.WriteLine("\n-----------------------------------------------------------\n");
                command.CommandText = "select COUNT(*) from fandv WHERE Color = 'red'";
                Console.WriteLine("Количество фруктов и овощей красного цвета: " + (int)command.ExecuteScalar());
                Console.WriteLine("\n-----------------------------------------------------------\n");

                /*
                 *  ?????????????????????????????????????????????????????????????????????????????????????????????????????
                 * 
                 * command.CommandText = "SELECT Colors.Color,COUNT(*) " +
                    "from fandv,(select DISTINCT(Color) from fandv) AS Colors " +
                    "WHERE  Colors.Color = fandv.Color GROUP BY Colors.Color";
                reader = command.ExecuteReader();

                DataTable dt = reader.GetSchemaTable();

                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        Console.WriteLine(dc.ColumnName +" " + dr[dc]);
                    }
                }
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.WriteLine(reader.GetName(0), reader.GetName(1));
                    }
                    Console.WriteLine(reader.GetString(0), reader.GetInt32(1));
                    line++;
                }
                reader.Close();
                Console.WriteLine("\n-----------------------------------------------------------\n");
                
                 ?????????????????????????????????????????????????????????????????????????????????????????????????????
                 
                 */

                command.CommandText = "select DISTINCT(Color) from fandv";
                reader = command.ExecuteReader();
                List<string> colors = new List<string>();
                while (reader.Read())
                {
                    colors.Add(reader.GetString(0));                    
                }
                reader.Close();
                string result = "";

                foreach (string color in colors) 
                {
                    command.CommandText = "select COUNT(*) from fandv WHERE Color LIKE '" + color +"'";
                    result += color + " : " + (int)command.ExecuteScalar() + "\n";
                }
                Console.WriteLine(result);
                Console.WriteLine("\n-----------------------------------------------------------\n");
                command.CommandText = "select Name from fandv WHERE Calories < " + Console.ReadLine();
                reader = command.ExecuteReader();

                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.WriteLine(reader.GetName(0));
                    }
                    Console.WriteLine(reader.GetString(0));
                    line++;
                }
                reader.Close();
                Console.WriteLine("\n-----------------------------------------------------------\n");

                command.CommandText = "select Name from fandv WHERE Calories > " + Console.ReadLine();
                reader = command.ExecuteReader();

                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.WriteLine(reader.GetName(0));
                    }
                    Console.WriteLine(reader.GetString(0));
                    line++;
                }
                reader.Close();
                Console.WriteLine("\n-----------------------------------------------------------\n");

                command.CommandText = "select Name from fandv WHERE Calories BETWEEN " + Console.ReadLine() + "AND " + Console.ReadLine();
                reader = command.ExecuteReader();

                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.WriteLine(reader.GetName(0));
                    }
                    Console.WriteLine(reader.GetString(0));
                    line++;
                }
                reader.Close();
                Console.WriteLine("\n-----------------------------------------------------------\n");

                command.CommandText = "select * from fandv WHERE Color LIKE 'red' OR Color LIKE 'yellow'";
                reader = command.ExecuteReader();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.WriteLine($"{reader.GetName(0)} | {reader.GetName(1)} " +
                            $"| {reader.GetName(2)} | {reader.GetName(3)} " +
                            $"| {reader.GetName(4)}");
                    }
                    Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} " +
                        $"| {reader.GetString(2)} | {reader.GetString(3)} " +
                        $"| {reader.GetInt32(4)}");
                    line++;
                }
                reader.Close();
                Console.WriteLine("\n-----------------------------------------------------------\n");
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