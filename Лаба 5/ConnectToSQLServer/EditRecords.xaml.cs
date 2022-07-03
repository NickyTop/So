using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace ConnectToSQLServer
{
    /// <summary>
    /// Логика взаимодействия для EditRecords.xaml
    /// </summary>
    public partial class EditRecords : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public EditRecords()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GetRecordsData();
        }
        private void GetAndDhowData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            connection.Close();
        }

        private void GetRecordsData()
        {
            string sqlQ = "SELECT * FROM Records;";
            try
            {
                GetAndDhowData(sqlQ, RecordsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddRecordsData()
        {
            string sqlQ = "INSERT INTO Records (IDRecords, Name, Performer, Quantity, Manufacturer)" +
               " VALUES(" + AddID.Text + ", '" + AddName.Text + "', '" + AddPerformer.Text + "', " + AddQuantity.Text + ", '" + AddManufac.Text + "'); ";
            try
            {
                GetAndDhowData(sqlQ, RecordsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DelRecordsData()
        {
            string sqlQ = "DELETE From Records " +
                "Where Name = '" + DelRecords.Text + "'; ";
            try
            {
                GetAndDhowData(sqlQ, RecordsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddRecordsData();
            GetRecordsData();
        }

        private void DelRecords_Click(object sender, RoutedEventArgs e)
        {
            DelRecordsData();
            GetRecordsData();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Records mw;
            mw = new Records();
            Hide();
            mw.Show();
        }

    }
}
