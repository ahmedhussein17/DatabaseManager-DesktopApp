namespace DatabaseManager
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Button btnTestConnection;
        private Label lblStatus;
        private ComboBox cmbEnvironment;
        private Button btnLoadObjects;
        private ListBox lstObjects;

        private void InitializeComponent()
        {
            this.btnTestConnection = new Button();
            this.lblStatus = new Label();
            this.cmbEnvironment = new ComboBox();
            this.btnLoadObjects = new Button();
            this.lstObjects = new ListBox();
            this.SuspendLayout();

            // btnTestConnection
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.Location = new Point(30, 30);
            this.btnTestConnection.Size = new Size(160, 40);
            this.btnTestConnection.Click += new EventHandler(this.btnTestConnection_Click);

            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Not connected";
            this.lblStatus.Location = new Point(200, 40);
            this.lblStatus.Size = new Size(300, 30);
            this.lblStatus.Font = new Font("Segoe UI", 10F);

            // cmbEnvironment
            this.cmbEnvironment.Name = "cmbEnvironment";
            this.cmbEnvironment.Location = new Point(30, 90);
            this.cmbEnvironment.Size = new Size(160, 30);
            this.cmbEnvironment.DropDownStyle = ComboBoxStyle.DropDownList;

            // btnLoadObjects
            this.btnLoadObjects.Name = "btnLoadObjects";
            this.btnLoadObjects.Text = "Load Tables/Views/Procs";
            this.btnLoadObjects.Location = new Point(200, 90);
            this.btnLoadObjects.Size = new Size(180, 30);
            this.btnLoadObjects.Click += new EventHandler(this.btnLoadObjects_Click);

            // lstObjects
            this.lstObjects.Name = "lstObjects";
            this.lstObjects.Location = new Point(30, 140);
            this.lstObjects.Size = new Size(500, 250);

            // Form1
            this.ClientSize = new Size(560, 420);
            this.Text = "Database Manager";
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbEnvironment);
            this.Controls.Add(this.btnLoadObjects);
            this.Controls.Add(this.lstObjects);
            this.Load += new EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }
    }
}