using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Controls;

namespace ConnectToSQLServer
{
    public partial class MainWindow : Window
    {
        string connectionString = null;        
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);
            //GetPerformersData();
            //GetMusicansData();
            //GetTradeData();
            //GetRecordsData();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            //this.Close();
        }

        private void Records_Click(object sender, RoutedEventArgs e)
        {
            Records mw;
            mw = new Records();
            Hide();
            mw.Show();
        }

        private void Performers_Click(object sender, RoutedEventArgs e)
        {
            Performers mw;
            mw = new Performers();
            Hide();
            mw.Show();
        }

        private void Trade_Click(object sender, RoutedEventArgs e)
        {
            Trade mw;
            mw = new Trade();
            Hide();
            mw.Show();
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


       /*private void GetTradeData()
        {
            string sqlQ = "";
            try
            {
                GetAndDhowData(sqlQ, TradeDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }*/

    }
}