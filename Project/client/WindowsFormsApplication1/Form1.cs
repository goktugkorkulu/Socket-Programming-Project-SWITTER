using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace client
{


    public partial class Form1 : Form
    {


        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        // Creates class for user information to send to Server
        class toSend
        {
            public string username;
            public string sweet;
            public string id;
            public string timestap;

        }
        toSend send1 = new toSend();

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // creating client
            string IP = textBox_ip.Text; // getting IP address as input

            int portNum; // 
            if (Int32.TryParse(textBox_port.Text, out portNum))
            {

                try
                {

                    //Connects to server, connect button disabled, sweet textbox and post button enabled
                    clientSocket.Connect(IP, portNum); // trying to connect to the server

                    button_connect.Enabled = false;
                    textBox_sweet.Enabled = true;
                    button_post.Enabled = true;
                    connected = true;
                    // richTextBox.AppendText("Connected to the server!\n");


                    string message_username = "CONNECT*" + textBox_username.Text;
                    Byte[] buffer = Encoding.Default.GetBytes(message_username);
                    clientSocket.Send(buffer);//Sends username to server with information of "connect"

                    Byte[] bufferA = new Byte[64];
                    clientSocket.Receive(bufferA);
                    string incomingMessage = Encoding.Default.GetString(bufferA);//ID received by Server
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (incomingMessage == "User not in database!\n") // if the client is not in the database of the server exit
                    {
                        connected = false;
                        terminating = true;
                        Environment.Exit(0);
                    }
                    else if (incomingMessage == "User is online!\n") // if user is already online and other user tries to get in with the same username
                    {
                        connected = false;
                        terminating = true;  // terminate 
                        Environment.Exit(0);
                    }
                    // richTextBox.AppendText(send1.id);
                    else
                    {
                        //adds client id, username to class object

                        send1.id = incomingMessage;
                        send1.username = textBox_username.Text; // making send1 username as the input from username textbox
                        //richTextBox.AppendText(incomingMessage);
                        richTextBox.AppendText("Connected to the server!\n");
                    }



                }
                catch
                {
                    richTextBox.AppendText("Could not connect to the server!\n"); // if connection is unsuccessful
                }

            }
            else
            {
                richTextBox.AppendText("Check the port\n");
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string msga = "EXIT*" + textBox_username.Text; // creating a message to send to the server as EXIT
            Byte[] bufferE = Encoding.Default.GetBytes(msga);
            clientSocket.Send(bufferE);//Sends username to server
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_request_Click(object sender, EventArgs e)
        {
            string message_request = "REQUEST*" + send1.username; // send request message to the server

            Byte[] buffer = Encoding.Default.GetBytes(message_request);
            clientSocket.Send(buffer);


            Byte[] bufferR = new Byte[9999];
            clientSocket.Receive(bufferR); // receiving the sweets from server that includes the other users but not the current user 
            string incomingMessage2 = Encoding.Default.GetString(bufferR);

            richTextBox.AppendText(incomingMessage2);


        }



        private void button_post_Click_1(object sender, EventArgs e)
        {
            //gets timestamp
            DateTime now = DateTime.Now;
            string timestamp = now.ToString();

            //adds remaining attributes to class object
            send1.timestap = timestamp;
            send1.sweet = textBox_sweet.Text;

            string messagetoServer = send1.username + "*" + send1.id + "*" + send1.timestap + "*" + send1.sweet;
            //richTextBox.AppendText("Sent: " + send1.username + " ," + send1.id + " at " + send1.timestap + " , " + send1.sweet + "\n");

            //sendsstring that has clinet username, id, timestap, sweet to Server
            if (messagetoServer != "" && messagetoServer.Length <= 100)
            {
                Byte[] buffer = Encoding.Default.GetBytes(messagetoServer);
                clientSocket.Send(buffer); // sending the sweet to the serve with necessary informations 
            }
            Byte[] bufferRR = new Byte[9999];
            clientSocket.Receive(bufferRR);
            string following_names = Encoding.Default.GetString(bufferRR);

            richTextBox.AppendText(following_names);
        }

        private void button1_Click(object sender, EventArgs e) //disconnect
        {
            string msg_ = "DISCONNECT*" + textBox_username.Text;
            if (msg_ != "")
            {
                Byte[] bufferT = Encoding.Default.GetBytes(msg_);
                clientSocket.Send(bufferT);
            }
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }



        private void follower_request_button_Click(object sender, EventArgs e) // request the ones that you follow
        {
            string username_ = textBox_username.Text;
            string msg = "FREQUEST*" + username_;
            if (msg != "")
            {
                Byte[] bufferX = Encoding.Default.GetBytes(msg);
                clientSocket.Send(bufferX);
            }

            Byte[] bufferT = new Byte[9999];
            clientSocket.Receive(bufferT);
            string following_names = Encoding.Default.GetString(bufferT);
            richTextBox.AppendText(following_names);
        }


        private void follow_button_Click(object sender, EventArgs e)
        {
            string user_name = textBox_username.Text;
            string tobefollowed_username = following_textBox.Text;
            string messag = "FOLLOW*" + user_name + "*" + tobefollowed_username;
            if (messag != "" && messag.Length <= 64)
            {
                Byte[] bufferB = Encoding.Default.GetBytes(messag);
                clientSocket.Send(bufferB);
            }
            Byte[] bufferFollow = new Byte[9999];
            clientSocket.Receive(bufferFollow);
            string following_names = Encoding.Default.GetString(bufferFollow);
            richTextBox.AppendText(following_names);

        }

        private void button_block_Click(object sender, EventArgs e)
        {

            //sending the username wishing to be blocked to the server
            string my_username = textBox_username.Text;
            string toBeBlocked_username = textBox_block.Text;
            string block_message = "BLOCK*" + my_username + "*" + toBeBlocked_username;
            if (block_message != "" && block_message.Length <= 64)
            {
                Byte[] bufferBlock = Encoding.Default.GetBytes(block_message);
                clientSocket.Send(bufferBlock);
            }

            //receiving server's action on the user wished to be blocked
            Byte[] bufferBlockReceive = new Byte[9999];
            clientSocket.Receive(bufferBlockReceive);
            string blocking_action_result = Encoding.Default.GetString(bufferBlockReceive);

            //printing out the action result
            richTextBox.AppendText(blocking_action_result);
        }

        private void button_deleteSweet_Click(object sender, EventArgs e)
        {
            //sending the sweetId of sweet wished to be deleted
            string my_username = textBox_username.Text;
            string toBeDeleted_sweetid = textBox_deleteSweet.Text;
            string dsweet_message = "DELSWEET*" + my_username + "*" + toBeDeleted_sweetid;
            if (dsweet_message != "" && dsweet_message.Length <= 64)
            {
                Byte[] buffer_dsweet = Encoding.Default.GetBytes(dsweet_message);
                clientSocket.Send(buffer_dsweet);
            }

            //receiving server's action on the user wished to be blocked
            Byte[] bufferDSweetReceive = new Byte[9999];
            clientSocket.Receive(bufferDSweetReceive);
            string DWeet_action_result = Encoding.Default.GetString(bufferDSweetReceive);

            //printing out the action result
            richTextBox.AppendText(DWeet_action_result);
        }

        private void button_followers_Click(object sender, EventArgs e)
        {
            string my_username = textBox_username.Text;
            string display_followers_message = "DISPLAYFOLLOWERS*" + my_username;
            if (display_followers_message != "" && display_followers_message.Length <= 64)
            {
                Byte[] buffer_dfollowers = Encoding.Default.GetBytes(display_followers_message);
                clientSocket.Send(buffer_dfollowers);
            }

            //receiving server's action on the demand of displaying the followers
            Byte[] bufferDisplayFollowersBuffer = new Byte[9999];
            clientSocket.Receive(bufferDisplayFollowersBuffer);
            string DisplayFollowers_action_result = Encoding.Default.GetString(bufferDisplayFollowersBuffer);

            //printing out the action result
            richTextBox.AppendText(DisplayFollowers_action_result);
        }

        private void button_followings_Click(object sender, EventArgs e)
        {
            string my_username = textBox_username.Text;
            string display_followings_message = "DISPLAYFOLLOWINGS*" + my_username;
            if (display_followings_message != "" && display_followings_message.Length <= 64)
            {
                Byte[] buffer_dfollowings = Encoding.Default.GetBytes(display_followings_message);
                clientSocket.Send(buffer_dfollowings);
            }

            //receiving server's action on the demand of displaying the followers
            Byte[] bufferDisplayFollowingsBuffer = new Byte[9999];
            clientSocket.Receive(bufferDisplayFollowingsBuffer);
            string DisplayFollowings_action_result = Encoding.Default.GetString(bufferDisplayFollowingsBuffer);

            //printing out the action result
            richTextBox.AppendText(DisplayFollowings_action_result);
        }

        private void button_both_Click(object sender, EventArgs e)
        {
            string my_username = textBox_username.Text;
            string display_BOTH_message = "FFREQUEST*" + my_username;
            if (display_BOTH_message != "" && display_BOTH_message.Length <= 64)
            {
                Byte[] send_buffer_displayBoth = Encoding.Default.GetBytes(display_BOTH_message);
                clientSocket.Send(send_buffer_displayBoth);
            }

            //receiving server's action on the demand of displaying the followers
            Byte[] receive_bufferDisplayBOTH = new Byte[9999];
            clientSocket.Receive(receive_bufferDisplayBOTH);
            string DisplayBOTH_action_result = Encoding.Default.GetString(receive_bufferDisplayBOTH);

            //printing out the action result
            richTextBox.AppendText(DisplayBOTH_action_result);
        }

        private void button_users_Click(object sender, EventArgs e)
        {

            string my_username = textBox_username.Text;
            string display_users_message = "USERS*"+ my_username ;
            if (display_users_message != "" && display_users_message.Length <= 64)
            {
                Byte[] buffer_dusers = Encoding.Default.GetBytes(display_users_message);
                clientSocket.Send(buffer_dusers);
            }

            //receiving server's action on the demand of displaying the USERS
            Byte[] bufferDisplayFollowingsBuffer = new Byte[9999];
            clientSocket.Receive(bufferDisplayFollowingsBuffer);
            string DisplayUsers_action_result = Encoding.Default.GetString(bufferDisplayFollowingsBuffer);

            //printing out the action result
            richTextBox.AppendText(DisplayUsers_action_result);

        }
    }
}
