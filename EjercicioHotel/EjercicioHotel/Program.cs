using System;
using System.Data.SqlClient;

namespace EjercicioHotel
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-F3J0NS5\\SQLEXPRESS;Initial Catalog=Hotel;Integrated Security=True");
        static void Main(string[] args)
        {
            Console.WriteLine("Elija una opción: 1.Registro, 2. Checkin");
            int opcion = Convert.ToInt32(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    RegistrarCliente();
                    break;
                case 2:
                    CheckIn();
                    break;
            }
            
           

        }
        public static void Menu(int opcion)
        {
            Console.WriteLine("¿Qué acción quiere realizar?\n 1. Registrar cliente \n 2. Editar datos del cliente \n3. Check-In");

        }
        public static void RegistrarCliente()
        {
            bool dniRegistrado = true;
            string dni;

            do
            {
                Console.WriteLine("Introduce tu DNI (con letra), si desea salir pulse 0");
                dni = Console.ReadLine().ToUpper();
                string query = $"SELECT*from Clientes WHERE DNI='{dni}'"; //Se pide que haga una lectura de todos los dnis registrados
                SqlCommand command = new SqlCommand(query, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                connection.Open(); //Abrir la conexión
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) //Si lo lee significa que el dni ya existe
                {
                    connection.Close();
                    Console.WriteLine($"Ya hay un cliente registrado con el DNI {dni}, introdúzcalo otra vez");
                }
                else if (dni == "0")
                {
                    connection.Close();
                    Console.WriteLine("No se ha registrado ningún cliente");
                }
                else
                {
                    connection.Close();
                    dniRegistrado = false;
                    Console.WriteLine("Introduce tu nombre");
                    string firstName = Console.ReadLine().ToUpper();
                    Console.WriteLine("Introduce tu apellido");
                    string lastName = Console.ReadLine().ToUpper();
                    command = new SqlCommand(query, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                    connection.Open(); //Abrir la conexión                                      
                    query = $"Insert into Clientes (NOMBRE,APELLIDO,DNI) Values('{firstName}','{lastName}','{dni}')"; //Revisar que los nombres de las columnas estén escritas igual que en SQL
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            } while (dniRegistrado == true || dni == "0");

        }
        public static void EditarCliente()
        {
            bool completedprogram = false;
            do
            {
                Console.WriteLine("Indícame tu Dni para poder editar tu registro.");//1.Pido el string dni
                string dni = Convert.ToString(Console.ReadLine());//2.Lo convierto
                string query = $"select NOMBRE,APELLIDO from Clientes Where DNI='{dni.ToUpper()}'";
                //3.Searching for the match in the DataBase.
                SqlCommand command = new SqlCommand(query, connection);//we open.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//execute order.
                if (reader.Read())//if dni matches Dni in data base --> continue
                {
                    connection.Close();//we close so we can open new order.
                    connection.Open();
                    query = $"select NOMBRE,APELLIDO from Clientes Where DNI='{dni.ToUpper()}'";
                    command = new SqlCommand(query, connection);
                    Console.WriteLine("Welcome Mr/rs " + command.ExecuteReader());
                    Console.WriteLine("\n Select one of the folowing:");
                    Console.WriteLine("\n 1: Edit First Name");
                    Console.WriteLine("\n 2: Edit Last Name");
                    Console.WriteLine("\n 3: Edit First and Last Name");
                    Console.WriteLine("\n Press 0 to exit");
                    completedprogram = true;
                    int opcion = Convert.ToInt32(Console.ReadLine());

                    connection.Close();
                    switch (opcion)
                    {
                        case 1:
                            connection.Open();
                            Console.WriteLine("Insert your '*'new'*' First Name");
                            string newFirstName = Console.ReadLine();
                            query = $"Update Clientes SET NOMBRE='{newFirstName.ToUpper()}'Where DNI='{dni}'";//aqui ejecuta sql, por lo tanto son comandos sql.
                            command = new SqlCommand(query, connection);
                            Console.WriteLine($"Actualized data: { newFirstName} with the ID { dni} associated");//escribimos
                            command.ExecuteNonQuery();
                            connection.Close();
                            break;
                        case 2:
                            connection.Open();
                            Console.WriteLine("Insert your '*'new'*' Last Name");
                            string newLastName = Console.ReadLine();
                            query = $"Update Clientes SET APELLIDO='{newLastName.ToUpper()}'Where DNI='{dni}'";//aqui ejecuta sql, por lo tanto son comandos sql.
                            command = new SqlCommand(query, connection);
                            Console.WriteLine($"Actualized data: {newLastName} with the ID {dni} associated");
                            command.ExecuteNonQuery();
                            connection.Close();
                            break;
                        case 3:
                            connection.Open();
                            Console.WriteLine("Insert your '*'new'*' First Name");
                            string newFirstName2 = Console.ReadLine();
                            query = $"Update Clientes SET NOMBRE='{newFirstName2.ToUpper()}'Where DNI='{dni}'";//aqui ejecuta sql, por lo tanto son comandos sql.
                            command = new SqlCommand(query, connection);
                            Console.WriteLine("Insert your '*'new'*' Last Name");
                            command.ExecuteNonQuery();
                            connection.Close();
                            connection.Open();
                            string newLastName2 = Console.ReadLine();
                            query = $"Update Clientes SET NOMBRE='{newLastName2.ToUpper()}'Where DNI='{dni}'";//aqui ejecuta sql, por lo tanto son comandos sql.
                            command = new SqlCommand(query, connection);
                            Console.WriteLine($"Actualized data: {newFirstName2 } {newLastName2} with the ID {dni} associated");
                            command.ExecuteNonQuery();
                            connection.Close();
                            break;
                        case 0:
                            Console.WriteLine("Exit succesfully");
                            break;
                        default:
                            Console.WriteLine("Your'e kidding me?? Error number #1043942,43432. go away...");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nLo sentimos su DNI no existe, Pruebe con otro Dni o registre su ingreso.");
                    connection.Close();
                }
            } while (!completedprogram);
            connection.Close();
        }

        public static void CheckIn()
        {
            Console.WriteLine("Introduzca su DNI");
            string dni = Console.ReadLine().ToUpper();
            string query = $"SELECT ID from Clientes WHERE DNI='{dni}'"; //Se pide que haga una lectura de todos los dnis registrados
            SqlCommand command = new SqlCommand(query, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
            connection.Open(); //Abrir la conexión
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                int idCliente = Convert.ToInt32(reader[0].ToString());
                connection.Close();
              
                string query2 = $"SELECT ID from Habitaciones WHERE ESTADO like 'OPEN'";
                connection.Open(); //Abrir la conexión
                SqlCommand command2 = new SqlCommand(query2, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                SqlDataReader reader2 = command2.ExecuteReader();
                //int idHabitacion = Convert.ToInt32(reader[0].ToString());

                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine($"{reader2[i].ToString()}");
                }

                DateTime fechaEntrada = DateTime.Now;
                bool habitacionCorrecta = false;
                do
                {
                Console.WriteLine("Seleccione un número de habitacion");
                int numHabitacion = Convert.ToInt32(Console.ReadLine());
                if (reader.Read())
                {
                    habitacionCorrecta = true;
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    query = $"Insert into Reservas (IDCliente, IDHabitación,FechaCheckin) VALUES ({idCliente},{numHabitacion},'{fechaEntrada}')";
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    query = $"Update Habitaciones SET Estado='CLOSED' Where ID={numHabitacion})";
                    connection.Close();
                }
                else
                {
                    Console.WriteLine("La habitación seleccionada no está disponible");
                }

                } while (habitacionCorrecta==false);

            }
            else
            {
                connection.Close();
                Console.WriteLine("Debe registrarse para poder hacer una reserva");
                Console.WriteLine("¿Desea registrarse?\n 1. Si \n 2.No");
                int registro = Convert.ToInt32(Console.ReadLine());
                if (registro == 1)
                {
                    RegistrarCliente();
                }

                else
                {
                    Console.WriteLine("");
                }

            }
        }




    }
}
