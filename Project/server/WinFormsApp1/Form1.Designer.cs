
namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_portNum = new System.Windows.Forms.TextBox();
            this.button_listen = new System.Windows.Forms.Button();
            this.textBox_data = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port Number:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox_portNum
            // 
            this.textBox_portNum.Location = new System.Drawing.Point(142, 32);
            this.textBox_portNum.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_portNum.Name = "textBox_portNum";
            this.textBox_portNum.Size = new System.Drawing.Size(163, 27);
            this.textBox_portNum.TabIndex = 1;
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(323, 32);
            this.button_listen.Margin = new System.Windows.Forms.Padding(2);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(174, 27);
            this.button_listen.TabIndex = 2;
            this.button_listen.Text = "Start Listener";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // textBox_data
            // 
            this.textBox_data.Location = new System.Drawing.Point(110, 72);
            this.textBox_data.Name = "textBox_data";
            this.textBox_data.Size = new System.Drawing.Size(714, 364);
            this.textBox_data.TabIndex = 3;
            this.textBox_data.Text = "";
            this.textBox_data.TextChanged += new System.EventHandler(this.textBox_data_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Data:";
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(512, 32);
            this.button_stop.Margin = new System.Windows.Forms.Padding(2);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(106, 27);
            this.button_stop.TabIndex = 5;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 445);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_data);
            this.Controls.Add(this.button_listen);
            this.Controls.Add(this.textBox_portNum);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_portNum;
        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.RichTextBox textBox_data;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_stop;
    }
}

