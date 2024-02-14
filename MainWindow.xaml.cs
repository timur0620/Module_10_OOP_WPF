using Module_10_OOP_WPF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module_10_OOP_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }
        private void btnAllClients_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();

            List<Manager> allClient = manager.GetAllClient();

            foreach (Manager client in allClient)
            {
                listClients.Items.Add(manager.StringOneClient(client));
            }
        }
        private void btnFindClient_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();

            string id = idSearchField.Text;

            manager = manager.GetOneClient(id);

            string[] client = manager.StringOneClient(manager).Split('|');

            lastNameField.Text = client[1];
            nameField.Text = client[2];
            surnameField.Text = client[3];
            phoneNumberField.Text = client[4];
            serialPasportField.Text = client[5];
        }
        private void updateClient_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();

            manager = manager.ChangeDataClient( idSearchField.Text,
                                                lastNameField.Text,
                                                nameField.Text,
                                                surnameField.Text,
                                                phoneNumberField.Text,
                                                serialPasportField.Text
                                                );

            manager.DeleteClient(idSearchField.Text);

            string temp = manager.StringOneClient(manager);

            listClients.Items.Clear();

            listClients.Items.Add( temp );

            manager.CreateOneClient(manager);
        }
        private void createClient_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();

            manager = manager.ChangeDataClient(idSearchField.Text,
                                                lastNameField.Text,
                                                nameField.Text,
                                                surnameField.Text,
                                                phoneNumberField.Text,
                                                serialPasportField.Text
                                                );
            manager.CreateOneClient(manager);

            string temp = manager.StringOneClient(manager);

            listClients.Items.Clear();

            listClients.Items.Add(temp);
        }
        private void deleteClient_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();

            manager.DeleteClient(idSearchField.Text) ;

            List<Manager> allClient = manager.GetAllClient();

            listClients.Items.Clear();

            foreach (Manager client in allClient)
            {
                listClients.Items.Add(manager.StringOneClient(client));
            }

        }
        private void btnAllClientConsultant_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();

            List<Manager> allClient = manager.GetAllClient();

            foreach (Manager client in allClient)
            {
                client.seriesPassportNumber = "*********";

                listClientConsultant.Items.Add(manager.StringOneClient(client));
            }
        }
        private void btnFindClientIdConsultant_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();

            string id = idSearchCons.Text;

            manager = manager.GetOneClient(id);

            string[] client = manager.StringOneClient(manager).Split('|');

            txtLastNameCons.Text = client[1];
            txtNameCons.Text = client[2];
            txtSurnameCons.Text = client[3];
            phoneNumberFildCons.Text = client[4];
            txtSerialPass.Text = "************";
        }
        private void btnUpdatePhone_Click(object sender, RoutedEventArgs e)
        {
            Consultant consultant = new Consultant();

            Manager manager = new Manager();

            string id = idSearchCons.Text;

            manager = manager.GetOneClient(id);

            string oldPhone = manager.phoneNumber;
            string newPhone = phoneNumberFildCons.Text;
            manager = consultant.ChangeNumberPhone(oldPhone, newPhone);

            string temp = manager.StringOneClient(manager);

            listClientConsultant.Items.Clear();

            listClientConsultant.Items.Add(temp);

            manager.CreateOneClient(manager);
        }
    }
}
