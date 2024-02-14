using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10_OOP_WPF.Classes
{
    class Consultant : Manager, IFieldAdd
    {   
        public new DateTime dateTime { get; set; }
        public new string IsSignature(bool flag)
        {   
            if (flag)
            {
                return "Manager|";
            }
            return "Consultant";
        }
        public new string DataOrPhone(bool flag)
        {   if (flag)
            {
                return "Phone|";
            }
            return "DataClient|";
        }
  
        public Consultant(string id, string lastName, string name, string surname,
                          string phoneNumber, string seriesPassportNumber) :
                          base(id, lastName, name, surname, phoneNumber, seriesPassportNumber)
        {

        }
        public Consultant() : this("", "", "", "", "","")
        {

        }
        public Manager ChangeNumberPhone(string oldNumber, string newNumber)
        {
            List<Manager> allClients = GetAllClient();

            Manager consultant = new Manager();

            if (newNumber == null || newNumber.Length == 0 || newNumber.Equals(""))
            {
                return consultant;
            }
            foreach (Manager client in allClients)
            {
                if (client.phoneNumber.Equals(oldNumber))
                {
                    client.phoneNumber = newNumber;

                    consultant = client;

                    break;
                }
            }
            return consultant;
        }  
    }
    interface IFieldAdd
    {
        DateTime dateTime { get; set; }
        string IsSignature(bool flag);
        string DataOrPhone(bool flag);
    }
}
