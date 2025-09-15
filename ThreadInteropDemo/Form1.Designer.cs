namespace ThreadInteropDemo
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.statusTextLabel = new System.Windows.Forms.Label();
            this.statusValueLabel = new System.Windows.Forms.Label();
            this.countTextLabel = new System.Windows.Forms.Label();
            this.countValueLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.abortButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // descriptionLabel
            this.descriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel.Location = new System.Drawing.Point(12, 9);
            this.descriptionLabel.Size = new System.Drawing.Size(360, 60);
            this.descriptionLabel.Text = "This program demonstrates a Windows Forms app in C# that controls a\n" +
                "native C++ worker thread. The thread increments a counter every second,\n" +
                "and you can Start, Stop, or Abort it. Status and count are shown below.";
            this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // statusTextLabel
            this.statusTextLabel.AutoSize = true;   // 👈 add this
            this.statusTextLabel.Location = new System.Drawing.Point(20, 90);
            this.statusTextLabel.Text = "Status:";

            // statusValueLabel
            this.statusValueLabel.AutoSize = true;
            this.statusValueLabel.Location = new System.Drawing.Point(120, 90);  // 👈 move a bit right
            this.statusValueLabel.Text = "Not Running";

            // countTextLabel
            this.countTextLabel.AutoSize = true;    // 👈 add this
            this.countTextLabel.Location = new System.Drawing.Point(20, 115);
            this.countTextLabel.Text = "Count:";

            // countValueLabel
            this.countValueLabel.AutoSize = true;
            this.countValueLabel.Location = new System.Drawing.Point(120, 115); // 👈 move a bit right
            this.countValueLabel.Text = "-1";

            // startButton
            this.startButton.Location = new System.Drawing.Point(90, 160);
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.Text = "Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);

            // stopButton
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(171, 160);
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);

            // abortButton
            this.abortButton.Enabled = false;
            this.abortButton.Location = new System.Drawing.Point(252, 160);
            this.abortButton.Size = new System.Drawing.Size(75, 23);
            this.abortButton.Text = "Abort";
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.statusTextLabel);
            this.Controls.Add(this.statusValueLabel);
            this.Controls.Add(this.countTextLabel);
            this.Controls.Add(this.countValueLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.abortButton);
            this.Name = "Form1";
            this.Text = "ITC Test Program";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label statusTextLabel;
        private System.Windows.Forms.Label statusValueLabel;
        private System.Windows.Forms.Label countTextLabel;
        private System.Windows.Forms.Label countValueLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button abortButton;
    }
}