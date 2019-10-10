using System;
using System.Data.SqlClient;

namespace EjercicioHotel
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-F3J0NS5\\SQLEXPRESS;Initial Catalog=Hotel;Integrated Security=True");
        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            Console.WriteLine($"¿Qué acción quiere realizar?\n 1. Registrar cliente \n 2. Editar datos del cliente \n 3. Nueva Reserva \n 4. Check Out");
            int opcion = Convert.ToInt32(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    RegistrarCliente();
                    break;
                case 2:
                    EditarCliente();
                    break;
                case 3:
                    CheckIn();
                    break;
                case 4:
                    CheckOut();
                    break;
                default:
                    Console.WriteLine("Your'e kidding me?? Error number #1043942,43432. go away...");
                    Menu();
                    break;
            }
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
                    dniRegistrado = false;
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
                    command = new SqlCommand(query, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                    command.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine("\n Su registro se ha realizado con éxito");
                }
            } while (dniRegistrado == true);
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
                            Console.WriteLine("Insert your 'new' First Name");
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
                            query = $"Update Clientes SET APELLIDO='{newLastName2.ToUpper()}'Where DNI='{dni}'";//aqui ejecuta sql, por lo tanto son comandos sql.
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
                    Console.WriteLine("\nWrong DNI back to the menu");
                    Menu();
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

                query = $"SELECT *from Habitaciones WHERE ESTADO like 'OPEN'";
                connection.Open(); //Abrir la conexión
                command = new SqlCommand(query, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                reader = command.ExecuteReader();

                while (reader.Read()) //en este caso también vale un if porque solo devuelve 1
                {
                    Console.WriteLine($"{reader["ID"].ToString()} {reader["ESTADO"].ToString()}");
                }
                connection.Close();

                DateTime fechaEntrada = DateTime.Now;
                bool habitacionCorrecta = false;
                do
                {
                    Console.WriteLine("Seleccione un número de habitacion");
                    string numHabitacion = Console.ReadLine();
                    query = $"SELECT ID from Habitaciones WHERE ESTADO like 'OPEN' and ID={numHabitacion}";
                    connection.Open(); //Abrir la conexión
                    command = new SqlCommand(query, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        habitacionCorrecta = true;
                        connection.Close();
                        connection.Open();
                        query = $"Insert into Reservas (IDCliente, IDHabitación,FechaCheckin) VALUES ({idCliente},'{numHabitacion}','{fechaEntrada}')";
                        command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();

                        connection.Open();
                        query = $"Update Habitaciones SET ESTADO ='CLOSED' Where ID='{numHabitacion}'";
                        command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                        Console.WriteLine("Reserva realizada con éxito");
                    }
                    else
                    {
                        Console.WriteLine("La habitación seleccionada no está disponible");
                    }

                } while (habitacionCorrecta == false);

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
                    Console.WriteLine("\n Esperamos que vuelva pronto");
                }

            }
        }
        public static void CheckOut()
        {
            bool completedprogram = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Indícame tu Dni para realizar tu CheckOut.");//1.Pido el string dni
                string dni = Console.ReadLine();//2.Lo convierto
                connection.Open();
                string query = $"select NOMBRE,APELLIDO from Clientes Where DNI='{dni.ToUpper()}'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();//execute order.
                if (reader.Read())
                {
                    connection.Close();
                    connection.Open();
                    string query2 = $"select ID from Clientes Where DNI='{dni.ToUpper()}'";
                    SqlCommand command2 = new SqlCommand(query2, connection);//we open.
                    SqlDataReader reader2 = command2.ExecuteReader();
                    reader2.Read();
                    string idCliente = reader2[0].ToString();
                    connection.Close();
                    connection.Open();
                    string query3 = $"select IDHabitación from Reservas Where IDcliente='{idCliente}'";
                    SqlCommand command3 = new SqlCommand(query3, connection);
                    SqlDataReader reader3 = command3.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    string query4 = $"Select IDcliente from Reservas";
                    SqlCommand command4 = new SqlCommand(query4, connection);
                    SqlDataReader reader4 = command4.ExecuteReader();

                    //3.Searching for the match in the DataBase.
                    if (reader4.Read())//if dni matches Dni in data base --> continue
                    {
                        string idHab = reader4[0].ToString();
                        //Console.WriteLine($"Thank you Mr/rs { reader["APELLIDO"].ToString()} ");

                        idCliente = reader4[0].ToString();
                        completedprogram = true;
                        DateTime checkoutdate = DateTime.Now;
                        connection.Close();
                        connection.Open();
                        query2 = $"Update Reservas SET FechaCheckOut='{checkoutdate}'Where IDCliente='{idCliente}'";
                        command = new SqlCommand(query2, connection);
                        command.ExecuteNonQuery();
                        connection.Close();//we close so we can open new order.
                        connection.Open();
                        string query5 = $"Update Habitaciones SET ESTADO='OPEN'Where ID='{idHab}'";
                        command = new SqlCommand(query5, connection);//we open.
                        command.ExecuteNonQuery();

                        //query = $"Update Habitaciones SET ESTADO='OPEN' Where ID='{idHab}'";
                        //command = new SqlCommand(query, connection);//we open.
                        //command.ExecuteNonQuery();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Check Out realized, hope you enjoied");
                        Console.WriteLine("\n If you want to go back to the menu");
                        Console.WriteLine("\n 1: Back to the menu");
                        Console.WriteLine("\n 2: Exit");
                        int opcion = Convert.ToInt32(Console.ReadLine());
                        switch (opcion)
                        {
                            case 1:
                                Menu();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n Wrong option, no check in found in registry.");
                        Console.WriteLine("\n Back to the menu.");
                        connection.Close();
                        Menu();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLo sentimos su DNI no existe, Pruebe con otro Dni.");
                    connection.Close();
                    Console.WriteLine("\n Back to the menu.");
                    Menu();
                }
            } while (!completedprogram);
            connection.Close();
        }
    }
}

