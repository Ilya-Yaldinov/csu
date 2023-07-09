using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npgsql;

public class DatabaseConnection
{
    private string connectionString = "Host=194.147.87.32;Port=5432;Database=Yaldinov;Username=Yaldinov;Password=Yaldinov";

    void Start()
    {
        // Создание подключения
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);

        try
        {
            // Открытие подключения
            connection.Open();

            Debug.Log("Успешное подключение к базе данных PostgreSQL!");
            
            // Здесь вы можете выполнять операции с базой данных
            
            // Закрытие подключения
            connection.Close();
        }
        catch (NpgsqlException ex)
        {
            Debug.LogError("Ошибка подключения к базе данных PostgreSQL: " + ex.Message);
        }
    }
}
