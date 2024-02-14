using Bogus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10_OOP_WPF.Classes
{
    class Manager : IFieldAdd
    {
        private string id { get; set; }
        private string lastName { get; set; }
        private string name { get; set; }
        private string surname { get; set; }
        public string phoneNumber { get; set; }
        public string seriesPassportNumber { get; set; }
        public Manager() : this("", "", "", "", "", "")
        {

        }
        public Manager(string id, string lastName, string name, string surname,
                       string phoneNumber, string seriesPassportNumber)
        {
            this.id = id;
            this.lastName = lastName;
            this.name = name;
            this.surname = surname;
            this.phoneNumber = phoneNumber;
            this.seriesPassportNumber = seriesPassportNumber;
        }
        public Manager GetOneClient(string id)
        {
            List<Manager> allClient = GetAllClient();

            Manager tempClient = new Manager();

            for (int i = 0; i < allClient.Count; i++)
            {
                if (allClient[i].id.Equals(id))
                {
                    tempClient = allClient[i];

                    break;
                }
            }
            return tempClient;
        }
        public List<Manager> GetAllClient()
        {
            List<Manager> allClients = new List<Manager>();

            using (StreamReader sr = File.OpenText(GetPath()))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    string[] strMassive = new string[s.Split('|').Length - 1];
                    strMassive = s.Split('|');
                    Manager cl = new Manager();
        
                    cl.id = strMassive[0];
                    cl.lastName = strMassive[1];
                    cl.name = strMassive[2];
                    cl.surname = strMassive[3];
                    cl.phoneNumber = strMassive[4];
                    cl.seriesPassportNumber = strMassive[5];

                    allClients.Add(cl);
                }
            }
            return allClients;
        }
        public void CreateOneClient(Manager newClient)
        {
            List<Manager> allClient = GetAllClient();

            newClient.id = GetCurrentID();

            allClient.Add(newClient);

            RecordInFile(allClient);
        }
        public List<Manager> DeleteClient(string id)
        {
            List<Manager> allClient = GetAllClient();

            List<Manager> tempClient = new List<Manager>();

            for (int i = 0; i < allClient.Count; i++)
            {
                if (allClient[i].id.Equals(id))
                {
                    continue;
                }
                tempClient.Add(allClient[i]);
            }
            RecordInFile(tempClient);

            return tempClient;
        }
        public string GetCurrentID()
        {
            List<Manager> allClient = GetAllClient();

            HashSet<int> idHash = new HashSet<int>();

            int idSearch = allClient.Count;

            foreach (Manager client in allClient)
            {
                idHash.Add(int.Parse(client.id));
            }
            while (true)
            {
                idSearch++;

                if (!idHash.Contains(idSearch))
                {
                    return idSearch.ToString();
                }
                continue;
            }
        }
        private void RecordInFile(List<Manager> allClient)
        {
            using (StreamWriter sw = new StreamWriter(GetPath()))
            {
                for (int i = 0; i < allClient.Count; i++)
                {
                    sw.WriteLine($"{allClient[i].id}|" +
                        $"{allClient[i].lastName}|" +
                        $"{allClient[i].name}|" +
                        $"{allClient[i].surname}|" +
                        $"{allClient[i].phoneNumber}|" +
                        $"{allClient[i].seriesPassportNumber}|" +
                        $"{DateTime.Now}|" +
                        $"{IsSignature(true)}|" +
                        $"{DataOrPhone(false)}|");
                }
            }
        }
        public void PrintAllClient()
        {
            using (StreamReader sr = File.OpenText(GetPath()))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }
        public String StringOneClient(Manager client)
        {
            return $"{client.id}|{client.lastName}|" +
                    $"{client.name}|{client.surname}|" +
                    $"{client.phoneNumber}|{client.seriesPassportNumber}|";
        }
        public void AddFakeClient(int countClients)
        {
            using (StreamWriter sw = new StreamWriter(GetPath()))

            {

                for (int i = 1; i < countClients; i++)
                {
                    var faker = new Faker();
                    Random rand = new Random();

                    sw.WriteLine($"{i}|" +
                                $"{faker.Person.LastName}|" +
                                $"{faker.Person.FirstName}|" +
                                $"{faker.Person.UserName}|" +
                                $"{faker.Phone.PhoneNumber()}|" +
                                $"{rand.Next(100_000_000, 9_000_000_00)}_{rand.Next(10000, 90000)}|");

                }
            }
        }
        public string GetPath()
        {
            return "F:\\c#Projects\\Module_10_OOP_WPF\\Classes\\DB\\database.csv";
        }
        public Manager ChangeDataClient(string id, string lastName, string name, string surname,
                                        string phoneNumber, string seriesPassportNumber)
        {
            Manager client = new Manager();

            client.id = id;
            client.lastName = lastName;
            client.name = name;
            client.surname = surname;
            client.phoneNumber = phoneNumber;
            client.seriesPassportNumber = seriesPassportNumber;

            return client;
        }
        public DateTime dateTime { get; set; }
        public string IsSignature(bool flag)
        {
            if (flag)
            {
                return "Manager|";
            }
            return "Consultant";
        }
        public string DataOrPhone(bool flag)
        {
            if (flag)
            {
                return "Phone|";
            }
            return "DataClient|";
        }

    }

}
