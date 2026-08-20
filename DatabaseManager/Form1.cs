using Microsoft.Data.SqlClient;
using DatabaseManager.Services;

namespace DatabaseManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                var connections = ConfigService.LoadConnections();
                var devConnection = connections.First(c => c.Name == "Dev");

                using (var conn = new SqlConnection(devConnection.ConnectionString))
                {
                    conn.Open();
                    lblStatus.Text = "Connected to " + devConnection.Name;
                    lblStatus.ForeColor = Color.Green;
                    Logger.Log("TestConnection", devConnection.Name, "-", "Success");
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Failed: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
                Logger.Log("TestConnection", "Dev", "-", "Failed: " + ex.Message);
            }
        }
    }
}