using System;
using System.Data.SqlClient;

namespace EjerciciosBBDD
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-F3J0NS5\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True"); //Actualizar el servidor y especificar la BBDD
            bool apellidoCorrecto = false;                                                                                                                                            /*string query = "SELECT*FROM Employees";*/ //crear un comando


            do
            {
                Console.WriteLine("Introduce tu apellido");
                string apellido = Console.ReadLine();
                //1º COMANDO
                string query = $"SELECT*from Employees WHERE LastName='{apellido}'";
                SqlCommand command = new SqlCommand(query, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                connection.Open(); //Abrir la conexión
                SqlDataReader reader = command.ExecuteReader(); //CADA VEZ QUE HAY UN SELECT SE NECESITA UN READER

                if (reader.Read()) //Devuelve un booleano
                {
                    connection.Close(); //cierra la conexion del primer comando
                    apellidoCorrecto = true;
                    Console.WriteLine("Introduce tu país de origen");
                    string country = Console.ReadLine();

                    //2º COMANDO, actualizando la query de antes
                    connection.Open(); //abre la conexión del segundo comando
                    query = $"Update Employees SET Country='{country}' Where LastName='{apellido}'";
                    command = new SqlCommand(query, connection); //creo un segundo comando para que ejecute otra acción
                    Console.WriteLine(command.ExecuteNonQuery() + " rows affected");
                    connection.Close();

                    //Para mostrarlo actualizado
                    query = $"select*from Employees where LastName like'{apellido}'";
                    command = new SqlCommand(query, connection); //creo un segundo comando para que ejecute otra acción
                    connection.Open(); //abre la conexión del segundo comando
                    reader = command.ExecuteReader(); //CADA VEZ QUE HAY UN SELECT SE NECESITA UN READER
                    while (reader.Read()) //en este caso también vale un if porque solo devuelve 1
                    {
                        Console.WriteLine($"{reader["LastName"].ToString()} {reader["Country"].ToString()}");
                    }
                    connection.Close();

                }
                else
                {
                    Console.WriteLine("Tu apellido no está registrado, introducelo otra vez");                   
                    connection.Close(); //IMPORTANTE
                }
                
            } while (apellidoCorrecto==false); //!apellidoCorrecto
            
            
            
            
            //string country = "ESP";
            //string query = $"Update Employees SET Country='{country}' Where Country='AUS'";
            //SqlCommand command = new SqlCommand(query,connection); //Conectar el comando con sql
            //connection.Open(); //Abrir la conexión
            //Console.WriteLine(command.ExecuteNonQuery()+" rows affected"); //No es una query, es una orden que se ejecuta una vez y ya esta
            
            
            
            //SqlDataReader reader = command.ExecuteReader(); //Se encarga de leer los registros, lo que devuelva el comando

            //Utilizar un bucle para leer registros, un while porque no sabes la cantidad de registros
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader[5].ToString()); //Las columnas son arrays
            //}

            //while (reader.Read())
            //{
            //    Console.WriteLine($"{reader[0].ToString()} {reader[1].ToString()} {reader[2].ToString()} {reader[3].ToString()}");
            //}

             //IMPORTANTE DESPUÉS DE EJECUTAR
        }
    }
}
