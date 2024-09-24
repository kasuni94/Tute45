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
using System.ServiceModel;
using BusinessTier;
using DataTier;

namespace GUIClient
{
    public partial class MainWindow : Window
    {
        private BusinessServerInterface businessServer;

        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<BusinessServerInterface> factory = new ChannelFactory<BusinessServerInterface>(
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:8200/BusinessService")
            );

            businessServer = factory.CreateChannel();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DisableUI();
            try
            {
                if (string.IsNullOrWhiteSpace(searchTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid last name.");
                    return;
                }

                string lastName = searchTextBox.Text;
                Record result = await Task.Run(() => businessServer.SearchRecordByLastName(lastName));

                if (result != null)
                {
                    idTextBox.Text = result.Id.ToString();
                    firstNameTextBox.Text = result.FirstName;
                    lastNameTextBox.Text = result.LastName;
                    balanceTextBox.Text = result.Balance.ToString();
                }
                else
                {
                    MessageBox.Show("No matching record found.");
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The search operation timed out.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                EnableUI();
            }
        }

        private void DisableUI()
        {
            searchButton.IsEnabled = false;
            progressBar.IsIndeterminate = true;
        }

        private void EnableUI()
        {
            searchButton.IsEnabled = true;
            progressBar.IsIndeterminate = false;
        }
    }
}
