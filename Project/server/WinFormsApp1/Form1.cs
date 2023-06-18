using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

//COMPILED IN VS2019

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<string> currusers = new List<string>();
        List<List<string>> sweets = new List<List<string>>();
        List<int> ids = new List<int>();
        string userName = "";
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        bool terminating = false;
        bool listening = false;
        List<string> databaseNames = new List<string>();
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_listen_Click(object sender, EventArgs e) //when we press listen button
        {
            string fileName = "user-db.txt";
            using (var reader = new StreamReader(fileName)) //reading file and putting all db names in list
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    databaseNames.Add(line);
                }
            }

            int serverPort;

            if (Int32.TryParse(textBox_portNum.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;
                button_stop.Enabled = true;
                textBox_data.Enabled = true;
                textBox_data.AppendText("Started listening on port: " + serverPort + "\n");

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();        
            }
            else
            {
                textBox_data.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while (listening) //always listening until stop or exit
            {
                try
                {
                    Socket newClient = serverSocket.Accept();     
                    Thread receiveThread = new Thread(() => Receive(newClient)); //thread for client
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        textBox_data.AppendText("The socket stopped working.\n");
                    }
                }   
            }
        }

        private void Receive(Socket thisClient) //when client recieved
        {
            bool connected = true;
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Trim('\0');
                    textBox_data.AppendText("Client: " + incomingMessage + "\n"); //client sends msg to server
                    if (incomingMessage.Substring(0,7) == "CONNECT") //if client is trying to connect
                    {
                        List<string> i = incomingMessage.Split("*").ToList();
                        userName = i[1];
                        bool check = false;
                        foreach (string name in databaseNames) //check if client name in db
                        {
                            if (userName == name)
                            {
                                check = true;
                                break;
                            } 
                        }
                        if(!check)
                        {
                            string message = "User not in database!\n";
                            buffer = Encoding.Default.GetBytes(message);
                            thisClient.Send(buffer); 
                            thisClient.Close(); 
                            textBox_data.AppendText("The user " + userName + " was not in the database!\n");
                        }
                        else
                        {//next check is user online
                            check = false;
                            foreach(string u in currusers)
                            {
                                if(u == userName)
                                {
                                    check = true;
                                    break;
                                }
                            }
                            if (check)
                            {
                                string message = "User is online!\n";
                                buffer = Encoding.Default.GetBytes(message);
                                thisClient.Send(buffer);
                                thisClient.Close(); 
                                textBox_data.AppendText("The user " + userName + " is already logged in!\n");
                            }
                            else
                            {
                                clientSockets.Add(thisClient);
                                string msgID = GetRandomNumber().ToString(); //user ID
                                buffer = Encoding.Default.GetBytes(msgID);
                                thisClient.Send(buffer);         
                                textBox_data.AppendText("The user " + userName + " is connected! With ID: " + msgID +"\n");
                                currusers.Add(userName);
                            }                    
                        }   
                    }
                    else if(incomingMessage.Substring(0,4) == "EXIT") //if client closed server
                    {
                        userName = incomingMessage.Substring(5);
                        currusers.Remove(userName);
                        textBox_data.AppendText(userName + " exited!\n");
                        thisClient.Close(); //also close so no infinite client:
                    }
                    else if (incomingMessage.Substring(0,7) == "REQUEST") //client requested for all sweets excepts its
                    {
                        textBox_data.AppendText("Request wanted!\n");
                        userName = incomingMessage.Substring(8);
                        Byte[] buf;
                        string rqt = "";
                        string sweatsFile = "sweets.txt";
                        string blockFile = "blocks.txt";
                        string line;
                        int idx;
                        List<string> blockedMeUsers = new List<string>();
                        using (var blockReader = new StreamReader(blockFile))
                        {
                            while ((line = blockReader.ReadLine()) != null)
                            {
                                idx = line.IndexOf(":");
                                if (idx < 0) { }
                                else
                                {
                                    if (userName != line.Substring(0, idx))
                                    {
                                        if (userName == line.Substring(idx + 1))
                                        {
                                            blockedMeUsers.Add(line.Substring(0, idx));
                                        }

                                    }
                                }
                            }
                        }
                        bool check = false;
                        using (var reader = new StreamReader(sweatsFile)) //reading all sweets
                        {        
                            while ((line = reader.ReadLine()) != null)
                            {
                                idx = line.IndexOf('(');
                                if (idx < 0) {
                                
                                }
                                else
                                {
                                string sweetUser = line.Substring(0, idx-1); //getting all sweets usernames
                                check = false;
                                if (sweetUser!=userName)
                                {
                                   
                                    foreach (string i2 in blockedMeUsers)
                                    {
                                        if (i2 == sweetUser)
                                        {
                                                check = true;
                                       
                                        break;
                                        }
                                    }
                                        if (!check)
                                        {
                                            rqt += line;
                                            rqt += "\n";
                                        }
                                   
                                }
                               
                                
                                }                             
                            }   
                            if (rqt == "")
                            {
                                rqt = "No sweet by other clients!\n";
                                buf = Encoding.Default.GetBytes(rqt);
                                thisClient.Send(buf);
                            }
                            else
                            {
                                buf = Encoding.Default.GetBytes(rqt);
                                thisClient.Send(buf);
                            }  
                        }
                    }   
                    else if(incomingMessage.Substring(0,8) == "FREQUEST")
                    {
                        Byte[] buf;
                        textBox_data.AppendText("Followed Request wanted!\n");
                        List<string> i = incomingMessage.Split("*").ToList();
                        userName = i[1];
                        List<string> followedUsers = new List<string>();
                        string followsFile = "follows.txt";
                        string sweetsFile = "sweets.txt";
                        int idx;
                        string line;
                        string user;
                        bool check;
                        string msg = "";
                        using(var reader = new StreamReader(followsFile))
                        {
                             while ((line = reader.ReadLine()) != null)
                            {
                                idx = line.IndexOf(":");
                                if (idx < 0) { }
                                else
                                {
                                    if(userName == line.Substring(0, idx))
                                    {
                                        check = false;
                                         user = line.Substring(idx+1);
                                        foreach(string n in followedUsers)
                                        {
                                            if (n == user)
                                            {
                                                check = true;
                                                break;
                                            }
                                        }
                                        if(!check)
                                        followedUsers.Add(user);
                                    }
                                }
                            }
                        }
                        if (followedUsers.Count() == 0)
                        {
                            msg = "No people is followed yet!\n";
                            buf = Encoding.Default.GetBytes(msg);
                            thisClient.Send(buf);
                        }
                        else
                        {
                            using (var reader = new StreamReader(sweetsFile))
                            {
                                while ((line = reader.ReadLine()) != null)
                                {
                                    idx = line.IndexOf("(");
                                    if (idx < 0) { }
                                    else
                                    {
                                        user = line.Substring(0, idx - 1);
                                        check = false;
                                        foreach (string followedUser in followedUsers)
                                        {
                                            if (user == followedUser)
                                            {
                                                check = true;
                                                break;
                                            }
                                        }
                                        if (check)
                                        {
                                            msg += line;
                                            msg += "\n";
                                        }
                                    }
                                }          
                            }
                            if (msg == "")
                            {
                                msg = "No sweet by followed clients yet!\n";
                                buf = Encoding.Default.GetBytes(msg);
                                thisClient.Send(buf);
                            }
                            else
                            {
                                buf = Encoding.Default.GetBytes(msg);
                                thisClient.Send(buf);
                            }  
                        }
                    }
                    else if(incomingMessage.Substring(0,10) == "DISCONNECT")
                    {
                        userName = incomingMessage.Substring(11);
                        currusers.Remove(userName);
                        textBox_data.AppendText(userName + " disconnected from the server!\n");
                        thisClient.Close(); //also close so no infinite client:
                    }
                    else if (incomingMessage.Substring(0, 6) == "FOLLOW")
                    {
                        string followsFile = "follows.txt";
                        List<string> i = incomingMessage.Split("*").ToList();
                        userName = i[1];
                        string userToFollow = i[2];
                        if(userName == userToFollow)
                        {
                            string message = "Can not follow yourself!\n";
                            buffer = Encoding.Default.GetBytes(message);
                            thisClient.Send(buffer);
                            textBox_data.AppendText("Client's can not follow themselves!\n");
                        }
                        else
                        {
                        List<string> followedUsers = new List<string>();
                        string rqt;
                        string user;
                        string line;
                        bool check;
                        int idx;
                        bool ch = false;
                        foreach (string name in databaseNames) //check if client name in db
                        {
                            if (userToFollow == name)
                            {
                                ch = true;
                                break;
                            }
                        }
                        if (!ch)
                        {
                            string message = "User " + userToFollow + " not in database, you can not follow!\n";
                            buffer = Encoding.Default.GetBytes(message);
                            thisClient.Send(buffer);
                            textBox_data.AppendText("The user " + userToFollow + " was not in the database, not able to follow!\n");
                        }
                        else
                        {
                        string blockFile = "blocks.txt";
                        List<string> blockedMeUsers = new List<string>();
                        using (var blockReader = new StreamReader(blockFile))
                        {
                            while ((line = blockReader.ReadLine()) != null)
                            {
                                idx = line.IndexOf(":");
                                if (idx < 0) { }
                                else
                                {
                                    if (userName != line.Substring(0, idx))
                                    {
                                        if(userName == line.Substring(idx + 1))
                                        {
                                            blockedMeUsers.Add(line.Substring(0, idx));
                                        }
                        
                                    }
                                }
                            }
                        }
                        bool chek = false;
                        foreach(string i2 in blockedMeUsers)
                        {
                            if (i2 == userToFollow)
                            {
                                chek = true;
                                break;
                            }
                        }
                        if (chek)
                        {
                            string message = "User " + userToFollow + " blocked the client, client isnt allowed to follow!\n";
                            buffer = Encoding.Default.GetBytes(message);
                            thisClient.Send(buffer);
                            textBox_data.AppendText("The user " + userToFollow + " has blocked you, not allowed to follow!\n");
                        }
                        else
                        {
                            using (var reader = new StreamReader(followsFile))
                            {
                                while ((line = reader.ReadLine()) != null)
                                {
                                    idx = line.IndexOf(":");
                                    if (idx < 0) { }
                                    else
                                    {
                                        if (userName == line.Substring(0, idx))
                                        {
                                            check = false;
                                            user = line.Substring(idx + 1);
                                            foreach (string n in followedUsers)
                                            {
                                                if (n == user)
                                                {
                                                    check = true;
                                                    break;
                                                }
                                            }
                                            if (!check)
                                                followedUsers.Add(user);
                                        }
                                    }
                                }
                            }
                            check = false;
                            foreach (string n in followedUsers)
                            {
                                if (n == userToFollow)
                                {
                                    check = true;
                                    textBox_data.AppendText("Client has already followed: " + userToFollow + "!\n");
                                    break; //already followed msg  
                                }
                            }
                            if (!check)
                            {
                                using (StreamWriter writetext = new StreamWriter("follows.txt", append: true)) //appending to follows.txt
                                {
                                    string mesg = userName + ":" + userToFollow;
                                    writetext.WriteLine(mesg);
                                }
                                Byte[] buf;
                                string msg = "You followed: " + userToFollow + "\n";
                                buf = Encoding.Default.GetBytes(msg);
                                thisClient.Send(buf);
                                textBox_data.AppendText("Client has followed: " + userToFollow + "!\n");
                            }
                            else
                            {
                                Byte[] buf;
                                string msg = "Already followed: " + userToFollow + "\n";
                                buf = Encoding.Default.GetBytes(msg);
                                thisClient.Send(buf);
                                        textBox_data.AppendText("Client has already followed: " + userToFollow + "!\n");
                                    }
                        }
                    
                        }

                        }
                    
                    }
                    else if (incomingMessage.Substring(0, 5) == "USERS")
                    {
                        Byte[] buf;
                        string userFile = "user-db.txt";
                        string line;
                        string msg = "All Users: \n";
                        using (var reader = new StreamReader(userFile))
                        {
                            while ((line = reader.ReadLine()) != null)
                                msg += line + "\n";
                        }
                        textBox_data.AppendText(msg);
                        buf = Encoding.Default.GetBytes(msg);
                        thisClient.Send(buf);
                    }
                    else if(incomingMessage.Substring(0,9) == "FFREQUEST")
                    {
                        Byte[] buf;
                        textBox_data.AppendText("Client Requested for list of Followers & Followed!\n");
                        List<string> i = incomingMessage.Split("*").ToList();
                        userName = i[1];
                        string followsFile = "follows.txt";
                        int idx;
                        string line;
                        string user;
                        bool check;
                        string msg = "";
                        List<string> followedUsers = new List<string>();
                        List<string> followingMeUsers = new List<string>();
                        using (var reader = new StreamReader(followsFile))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                idx = line.IndexOf(":");
                                if (idx < 0) { }
                                else
                                {
                                    if (userName == line.Substring(0, idx))
                                    {
                                        string you = line.Substring(idx + 1);
                                        followedUsers.Add(you);
                                    }
                                    else if(userName == line.Substring(idx + 1))
                                    {
                                        string me = line.Substring(0, idx);
                                        followingMeUsers.Add(me);
                                    }
                                }
                            }
                        }
                        if (followedUsers.Count() == 0 && followingMeUsers.Count() == 0)
                        {
                            msg = "No people is followed & no people has followed you yet!\n";
                            buf = Encoding.Default.GetBytes(msg);
                            thisClient.Send(buf);
                            textBox_data.AppendText("Client: no people has followed & following!\n");


                        }
                        else
                        {
                            msg = "You Followed: \n";
                            if (followedUsers.Count() == 0)
                            {
                                msg += "No one is followed \n";
                            }
                            else
                            {
                                foreach(string u in followedUsers)
                                {
                                    msg += u + "\n";
                                }
                            } 
                            msg += "Following You: \n";
                            if (followingMeUsers.Count() == 0)
                            {
                                msg += "No one is following you \n";
                            }
                            else
                            {
                                foreach (string u in followingMeUsers)
                                {
                                    msg += u + "\n";
                                }
                            }           
                            buf = Encoding.Default.GetBytes(msg);
                            thisClient.Send(buf);
                            msg = "Client: " + msg;
                            textBox_data.AppendText(msg);
                        }
                    }
                    else if(incomingMessage.Substring(0,5) == "BLOCK")
                    {
                        string blocksFile = "blocks.txt";
                        List<string> i = incomingMessage.Split("*").ToList();
                        userName = i[1];
                        string userToBlock = i[2];
                        if (userName == userToBlock)
                        {
                            string message = "Can not block yourself!\n";
                            buffer = Encoding.Default.GetBytes(message);
                            thisClient.Send(buffer);
                            textBox_data.AppendText("Client's can not block themselves!\n");
                        }
                        else
                        {
                            List<string> blockedUsers = new List<string>();
                            string rqt;
                            string user;
                            string line;
                            bool check;
                            int idx;
                            bool ch = false;
                            foreach (string name in databaseNames) //check if client name in db
                            {
                                if (userToBlock == name)
                                {
                                    ch = true;
                                    break;
                                }
                            }
                            if (!ch)
                            {
                                string message = "User " + userToBlock + " not in database, you can not follow!\n";
                                buffer = Encoding.Default.GetBytes(message);
                                thisClient.Send(buffer);
                                textBox_data.AppendText("The user " + userToBlock + " was not in the database, not able to follow!\n");
                            }
                            else
                            {
                                using (var reader = new StreamReader(blocksFile))
                                {
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        idx = line.IndexOf(":");
                                        if (idx < 0) { }
                                        else
                                        {
                                            if (userName == line.Substring(0, idx))
                                            {
                                                check = false;
                                                user = line.Substring(idx + 1);
                                                foreach (string n in blockedUsers)
                                                {
                                                    if (n == user)
                                                    {
                                                        check = true;
                                                        break;
                                                    }
                                                }
                                                if (!check)
                                                    blockedUsers.Add(user);
                                            }
                                        }
                                    }
                                }
                                check = false;
                                foreach (string n in blockedUsers)
                                {
                                    if (n == userToBlock)
                                    {
                                        check = true;
                                        textBox_data.AppendText("Client has already blocked: " + userToBlock + "!\n");
                                        break; //already followed msg  
                                    }
                                }
                                if (!check)
                                {
                                    using (StreamWriter writetext = new StreamWriter("blocks.txt", append: true)) //appending to follows.txt
                                    {
                                        string mesg = userName + ":" + userToBlock;
                                        writetext.WriteLine(mesg);
                                    }
                                    Byte[] buf;
                                    string msg = "You blocked: " + userToBlock + "\n";
                                    buf = Encoding.Default.GetBytes(msg);
                                    thisClient.Send(buf);
                                    textBox_data.AppendText("Client has blocked: " + userToBlock + "!\n");

                                    string followFile = "follows.txt";
                                    int count = 0;
                                    bool checkIfFollowing = false;
                                    int idx0 = 0;
                                    string ul, ur;
                                    List<string> lines = new List<string>();
                                    using (var followReader1 = new StreamReader(followFile))
                                    {

                                        while ((line = followReader1.ReadLine()) != null)
                                        {
                                            idx0 = line.IndexOf(":");
                                            if (idx0 < 0) { }
                                            else
                                            {
                                                ul = line.Substring(0, idx0);
                                                ur = line.Substring(idx0 + 1);
                                                if (ul == userToBlock && ur == userName)
                                                {
                                                    checkIfFollowing = true;
                                                }
                                                else
                                                {
                                                    lines.Add(line);
                                                }
                                            }
                                            //count++;
                                        }
                                    }
                                    File.WriteAllText(followFile, String.Empty);
                                    using (StreamWriter writetext = new StreamWriter("follows.txt"))
                                    {
                                        foreach (string s in lines)
                                        {
                                            writetext.WriteLine(s);   
                                        }

                                    }
                                }
                                else
                                {
                                    Byte[] buf;
                                    string msg = "Already blocked: " + userToBlock + "\n";
                                    buf = Encoding.Default.GetBytes(msg);
                                    thisClient.Send(buf);
                                }
                            }
                        }
                    }
                    else if(incomingMessage.Substring(0,16) == "DISPLAYFOLLOWERS")
                    {
                        Byte[] buf;
                        textBox_data.AppendText("Client Requested for list of Followers & Followed!\n");
                        List<string> i = incomingMessage.Split("*").ToList();
                        userName = i[1];
                        string followsFile = "follows.txt";
                        int idx;
                        string line;
                        string user;
                        bool check;
                        string msg = "";
                       
                        List<string> followingMeUsers = new List<string>();
                        using (var reader = new StreamReader(followsFile))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                idx = line.IndexOf(":");
                                if (idx < 0) { }
                                else
                                {
                                    if (userName == line.Substring(idx + 1))
                                    {
                                        string me = line.Substring(0, idx);
                                        followingMeUsers.Add(me);
                                    }
                                }
                            }
                        }
                        if ( followingMeUsers.Count() == 0)
                        {
                            msg = "No people has followed you yet!\n";
                            buf = Encoding.Default.GetBytes(msg);
                            thisClient.Send(buf);
                            textBox_data.AppendText("Client: No people has followed you yet!\n");
                        }
                        else
                        {

                        msg += "Following You: \n";
                        if (followingMeUsers.Count() == 0)
                        {
                            msg += "No one is following you \n";
                        }
                        else
                        {
                            foreach (string u in followingMeUsers)
                            {
                                msg += u + "\n";
                            }
                        }
                        buf = Encoding.Default.GetBytes(msg);
                        thisClient.Send(buf);
                            textBox_data.AppendText("Client: " + msg);
                    }
                    }
                    else if (incomingMessage.Substring(0, 17) == "DISPLAYFOLLOWINGS")
                    {
                        Byte[] buf;
                        textBox_data.AppendText("Client Requested for list of Followers & Followed!\n");
                        List<string> i = incomingMessage.Split("*").ToList();
                        userName = i[1];
                        string followsFile = "follows.txt";
                        int idx;
                        string line;
                        string user;
                        bool check;
                        string msg = "";
                        List<string> followedUsers = new List<string>();
                       
                        using (var reader = new StreamReader(followsFile))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                idx = line.IndexOf(":");
                                if (idx < 0) { }
                                else
                                {
                                    if (userName == line.Substring(0, idx))
                                    {
                                        string you = line.Substring(idx + 1);
                                        followedUsers.Add(you);
                                    }
                                   
                                }
                            }
                        }
                        if (followedUsers.Count() == 0)
                        {
                            msg = "No people is followed yet!\n";
                            buf = Encoding.Default.GetBytes(msg);
                            thisClient.Send(buf);
                            textBox_data.AppendText("Client: No people is followed yet!\n");
                        }
                        else
                        {
                            msg = "You Followed: \n";
                            if (followedUsers.Count() == 0)
                            {
                                msg += "No one is followed \n";
                            }
                            else
                            {
                                foreach (string u in followedUsers)
                                {
                                    msg += u + "\n";
                                }
                            }
                            buf = Encoding.Default.GetBytes(msg);
                            thisClient.Send(buf);
                            textBox_data.AppendText("Client: " + msg);
                        }
                    }
                    else if(incomingMessage.Substring(0,8) == "DELSWEET")
                    {
                        List<string> i = incomingMessage.Split("*").ToList();
                        userName = i[1];
                        string delId = i[2];
                        Byte[] buf;
                        string sweetFile = "sweets.txt";
                        string line;
                        int idx1, idx2;
                        int len = userName.Length; //string len
                        bool ch = false;
                        bool chid = false;
                        List<string> mySweets = new List<string>();
                        using (var reader = new StreamReader(sweetFile))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                idx1 = line.IndexOf("(");
                                idx2 = line.IndexOf(")");
                    
                                if(idx1 >= 0 && idx2 >= 0)
                                {
                                    string name = line.Substring(0, len);
                                    string idd = line.Substring(idx1 + 1, idx2 - idx1 - 1);
                                     if (name == userName && idd == delId)
                                    {
                                        ch = true;
                                    }
                                    else
                                    {
                                        mySweets.Add(line);
                                    }
                                    if(line.Substring(idx1 + 1, idx2 - idx1 - 1) == delId)
                                    {
                                        chid = true;
                                    }
                                }
                                else
                                {

                                }
                               
                            }      
                        }
                        if(!ch & !chid)
                        {
                            string message = "ID " + delId + " not existing!\n";
                            buf = Encoding.Default.GetBytes(message);
                            thisClient.Send(buf);
                            textBox_data.AppendText("The ID of sweet " + delId + " was not in sweets, not able to delete!\n");
                        }
                        else if(!ch & chid)
                        {
                            string message = "ID " + delId + " not existing for this client!\n";
                            buf = Encoding.Default.GetBytes(message);
                            thisClient.Send(buf);
                            textBox_data.AppendText("The ID of sweet " + delId + " was not in sweets of the client, not able to delete!\n");
                        }
                        else
                        {
                            File.WriteAllText(sweetFile, String.Empty);
                            using (StreamWriter writetext = new StreamWriter("sweets.txt"))
                            {
                                foreach (string s in mySweets)
                                {
                                    writetext.WriteLine(s);
                                }
                            }
                            string message = "ID " + delId + " deleted!\n";
                            buf = Encoding.Default.GetBytes(message);
                            thisClient.Send(buf);
                            textBox_data.AppendText("Client: " +userName + " the ID of sweet: " + delId + " deleted!\n");
                            }
                        }
                    else
                    {
                        List<string> i = incomingMessage.Split("*").ToList();
                        string sweetId = GetRandomNumber().ToString();
                        string userId = i[1];
                        string message = i[0] + " (" + sweetId + "): '" + i[3] + "' at " + i[2] + "\n"; //creating the sweet
                        buffer = Encoding.Default.GetBytes(message);
                        using (StreamWriter writetext = new StreamWriter("sweets.txt", append: true)) //appending to sweets.txt
                        {
                            writetext.WriteLine(message);
                        }       
                        Byte[] buf;
                        buf = Encoding.Default.GetBytes(message);
                        thisClient.Send(buf);
                    }               
                }
                catch {}
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) //when form is closed
        {
            listening = false;
            terminating = true;
            foreach (Socket i in clientSockets)
            {
                i.Close();
            }
            serverSocket.Close();
            Environment.Exit(0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //stop button
        {
            listening = false;
            terminating = true;
            textBox_data.AppendText("Server stopped \n");
            button_listen.Enabled = true;
            button_stop.Enabled = false;
            foreach (Socket i in clientSockets)
            {
                i.Close(); //closing all clients before stoping
            }
            serverSocket.Close();
            //Environment.Exit(0);
        }

        private void textBox_data_TextChanged(object sender, EventArgs e)
        {

        }
       
        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber()
        {
            lock(getrandom) //getting random number for user when logged in
            {
                return getrandom.Next(0,2147483647);
            }
        }
    }
}
