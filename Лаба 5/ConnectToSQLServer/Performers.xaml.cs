using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConnectToSQLServer
{
    /// <summary>
    /// Логика взаимодействия для Performers.xaml
    /// </summary>
    public partial class Performers : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable dT1;
        SqlDataAdapter Data;
        

        public Performers()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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

        private void GetPerformersRecordsData()
        {
            string sqlQ = "SELECT Performer, Name From Records WHERE Performer = '" + InputGroup.Text + "'; ";
            try
            {
                GetAndDhowData(sqlQ, PerformersRecordsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetPerformersMusicansData()
        {
            string sqlQ = "SELECT P.IDPerformer, PerfName, MusName " +
                "From Performers P FULL JOIN MusicansInGroups MG " +
                "ON P.IDPerformer = MG.IDPerformer FULL JOIN Musicans " +
                "ON MG.IDMusicans = Musicans.IDMusicans " +
                "WHERE PerfName = '" + InputGroup.Text + "'; ";
            try
            {
                GetAndDhowData(sqlQ, PerformersMusicansDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddMusicansData()
        {
            string sqlQ = "INSERT INTO Musicans (MusName) VALUES ('" + Musicans.Text + "') ";
                
            String IDID;
            Data = new SqlDataAdapter("SELECT TOP (1) [IDMusicans]  FROM [Shop].[dbo].[Musicans]  order BY IDMusicans DESC", sqlQ);
            dT1 = new DataTable("MusPerData");
            IDID = dT1.Rows[0][0].ToString();

            try
            {
                GetAndDhowData(sqlQ, PerformersRecordsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            string sqlQ2 = "INSERT INTO MusicansInGroups (IDMusicans, IDPerformer) VALUES(" + IDID + ", " + ID.Text + ");";
            try
            {
                GetAndDhowData(sqlQ2, PerformersRecordsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DeleteMusicansData()
        {
            string sqlQ = "DELETE FROM Musicans WHERE MusName = '" + Musicans.Text + "'; ";
            try
            {
                GetAndDhowData(sqlQ, PerformersRecordsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void SerchPerformers_Click(object sender, RoutedEventArgs e)
        {
            GetPerformersRecordsData();
            GetPerformersMusicansData();
        }

        private void GoToMainWin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw;
            mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void AddSkladButton_Click(object sender, RoutedEventArgs e)
        {
            AddMusicansData();
            GetPerformersRecordsData(); 
            GetPerformersMusicansData();
        }

        private void DelSkladButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteMusicansData();
            GetPerformersRecordsData();
            GetPerformersMusicansData();
        }
    }
}
