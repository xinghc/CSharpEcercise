using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteExe
{
    static class Constants
    {
        public const string DB_FILENAME = "MyDatabase.sqlite";
    }
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection m_dbConnection;
            string sql_act;

            if (!File.Exists(Constants.DB_FILENAME))
            {
                SQLiteConnection.CreateFile(Constants.DB_FILENAME);
            }
            sql_act = "Data Source=" + Constants.DB_FILENAME + "; Version=3;";
            m_dbConnection = new SQLiteConnection(sql_act);
            m_dbConnection.Open();

            sql_act = "create table if not exists highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql_act, m_dbConnection);
            command.ExecuteNonQuery();
            sql_act = "insert into highscores (name, score) values ('Me', 3000)";
            command = new SQLiteCommand(sql_act, m_dbConnection);
            command.ExecuteNonQuery();
            sql_act = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql_act, m_dbConnection);
            command.ExecuteNonQuery();
            sql_act = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql_act, m_dbConnection);
            command.ExecuteNonQuery();

            sql_act = "select * from highscores order by score desc";
            command = new SQLiteCommand(sql_act, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);

            m_dbConnection.Close();
            Console.ReadLine();
        }
    }
}
