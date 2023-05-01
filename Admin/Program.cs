using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin;
using System.IO;

namespace Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MUsers> users = new List<MUsers>();
            List<EmployeesData> workers = new List<EmployeesData>();
            List<BusData> buses = new List<BusData>();
            string path = "textfile.txt";
            string path1 = "employees.txt";
            string path2 = "buses.txt";
            Console.Clear();
            int option;
            bool check = readData(path, users);
            if (check)
                Console.WriteLine("Data Loaded SuccessFully");
            else
                Console.WriteLine("Data Not Loaded");

            Console.ReadKey();

            do
            {
                Console.Clear();
                option = menu();
                Console.Clear();
                bool check1 = LoadEmployeesData(path1, workers);
                if (check1)
                    Console.WriteLine("Employees Data Loaded SuccessFully");
                else
                    Console.WriteLine("Employees Data Not Loaded");
                LoadBusData(buses, path2);

                Console.ReadKey();

                if (option == 1)
                {
                    Console.Clear();

                    MUsers user = takeInputWithoutRole();
                    if (user != null)
                    {

                        Console.Clear();

                        user = signIn(user, users);
                        if (user == null)
                            Console.WriteLine("Invalid User");
                        else if (user.IsAdmin())
                        {
                            int adminchoice = AdminMenu();
                            while (true)
                            {

                             
                             
                              

                                if (adminchoice == 1)
                                {
                                    Console.Clear();
                                    int number;
                                    string numbers;
                                    AdminHeader();
                                    Console.WriteLine("Enter the number of employees: ");
                                    numbers = (Console.ReadLine());
                                    number = Intvalidation(numbers);

                                    for (int x = 0; x < number; x++)
                                    {
                                        EmployeesData obj = new EmployeesData();
                                        obj = AddEmployees(workers, path1);
                                        StoreEmployeesInList(workers, obj);



                                    }


                                    Console.ReadKey();
                                    adminchoice = AdminMenu();
                                }
                                else if (adminchoice == 2)
                                {
                                    Console.Clear();
                                    int number1;
                                    string numbers1;
                                    AdminHeader();
                                    Console.WriteLine("Enter the number of employees: ");
                                    numbers1 = (Console.ReadLine());
                                    number1 = Intvalidation(numbers1);
                                    for (int x = 0; x < number1; x++)
                                    {
                                        DeleteEmployee(workers, path1);

                                    }
                                    Console.ReadKey();

                                    adminchoice = AdminMenu();
                                }

                                else if (adminchoice == 3)
                                {
                                    Console.Clear();
                                    AdminHeader();
                                    Updateemployees(workers, path1);
                                    Console.ReadKey();
                                    adminchoice = AdminMenu();
                                }
                                else if (adminchoice == 4)
                                {
                                    Console.Clear();
                                    AdminHeader();
                                    SearchEmployee(workers);
                                    Console.ReadKey();
                                    adminchoice = AdminMenu();

                                }
                                else if (adminchoice == 5)
                                {
                                    Console.Clear();
                                    AdminHeader();
                                    ViewEmployee(workers);
                                    Console.ReadKey();
                                    adminchoice = AdminMenu();


                                }
                                else if (adminchoice == 6)
                                {
                                    int number1;
                                    string numbers1;
                                    Console.Clear();
                                    AdminHeader();
                                    Console.WriteLine("Enter the number of buses: ");
                                    numbers1 = (Console.ReadLine());
                                    number1 = Intvalidation(numbers1);
                                    for (int x = 0; x < number1; x++)
                                    {
                                        BusData bus = new BusData();
                                        bus = AddBus(buses, path2);
                                        Console.ReadKey();




                                    }



                                    adminchoice = AdminMenu();
                                }
                                else if (adminchoice == 7)
                                {
                                    Console.Clear();
                                    AdminHeader();
                                    UpdateBus(buses, path2);
                                    adminchoice = AdminMenu();

                                }
                                else if (adminchoice == 8)
                                {
                                    Console.Clear();
                                    AdminHeader();
                                    SearchBus(buses);
                                    Console.ReadKey();
                                    adminchoice = AdminMenu();
                                }
                                else if (adminchoice == 9)
                                {
                                    Console.Clear();
                                    AdminHeader();
                                    DeleteBus(buses, path2);
                                    Console.ReadKey();
                                    adminchoice = AdminMenu();
                                }
                                else if (adminchoice == 10)
                                {
                                    Console.Clear();
                                    AdminHeader();
                                    ViewBus(buses);
                                    Console.ReadKey();
                                    adminchoice = AdminMenu();
                                }
                                Console.ReadKey();
                            }

                        }
                    }








                    else
                        Console.WriteLine("User Menu");

                }
                else if (option == 2)
                {

                    Console.Clear();

                    MUsers user = takeInputWithRole();
                    if (user != null)
                    {
                        storeDataInFile(path, user);
                        storeDataInList(users, user);

                    }
                }

                Console.ReadKey();
            }
            while (option < 3);
        }
        static int menu()
        {
            Console.WriteLine("1. Sign IN");
            Console.WriteLine("2. Sign UP");
            Console.WriteLine("3. Exit");
            int option;
            option = int.Parse(Console.ReadLine());
            return option;
        }
        static string dataParse(string line, int field)
        {
            string item = "";
            int commacount = 1;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    commacount++;
                }
                else if (commacount == field)
                {
                    item = item + line[i];
                }
            }
            return item;
        }
        static bool readData(string path, List<MUsers> users)

        {
            if (File.Exists(path))
            {
                StreamReader fileVariable = new StreamReader(path);
                string record;
                while ((record = fileVariable.ReadLine()) != null)
                {
                    string name = dataParse(record, 1);
                    string password = dataParse(record, 2);
                    string role = dataParse(record, 3);
                    MUsers user = new MUsers(name, password, role);
                    storeDataInList(users, user);
                }
                fileVariable.Close();
                return true;
            }
            return false;
        }
        static MUsers takeInputWithoutRole()
        {
            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();
            if (name != null && password != null)
            {
                MUsers user = new MUsers(name, password);
                return user;
            }
            return null;

        }
        static MUsers takeInputWithRole()
        {
            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter Role: ");
            string role = Console.ReadLine();
            if (name != null && password != null && role != null)
            {
                MUsers user = new MUsers(name, password, role);
                return user;
            }
            return null;
        }
        static void storeDataInList(List<MUsers> users, MUsers user)
        {
            users.Add(user);
        }
        static void storeDataInFile(string path, MUsers user)
        {
            StreamWriter file = new StreamWriter(path, true);
            file.WriteLine(user.name + "," + user.password + "," + user.role);
            file.Flush();
            file.Close();
        }
        static MUsers signIn(MUsers user, List<MUsers> users)
        {
            foreach (MUsers storedUser in users)
            {
                if (user.name == storedUser.name && user.password == storedUser.password)
                {
                    return storedUser;
                }
            }
            return null;
        }
        static void main_header()
        {

            Console.WriteLine("      >>        >=>              >=======>                                              >=>       >=>              >=>                              ");
            Console.WriteLine("     >>=>       >=>              >=>                      >=>                           >> >=>   >>=>              >=>                              ");
            Console.WriteLine("    >> >=>      >=>              >=>          >=> >=>            >=> >=>  >> >==>       >=> >=> > >=>    >=>     >=>>==>    >=>     >> >==>  >===>  ");
            Console.WriteLine("   >=>  >=>     >=> >====>       >=====>    >=>   >=>     >=>  >=>   >=>   >=>          >=>  >=>  >=>  >=>  >=>    >=>    >=>  >=>   >=>    >=>     ");
            Console.WriteLine("  >=====>>=>    >=>              >=>       >=>    >=>     >=> >=>    >=>   >=>          >=>   >>  >=> >=>    >=>   >=>   >=>    >=>  >=>      >==>  ");
            Console.WriteLine(" >=>      >=>   >=>              >=>        >=>   >=>     >=>  >=>   >=>   >=>          >=>       >=>  >=>  >=>    >=>    >=>  >=>   >=>        >=> ");
            Console.WriteLine(">=>        >=> >==>              >=>         >==>>>==>    >=>   >==>>>==> >==>          >=>       >=>    >=>        >=>     >=>     >==>    >=> >=> ");
            Console.WriteLine("                                                       >==>                                                                                         ");
            Console.WriteLine("");
            Console.WriteLine("");

        }
        static void AdminHeader()
        {

            Console.WriteLine("   _   _   _   _   _   _   _     _   _   _   _     _   _   _   _   _  ");
            Console.WriteLine("  / \\ / \\ / \\ / \\ / \\ / \\ / \\   / \\ / \\ / \\ / \\   / \\ / \\ / \\ / \\ / \\ ");
            Console.WriteLine(" ( W | e | l | c | o | m | e ) ( D | e | a | r ) ( A | d | m | i | n )");
            Console.WriteLine("  \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/   \\_/ \\_/ \\_/ \\_/   \\_/ \\_/ \\_/ \\_/ \\_/ ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");



        }
        // Admin FUNCTIONS
        static int AdminMenu()
        {
            Console.Clear();
            AdminHeader();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" You can do these tasks .");
            Console.WriteLine(" -----------------------------------------");
            Console.WriteLine(" 1.  add employee.");
            Console.WriteLine(" 2.  delete employee.");
            Console.WriteLine(" 3.  update employee.");
            Console.WriteLine(" 4.  search employee.");
            Console.WriteLine(" 5.  view all employees .");
            Console.WriteLine(" 6.  add bus with timing and route .");
            Console.WriteLine(" 7.  update bus");
            Console.WriteLine(" 8.  search bus");
            Console.WriteLine(" 9.  delete bus .");
            Console.WriteLine(" 10. view all buses .");
            Console.WriteLine(" 0.  exit.");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Your choice: ");
            string opt = (Console.ReadLine());
            int option = Intvalidation(opt);
            return option;

        }
        static int Intvalidation(string number)
        {
            if (int.TryParse(number, out int result))
            {
                return result;
            }
            string num = "er";
            int res = 0;
            while (!int.TryParse(num.ToString(), out res))
            {
                Console.WriteLine("Enter integer number: ");
                num = Console.ReadLine();
            }
            return res;
        }
        static EmployeesData AddEmployees(List<EmployeesData> workers, string path1)
        {
            Console.WriteLine("Enter employee name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter employee type: ");
            string type = Console.ReadLine();
            if (name != null && type != null)
            {

                EmployeesData obj = new EmployeesData(name, type);
                StoreEmployeesInFile(path1, obj);

                return obj;
            }

            return null;
        }
        static bool StoreEmployeesInList(List<EmployeesData> workers, EmployeesData user)
        {

            workers.Add(user);
            return true;
        }
        static void StoreEmployeesInFile(string path1, EmployeesData user)
        {
            StreamWriter file = new StreamWriter(path1, true);
            file.WriteLine(user.Emp_name + "," + user.Emp_type);
            file.Flush();
            file.Close();
        }
        static bool LoadEmployeesData(string path1, List<EmployeesData> users)

        {
            if (File.Exists(path1))
            {
                StreamReader fileVariable = new StreamReader(path1);
                string record;
                while ((record = fileVariable.ReadLine()) != null)
                {
                    string name = dataParse(record, 1);
                    string type = dataParse(record, 2);

                    EmployeesData user = new EmployeesData(name, type);
                    StoreEmployeesInList(users, user);
                  
                }
                fileVariable.Close();
                return true;
            }
            return false;
        }
        static void DeleteEmployee(List<EmployeesData> workers, string path1)
        {
            int index = 0;
            Console.WriteLine("Enter the name of employee to delete: ");
            string name = Console.ReadLine();
            for (int i = 0; i < workers.Count(); i++) // this loop will give the index to delete
            {

                if (name == workers[i].Emp_name)
                {
                    index = i;

                }
            }
            workers.RemoveAt(index);
            StoreUpdatedEmployeesInFile(workers, path1);


        }
        static void Updateemployees(List<EmployeesData> workers, string path1)
        {
            int index = 0;
            Console.WriteLine("Enter the name of employee to change: ");
            string name = Console.ReadLine();
            for (int i = 0; i < workers.Count; i++) // this loop will give the index to delete
            {

                if (name == workers[i].Emp_name)
                {
                    index = i;

                }
            }
            Console.WriteLine("Enter updated name: ");
            string upName = Console.ReadLine();
            workers[index].Emp_name = upName;



        }
        static void SearchEmployee(List<EmployeesData> workers)
        {
            Console.WriteLine("Enter the name of employee to search: ");
            string name = Console.ReadLine();
            Console.WriteLine(workers.Count());
            Console.ReadKey();
            for (int i = 0; i < workers.Count; i++) // this loop will give the index to delete
            {
                Console.Write("Name: ");
                Console.WriteLine(workers[i].Emp_name);


                if (name == workers[i].Emp_name)
                {
                    Console.Write("this workers is : ");
                    Console.WriteLine(workers[i].Emp_type);
                    break;

                }
            }


        }
        static void StoreUpdatedEmployeesInFile(List<EmployeesData> workers, string path1)
        {
            StreamWriter file = new StreamWriter(path1);
            for (int x = 0; x < workers.Count(); x++)
            {
                file.WriteLine(workers[x].Emp_name + "," + workers[x].Emp_type);
            }
            file.Flush();
            file.Close();
        }
        static void ViewEmployee(List<EmployeesData> workers)
        {
            Console.WriteLine("Name \t" + "\t\t Type");
            for (int x = 0; x < workers.Count(); x++)
            {
                Console.WriteLine(workers[x].Emp_name + "\t\t\t" + workers[x].Emp_type);
            }

        }
        static BusData AddBus(List<BusData> buses, string path2)
        {

            Console.WriteLine("Enter the Serial number of bus: ");
            string busSerial = Console.ReadLine();
            Console.WriteLine("Enter the timing of the bus: ");
            string busTiming = Console.ReadLine();
            Console.WriteLine("Enter the route of the bus : ");
            string busRoute = Console.ReadLine();
            Console.WriteLine("Enter the date of departure :  ");
            string date = Console.ReadLine();
            BusData info = new BusData(busSerial, busTiming, busRoute, date);
            StoreBusToFile(info, path2);
            AddBusToList(buses, info);
            return info;

        }
        static void AddBusToList(List<BusData> buses, BusData obj)
        {
            buses.Add(obj);
        }
        static void StoreBusToFile(BusData obj, string path2)
        {

            StreamWriter file = new StreamWriter(path2, true);
            file.WriteLine(obj.busSerial + "," + obj.busTiming + "," + obj.busRoute + "," + obj.date);
            file.Flush();
            file.Close();

        }
        static void StoreUpdatedBusToFile(List<BusData> buses, string path2)
        {
            StreamWriter file = new StreamWriter(path2);
            for (int x = 0; x < buses.Count(); x++)
            {
                file.WriteLine(buses[x].busSerial + "," + buses[x].busTiming + "," + buses[x].busRoute + "," + buses[x].date);
            }
            file.Flush();
            file.Close();
        }
        static void LoadBusData(List<BusData> buses, string path2)
        {
            if (File.Exists(path2))
            {
                StreamReader file = new StreamReader(path2);
                string line;
                while ((line = file.ReadLine()) != null)
                {


                    string busSerial = dataParse(line, 1);
                    string busTiming = dataParse(line, 2);
                    string busRoute = dataParse(line, 3);
                    string date = dataParse(line, 4);
                    BusData info = new BusData(busSerial, busTiming, busRoute, date);
                  


                    buses.Add(info);



                }
                file.Close();
            }
            else
            {
                Console.WriteLine("File does not exists");
            }

        }
        static void UpdateBus(List<BusData> buses, string path2)
        {

            string route;
            Console.WriteLine("Enter the serial number of the bus you want to change the route: ");
            string number = Console.ReadLine();
            for (int x = 0; x < buses.Count(); x++)
            {
                if (number == buses[x].busSerial)
                {
                    Console.WriteLine("Enter the updated route : ");
                    route = Console.ReadLine();
                    buses[x].busRoute = route;
                }
            }
            StoreUpdatedBusToFile(buses, path2);
        }
        static void DeleteBus(List<BusData> buses, string path2)
        {

            Console.WriteLine("Enter the serial of bus to delete: ");
            string number = Console.ReadLine();
            for (int x = 0; x < buses.Count(); x++)
            {
                if (number == buses[x].busSerial)
                {
                    buses.RemoveAt(x);
                }
            }
            StoreUpdatedBusToFile(buses, path2);
        }
        static void SearchBus(List<BusData> buses)
        {
            Console.WriteLine("Enter the route to search: ");
            string route = Console.ReadLine();
            for (int x = 0; x < buses.Count(); x++)
            {
                if (route == buses[x].busRoute)
                {
                    Console.WriteLine("Timing of route :{0} ", buses[x].busTiming);
                    Console.WriteLine("Serial of bus :{0} ", buses[x].busSerial);
                    Console.WriteLine("Date of departure of route :{0} ", buses[x].date);

                }
            }
        }
        static void ViewBus(List<BusData> buses)
        {
            Console.WriteLine("Serial \t\t Timing \t Date \t\t Route");
            for (int x = 0; x < buses.Count(); x++)
            {
                Console.WriteLine(buses[x].busSerial + "\t\t" + buses[x].busTiming + "\t\t" + buses[x].date + " \t\t " + buses[x].busRoute);
            }
        }

    }
}
