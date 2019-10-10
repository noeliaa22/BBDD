using System;
using System.Data.SqlClient;
namespace EjercicioHotel
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-F3J0NS5\\SQLEXPRESS;Initial Catalog=Hotel;Integrated Security=True");
        static void Main(string[] args)
        {
            Console.WriteLine();
           // Menu();
            Listado();
            Console.WriteLine();
        }
        public static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.WriteLine("\n¿Qué acción quiere realizar?");
            Console.WriteLine("\n1.Registrar cliente");
            Console.WriteLine("\n2.Editar datos del cliente");
            Console.WriteLine("\n3. Nueva Reserva ");
            Console.WriteLine("\n4.Check Out");
            Console.WriteLine("\n5.Exit");
            Console.WriteLine("*********************************");
            int opcion = 0;
            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                switch (opcion)
                {
                    case 1:
                        RegistrarCliente();
                        connection.Close();
                        break;
                    case 2:
                        EditarCliente();
                        connection.Close();
                        break;
                    case 3:
                        CheckIn();
                        connection.Close();
                        break;
                    case 4:
                        CheckOut();
                        connection.Close();
                        break;
                    case 5:                       
                        connection.Close();
                        break;
                    default:
                        Console.WriteLine("Your'e kidding me?? Error number #1043942,43432. go away...");
                        Menu();
                        connection.Close();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n Ánimo campeón(a), inténtalo otra vez\n");
                Menu();
            }
        }
        public static void RegistrarCliente()
        {
            bool dniRegistrado = true;
            string dni;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
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
                    dniRegistrado = false;

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
                    Console.WriteLine("\nCliente registrado");
                    Menu();
                }
            } while (dniRegistrado == true);
        }
        public static void EditarCliente()
        {
            bool completedprogram = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Indícame tu Dni para poder editar tu registro.");//1.Pido el string dni
                string dni = Convert.ToString(Console.ReadLine()).ToUpper();//2.Lo convierto
                string query = $"select NOMBRE,APELLIDO from Clientes Where DNI='{dni.ToUpper()}'";
                //3.Searching for the match in the DataBase.

                SqlCommand command = new SqlCommand(query, connection);//we open.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//execute order.
                if (reader.Read())//if dni matches Dni in data base --> continue
                {
                    connection.Close();//we close so we can open new order.
                    query = $"select NOMBRE,APELLIDO from Clientes Where DNI='{dni}'";
                    command = new SqlCommand(query, connection);
                    connection.Open();
                    reader = command.ExecuteReader();


                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Welcome Mr/rs ");
                    Console.WriteLine("\n Select one of the folowing:");
                    Console.WriteLine("\n 1: Edit First Name");
                    Console.WriteLine("\n 2: Edit Last Name");
                    Console.WriteLine("\n 3: Edit First and Last Name");
                    Console.WriteLine("\n Press 4 to go back to the menu");
                    completedprogram = true;
                    connection.Close();
                    int opcion = 0;
                    if (int.TryParse(Console.ReadLine(), out opcion))
                    {
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
                                Console.WriteLine("Actualizado");
                                Menu();
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
                                Console.WriteLine("Actualizado");
                                Menu();
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
                                Console.WriteLine("Actualizado");
                                Menu();
                                break;
                            case 4:
                                Console.WriteLine("\nExit succesfully");
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine("Your'e kidding me?? Error number #1043942,43432. go away...");
                                Menu();
                                break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n Ánimo campeón(a), inténtalo otra vez\n");
                        EditarCliente();
                    }

                }
                else
                {
                    Console.WriteLine("\nWrong DNI back to the menu");
                    Menu();
                    connection.Close();
                }
            connection.Close();
            } while (!completedprogram);
            connection.Close();
            Menu();

        }
        public static void CheckIn()
        {
            Console.ForegroundColor = ConsoleColor.Green;
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
                        query = $"Insert into Reservas (IDCliente, IDHabitación,FechaCheckin) VALUES ({idCliente},'{numHabitacion}','{fechaEntrada}')"; //o ponerlo como GETDATE(), para que lo ejecute SQL
                        command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                        connection.Open();
                        query = $"Update Habitaciones SET ESTADO ='CLOSED' Where ID='{numHabitacion}'";
                        command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Check In SUCCESFULLY");
                        Menu();
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
                    Console.WriteLine("");
                    Menu();
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
                    string query3 = $"Select IDcliente from Reservas";
                    SqlCommand command3 = new SqlCommand(query3, connection);
                    SqlDataReader reader3 = command3.ExecuteReader();
                    connection.Close();
                    connection.Open();
                    string query4 = $"select ID from Reservas Where IDcliente='{idCliente}'"; //and fechaCheckout is NULL

                    SqlCommand command4 = new SqlCommand(query4, connection);

                    SqlDataReader reader4 = command4.ExecuteReader();


                    //3.Searching for the match in the DataBase.
                    if (reader4.Read())//if dni matches Dni in data base --> continue
                    {
                        string idReserva = reader4[0].ToString();

                        //Console.WriteLine($"Thank you Mr/rs { reader["APELLIDO"].ToString()} ");

                        idReserva = reader4[0].ToString();
                        completedprogram = true;
                        DateTime checkoutdate = DateTime.Now;
                        connection.Close();
                        connection.Open();
                        string query5 = $"Select IDHabitación from Reservas where IDcliente='{idCliente}'";
                        SqlCommand command5 = new SqlCommand(query5, connection);
                        SqlDataReader reader5 = command5.ExecuteReader();
                        reader5.Read();
                        string idHab = reader5[0].ToString();
                        connection.Close();
                        connection.Open();
                        query2 = $"Update Reservas SET FechaCheckOut='{checkoutdate}'Where ID='{idReserva}'";

                        command = new SqlCommand(query2, connection);
                        command.ExecuteNonQuery();
                        connection.Close();//we close so we can open new order.
                        connection.Open();
                        string query6 = $"Update Habitaciones SET ESTADO='OPEN'Where ID='{idHab}'";
                        command = new SqlCommand(query6, connection);//we open.
                        command.ExecuteNonQuery();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Check Out realized, hope you enjoied");
                        Console.WriteLine("\n If you want to go back to the menu");
                        Console.WriteLine("\n 1: Back to the menu");
                        Console.WriteLine("\n 2: Exit");
                        int opcion = 0;
                        if (int.TryParse(Console.ReadLine(), out opcion))
                        {
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
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n Ánimo campeón(a), inténtalo otra vez\n");
                            Console.WriteLine("\n If you want to go back to the menu");
                            Console.WriteLine("\n 1: Back to the menu");
                            Console.WriteLine("\n Esta vez pulse cualquier otra tecla si quiere salir");
                            opcion = 0;
                            if (int.TryParse(Console.ReadLine(), out opcion))
                            {
                                switch (opcion)
                                {
                                    case 1:
                                        Menu();
                                        break;
                                    default:
                                        break;
                                }
                            }

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
    public static void Listado()
    {
            bool dniRegistrado = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("*********************************");
                Console.WriteLine("\n¿Qué acción quiere realizar?");
                Console.WriteLine("\n1.Ver listado de todas las habitaciones con nombre de su huésped o vacía");
                Console.WriteLine("\n2.Ver listado de ocupadas con el nombre de su huésped");
                Console.WriteLine("\n3. Ver listado de habitaciones vacías ");
                Console.WriteLine("\n4.Check Out");
                Console.WriteLine("*********************************");

                int opcion = 0;
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            connection.Open(); //Abrir la conexión
                            string query = $"SELECT Reservas.ID, Habitaciones.ID, Clientes.NOMBRE , Habitaciones.ESTADO " +
                                $"from((Reservas inner join Clientes on Clientes.ID = Reservas.IDCliente) " +
                                $"inner join Habitaciones on Habitaciones.ID = Reservas.IDHabitación) " +
                                $"WHERE Habitaciones.ESTADO LIKE 'CLOSED'"; //Se pide que haga una lectura de todos los dnis registrados
                            SqlCommand command = new SqlCommand(query, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                            SqlDataReader reader = command.ExecuteReader();
                            reader.Read();

                            while (reader.Read()) //en este caso también vale un if porque solo devuelve 1
                            {
                                Console.WriteLine($"{reader["Reservas.ID"].ToString()} {reader["Habitaciones.IS"].ToString()} {reader["Clientes.NOMBRE"].ToString()}");
                            }
                            break;
                            connection.Close();
                        //case 2:
                        //    connection.Open(); //Abrir la conexión
                        //    string query2 = $"SELECT*from Reserva,Habitación,Cliente"; //Se pide que haga una lectura de todos los dnis registrados
                        //    SqlCommand command2 = new SqlCommand(query2, connection); //Conectar el comando con sql, SIEMPRE TIENE QUE ESTAR
                        //    reader = command2.ExecuteReader();
                        //    reader.Read();
                        //    connection.Close();
                        //    break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n Ánimo campeón(a), inténtalo otra vez\n");
                }
            } while (dniRegistrado == true);

        }

    }

}