namespace client
{
    public partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_sweet = new System.Windows.Forms.TextBox();
            this.button_request = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.button_post = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.disconnect_button = new System.Windows.Forms.Button();
            this.follow_button = new System.Windows.Forms.Button();
            this.follower_request_button = new System.Windows.Forms.Button();
            this.following_textBox = new System.Windows.Forms.TextBox();
            this.follow = new System.Windows.Forms.Label();
            this.button_block = new System.Windows.Forms.Button();
            this.textBox_block = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_deleteSweet = new System.Windows.Forms.TextBox();
            this.button_deleteSweet = new System.Windows.Forms.Button();
            this.button_followers = new System.Windows.Forms.Button();
            this.button_followings = new System.Windows.Forms.Button();
            this.button_both = new System.Windows.Forms.Button();
            this.button_users = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username:";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(170, 50);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(313, 31);
            this.textBox_port.TabIndex = 3;
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(170, 128);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(313, 31);
            this.textBox_ip.TabIndex = 4;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(170, 212);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(313, 31);
            this.textBox_username.TabIndex = 5;
            // 
            // textBox_sweet
            // 
            this.textBox_sweet.Location = new System.Drawing.Point(590, 930);
            this.textBox_sweet.Name = "textBox_sweet";
            this.textBox_sweet.Size = new System.Drawing.Size(1044, 31);
            this.textBox_sweet.TabIndex = 6;
            // 
            // button_request
            // 
            this.button_request.Location = new System.Drawing.Point(122, 831);
            this.button_request.Name = "button_request";
            this.button_request.Size = new System.Drawing.Size(147, 61);
            this.button_request.TabIndex = 7;
            this.button_request.Text = "All Request";
            this.button_request.UseVisualStyleBackColor = true;
            this.button_request.Click += new System.EventHandler(this.button_request_Click);
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(58, 264);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(254, 61);
            this.button_connect.TabIndex = 8;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // button_post
            // 
            this.button_post.Location = new System.Drawing.Point(1641, 919);
            this.button_post.Name = "button_post";
            this.button_post.Size = new System.Drawing.Size(140, 59);
            this.button_post.TabIndex = 9;
            this.button_post.Text = "Post";
            this.button_post.UseVisualStyleBackColor = true;
            this.button_post.Click += new System.EventHandler(this.button_post_Click_1);
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(590, 45);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(1189, 845);
            this.richTextBox.TabIndex = 10;
            this.richTextBox.Text = "";
            // 
            // disconnect_button
            // 
            this.disconnect_button.Location = new System.Drawing.Point(334, 264);
            this.disconnect_button.Name = "disconnect_button";
            this.disconnect_button.Size = new System.Drawing.Size(150, 61);
            this.disconnect_button.TabIndex = 11;
            this.disconnect_button.Text = "Disconnect";
            this.disconnect_button.UseVisualStyleBackColor = true;
            this.disconnect_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // follow_button
            // 
            this.follow_button.Location = new System.Drawing.Point(58, 455);
            this.follow_button.Name = "follow_button";
            this.follow_button.Size = new System.Drawing.Size(426, 50);
            this.follow_button.TabIndex = 12;
            this.follow_button.Text = "Follow";
            this.follow_button.UseVisualStyleBackColor = true;
            this.follow_button.Click += new System.EventHandler(this.follow_button_Click);
            // 
            // follower_request_button
            // 
            this.follower_request_button.Location = new System.Drawing.Point(274, 831);
            this.follower_request_button.Name = "follower_request_button";
            this.follower_request_button.Size = new System.Drawing.Size(147, 61);
            this.follower_request_button.TabIndex = 13;
            this.follower_request_button.Text = "Request";
            this.follower_request_button.UseVisualStyleBackColor = true;
            this.follower_request_button.Click += new System.EventHandler(this.follower_request_button_Click);
            // 
            // following_textBox
            // 
            this.following_textBox.Location = new System.Drawing.Point(188, 414);
            this.following_textBox.Name = "following_textBox";
            this.following_textBox.Size = new System.Drawing.Size(295, 31);
            this.following_textBox.TabIndex = 16;
            // 
            // follow
            // 
            this.follow.AutoSize = true;
            this.follow.Location = new System.Drawing.Point(54, 419);
            this.follow.Name = "follow";
            this.follow.Size = new System.Drawing.Size(131, 25);
            this.follow.TabIndex = 15;
            this.follow.Text = "Follow User:";
            // 
            // button_block
            // 
            this.button_block.Location = new System.Drawing.Point(58, 591);
            this.button_block.Name = "button_block";
            this.button_block.Size = new System.Drawing.Size(426, 50);
            this.button_block.TabIndex = 17;
            this.button_block.Text = "Block";
            this.button_block.UseVisualStyleBackColor = true;
            this.button_block.Click += new System.EventHandler(this.button_block_Click);
            // 
            // textBox_block
            // 
            this.textBox_block.Location = new System.Drawing.Point(188, 550);
            this.textBox_block.Name = "textBox_block";
            this.textBox_block.Size = new System.Drawing.Size(295, 31);
            this.textBox_block.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 555);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 25);
            this.label4.TabIndex = 19;
            this.label4.Text = "Block User:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 691);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 25);
            this.label5.TabIndex = 20;
            this.label5.Text = "Delete Sweet:";
            // 
            // textBox_deleteSweet
            // 
            this.textBox_deleteSweet.Location = new System.Drawing.Point(188, 686);
            this.textBox_deleteSweet.Name = "textBox_deleteSweet";
            this.textBox_deleteSweet.Size = new System.Drawing.Size(295, 31);
            this.textBox_deleteSweet.TabIndex = 21;
            // 
            // button_deleteSweet
            // 
            this.button_deleteSweet.Location = new System.Drawing.Point(58, 727);
            this.button_deleteSweet.Name = "button_deleteSweet";
            this.button_deleteSweet.Size = new System.Drawing.Size(426, 50);
            this.button_deleteSweet.TabIndex = 22;
            this.button_deleteSweet.Text = "Delete Sweet";
            this.button_deleteSweet.UseVisualStyleBackColor = true;
            this.button_deleteSweet.Click += new System.EventHandler(this.button_deleteSweet_Click);
            // 
            // button_followers
            // 
            this.button_followers.Location = new System.Drawing.Point(58, 903);
            this.button_followers.Name = "button_followers";
            this.button_followers.Size = new System.Drawing.Size(147, 61);
            this.button_followers.TabIndex = 23;
            this.button_followers.Text = "Followers";
            this.button_followers.UseVisualStyleBackColor = true;
            this.button_followers.Click += new System.EventHandler(this.button_followers_Click);
            // 
            // button_followings
            // 
            this.button_followings.Location = new System.Drawing.Point(214, 903);
            this.button_followings.Name = "button_followings";
            this.button_followings.Size = new System.Drawing.Size(147, 61);
            this.button_followings.TabIndex = 24;
            this.button_followings.Text = "Followings";
            this.button_followings.UseVisualStyleBackColor = true;
            this.button_followings.Click += new System.EventHandler(this.button_followings_Click);
            // 
            // button_both
            // 
            this.button_both.Location = new System.Drawing.Point(368, 903);
            this.button_both.Name = "button_both";
            this.button_both.Size = new System.Drawing.Size(147, 61);
            this.button_both.TabIndex = 25;
            this.button_both.Text = "Both";
            this.button_both.UseVisualStyleBackColor = true;
            this.button_both.Click += new System.EventHandler(this.button_both_Click);
            // 
            // button_users
            // 
            this.button_users.Location = new System.Drawing.Point(58, 342);
            this.button_users.Name = "button_users";
            this.button_users.Size = new System.Drawing.Size(426, 50);
            this.button_users.TabIndex = 26;
            this.button_users.Text = "Users";
            this.button_users.UseVisualStyleBackColor = true;
            this.button_users.Click += new System.EventHandler(this.button_users_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1852, 997);
            this.Controls.Add(this.button_users);
            this.Controls.Add(this.button_both);
            this.Controls.Add(this.button_followings);
            this.Controls.Add(this.button_followers);
            this.Controls.Add(this.button_deleteSweet);
            this.Controls.Add(this.textBox_deleteSweet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_block);
            this.Controls.Add(this.button_block);
            this.Controls.Add(this.follow);
            this.Controls.Add(this.following_textBox);
            this.Controls.Add(this.follower_request_button);
            this.Controls.Add(this.follow_button);
            this.Controls.Add(this.disconnect_button);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.button_post);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.button_request);
            this.Controls.Add(this.textBox_sweet);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_sweet;
        private System.Windows.Forms.Button button_request;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Button button_post;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button disconnect_button;
        private System.Windows.Forms.Button follow_button;
        private System.Windows.Forms.Button follower_request_button;
        private System.Windows.Forms.TextBox following_textBox;
        private System.Windows.Forms.Label follow;
        private System.Windows.Forms.Button button_block;
        private System.Windows.Forms.TextBox textBox_block;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_deleteSweet;
        private System.Windows.Forms.Button button_deleteSweet;
        private System.Windows.Forms.Button button_followers;
        private System.Windows.Forms.Button button_followings;
        private System.Windows.Forms.Button button_both;
        private System.Windows.Forms.Button button_users;
    }
}

