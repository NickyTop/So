using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace ConnectToSQLServer
{
    /// <summary>
    /// Логика взаимодействия для Trade.xaml
    /// </summary>
    public partial class Trade : Window
    {

        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public Trade()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GetTradeData();
            GetTopSalesData();
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

        private void GetTopSalesData()
        {
            string sqlQ = "SELECT TOP(20) Name as [Назва], ThisYearSales as [Продажів за рік] " +
                "From Records R FULL JOIN Trade T " +
                "ON R.IDRecords = T.IDRecords ORDER BY ThisYearSales DESC";
            try
            {
                GetAndDhowData(sqlQ, TopSalesDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetTradeData()
        {
            string sqlQ = "SELECT * FROM Trade;";
            try
            {
                GetAndDhowData(sqlQ, TradeDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UpdateSallingPriceData()
        {
            string sqlQ = "UPDATE Trade " +
                "SET SallingPrice = " + SalPrice.Text + " " +
                "WHERE IDRecords = " + ID.Text + "; ";
            try
            {
                GetAndDhowData(sqlQ, TradeDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UpdatePurchasePriceData()
        {
            string sqlQ = "UPDATE Trade " +
                "SET PurchasePrice = " + PurPrice.Text + " " +
                "WHERE IDRecords = " + ID.Text + "; ";
            try
            {
                GetAndDhowData(sqlQ, TradeDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UpdateThisYearSalesData()
        {
            string sqlQ = "UPDATE Trade " +
                "SET ThisYearSales = " + ThisSales.Text + " " +
                "WHERE IDRecords = " + ID.Text + "; ";
            try
            {
                GetAndDhowData(sqlQ, TradeDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UpdateLastYearSalesData() 
        {
            string sqlQ = "UPDATE Trade " +
                "SET LastYearSales = " + LastSales.Text + " " +
                "WHERE IDRecords = " + ID.Text + "; ";
            try
            {
                GetAndDhowData(sqlQ, TradeDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UpdateAdressData()
        {
            string sqlQ = "UPDATE Trade " +
                "SET SupplierAdress = '" + Adress.Text + "' " +
                "WHERE IDRecords = " + ID.Text + "; ";
            try
            {
                GetAndDhowData(sqlQ, TradeDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SalPriceUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateSallingPriceData();
            GetTradeData();
        }

        private void PurPriceUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdatePurchasePriceData();
            GetTradeData();
        }

        private void ThisYearSalesUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateThisYearSalesData();
            GetTradeData();
            GetTopSalesData();
        }

        private void LastYearSalesUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateLastYearSalesData();
            GetTradeData();
        }

        private void AdressUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateAdressData();
            GetTradeData();
        }

        private void GoToMainWin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw;
            mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
