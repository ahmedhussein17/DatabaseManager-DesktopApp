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

        private void Form1_Load(object sender, EventArgs e)
        {
            var connections = ConfigService.LoadConnections();
            foreach (var c in connections)
            {
                cmbEnvironment.Items.Add(c.Name);
            }
            if (cmbEnvironment.Items.Count > 0)
                cmbEnvironment.SelectedIndex = 0;
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

        private void btnLoadObjects_Click(object sender, EventArgs e)
        {
            lstObjects.Items.Clear();

            if (cmbEnvironment.SelectedItem == null)
            {
                MessageBox.Show("Pick an environment first.");
                return;
            }

            string selectedEnv = cmbEnvironment.SelectedItem.ToString();
            var connections = ConfigService.LoadConnections();
            var connInfo = connections.First(c => c.Name == selectedEnv);

            try
            {
                using (var conn = new SqlConnection(connInfo.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT name, type_desc
                        FROM sys.objects
                        WHERE type IN ('U', 'V', 'P')
                        ORDER BY type_desc, name";

                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            string type = reader.GetString(1);
                            lstObjects.Items.Add($"[{type}] {name}");
                        }
                    }
                }

                Logger.Log("LoadObjects", selectedEnv, "-", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load objects: " + ex.Message);
                Logger.Log("LoadObjects", selectedEnv, "-", "Failed: " + ex.Message);
            }
        }
    }
}