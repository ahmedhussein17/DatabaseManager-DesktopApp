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
            cmbTable.Items.Clear();

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
                var objects = SchemaService.GetDatabaseObjects(connInfo.ConnectionString);

                foreach (var obj in objects)
                {
                    lstObjects.Items.Add($"[{obj.Type}] {obj.Name}");

                    if (obj.Type == "USER_TABLE")
                        cmbTable.Items.Add(obj.Name);
                }

                if (cmbTable.Items.Count > 0)
                    cmbTable.SelectedIndex = 0;

                Logger.Log("LoadObjects", selectedEnv, "-", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load objects: " + ex.Message);
                Logger.Log("LoadObjects", selectedEnv, "-", "Failed: " + ex.Message);
            }
        }

        private void btnBackupTable_Click(object sender, EventArgs e)
        {
            if (cmbEnvironment.SelectedItem == null || cmbTable.SelectedItem == null)
            {
                MessageBox.Show("Pick an environment and a table first.");
                return;
            }

            string selectedEnv = cmbEnvironment.SelectedItem.ToString();
            string tableName = cmbTable.SelectedItem.ToString();

            var connections = ConfigService.LoadConnections();
            var connInfo = connections.First(c => c.Name == selectedEnv);

            try
            {
                var result = BackupService.BackupTable(connInfo.ConnectionString, tableName);

                lblBackupStatus.Text = result.Message;
                lblBackupStatus.ForeColor = result.Success ? Color.Green : Color.Orange;

                Logger.Log("BackupTable", selectedEnv, result.BackupName, result.Success ? "Success" : "Skipped - already exists");
            }
            catch (Exception ex)
            {
                lblBackupStatus.Text = "Backup failed: " + ex.Message;
                lblBackupStatus.ForeColor = Color.Red;
                Logger.Log("BackupTable", selectedEnv, tableName, "Failed: " + ex.Message);
            }
        }
    }
}