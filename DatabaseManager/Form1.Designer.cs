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

        private void InitializeComponent()
        {
            this.btnTestConnection = new Button();
            this.lblStatus = new Label();
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
            this.lblStatus.Location = new Point(30, 90);
            this.lblStatus.Size = new Size(400, 30);
            this.lblStatus.Font = new Font("Segoe UI", 10F);

            // Form1
            this.ClientSize = new Size(480, 200);
            this.Text = "Database Manager";
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.lblStatus);
            this.ResumeLayout(false);
        }
    }
}