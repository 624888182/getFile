using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DB.EAI
{
    class FTPClient
    {

        #region 篶硑ㄧ计
        /// <summary>
        /// 篶硑ㄧ计
        /// </summary>
        public FTPClient()
        {
            strRemoteHost = "";
            strRemotePath = "";
            strRemoteUser = "";
            strRemotePass = "";
            strRemotePort = 21;
            bConnected = false;
        }

        /// <summary>
        /// 篶硑ㄧ计
        /// </summary>
        /// <param name="remoteHost"></param>
        /// <param name="remotePath"></param>
        /// <param name="remoteUser"></param>
        /// <param name="remotePass"></param>
        /// <param name="remotePort"></param>
        public FTPClient(string remoteHost, string remotePath, string remoteUser, string remotePass, int remotePort)
        {
            strRemoteHost = remoteHost;
            strRemotePath = remotePath;
            strRemoteUser = remoteUser;
            strRemotePass = remotePass;
            strRemotePort = remotePort;
            Connect();
        }
        #endregion

        #region 祅嘲
        /// <summary>
        /// FTP狝竟IP
        /// </summary>
        private string strRemoteHost;
        public string RemoteHost
        {
            get
            {
                return strRemoteHost;
            }
            set
            {
                strRemoteHost = value;
            }
        }
        /// <summary>
        /// FTP狝竟梆
        /// </summary>
        private int strRemotePort;
        public int RemotePort
        {
            get
            {
                return strRemotePort;
            }
            set
            {
                strRemotePort = value;
            }
        }
        /// <summary>
        /// 讽玡狝竟ヘ魁
        /// </summary>
        private string strRemotePath;
        public string RemotePath
        {
            get
            {
                return strRemotePath;
            }
            set
            {
                strRemotePath = value;
            }
        }
        /// <summary>
        /// 祅魁ㄏノ眀腹
        /// </summary>
        private string strRemoteUser;
        public string RemoteUser
        {
            set
            {
                strRemoteUser = value;
            }
        }
        /// <summary>
        /// ㄏノ祅魁盞絏
        /// </summary>
        private string strRemotePass;
        public string RemotePass
        {
            set
            {
                strRemotePass = value;
            }
        }

        /// <summary>
        /// 琌祅魁
        /// </summary>
        private Boolean bConnected;
        public bool Connected
        {
            get
            {
                return bConnected;
            }
        }
        #endregion

        #region 硈挡
        /// <summary>
        /// ミ硈钡 
        /// </summary>
        public void Connect()
        {
            IPEndPoint main_ipEndPoint;
            socketControl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
#if NET1
                main_ipEndPoint = new IPEndPoint(Dns.GetHostByName(RemoteHost).AddressList[0], strRemotePort);
#else
            main_ipEndPoint = new IPEndPoint(System.Net.Dns.GetHostEntry(RemoteHost).AddressList[0], strRemotePort);
#endif
            // 硈挡
            try
            {
                socketControl.Connect(main_ipEndPoint);
            }
            catch (Exception)
            {
                OracleDB.Write("Couldn't connect to remote server");
                throw new IOException("Couldn't connect to remote server");
            }

            // 莉莱氮絏
            ReadReply();
            if (iReplyCode != 220)
            {
                DisConnect();
                throw new IOException(strReply.Substring(4));
            }

            // 祅嘲
            SendCommand("USER " + strRemoteUser);
            if (!(iReplyCode == 331 || iReplyCode == 230))
            {
                CloseSocketConnect();//闽超硈钡
                throw new IOException(strReply.Substring(4));
            }
            if (iReplyCode != 230)
            {
                SendCommand("PASS " + strRemotePass);
                if (!(iReplyCode == 230 || iReplyCode == 202))
                {
                    CloseSocketConnect();//闽超硈钡
                    throw new IOException(strReply.Substring(4));
                }
            }
            bConnected = true;

            // ち传ヘ魁
            ChDir(strRemotePath);
        }


        /// <summary>
        /// 闽超硈钡
        /// </summary>
        public void DisConnect()
        {
            if (socketControl != null)
            {
                SendCommand("QUIT");
            }
            CloseSocketConnect();
        }

        #endregion

        #region 肚块家Α

        /// <summary>
        /// 肚块家Α:秈摸ASCII摸
        /// </summary>
        public enum TransferType { Binary, ASCII };

        /// <summary>
        /// 砞竚肚块家Α
        /// </summary>
        /// <param name="ttType">肚块家Α</param>
        public void SetTransferType(TransferType ttType)
        {
            if (ttType == TransferType.Binary)
            {
                SendCommand("TYPE I");//binary摸肚块
            }
            else
            {
                SendCommand("TYPE A");//ASCII摸肚块
            }
            if (iReplyCode != 200)
            {
                throw new IOException(strReply.Substring(4));
            }
            else
            {
                trType = ttType;
            }
        }


        /// <summary>
        /// 莉眔肚块家Α
        /// </summary>
        /// <returns>肚块家Α</returns>
        public TransferType GetTransferType()
        {
            return trType;
        }

        #endregion

        #region 郎巨
        /// <summary>
        /// 莉眔郎睲虫
        /// </summary>
        /// <param name="strMask">郎で皌﹃</param>
        /// <returns></returns>
        public string[] Dir(string strMask)
        {
            // ミ硈挡
            if (!bConnected)
            {
                Connect();
            }

            //ミ秈︽戈硈钡socket
            Socket socketData = CreateDataSocket();

            //肚癳㏑
            SendCommand("NLST " + strMask);

            if (iReplyCode == 550)
            {
                return new string[] { };
            }

            //だ猂莱氮絏
            if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226))
            {
                OracleDB.Write(strReply.Substring(4));
                throw new IOException(strReply.Substring(4));
            }

            //莉眔挡狦
            strMsg = "";
            while (true)
            {
                int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                strMsg += ASCII.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] strsFileList = strMsg.Replace("\r", "").Split(seperator);
            socketData.Close();//戈socket闽超穦Τ絏
            if (iReplyCode != 226)
            {
                ReadReply();
                if (iReplyCode != 226)
                {
                    OracleDB.Write(strReply.Substring(4));
                    throw new IOException(strReply.Substring(4));
                }
            }
            return strsFileList;
        }


        /// <summary>
        /// 莉郎
        /// </summary>
        /// <param name="strFileName">郎</param>
        /// <returns>郎</returns>
        public long GetFileSize(string strFileName)
        {
            if (!bConnected)
            {
                Connect();
            }
            SendCommand("SIZE " + Path.GetFileName(strFileName));
            long lSize = 0;
            string strtmp = strReply.Replace("213 ", "").Replace("\r", "");
            if (iReplyCode == 213)
            {
                lSize = Int64.Parse(strtmp);
            }
            else
            {
                ;//throw new IOException(strtmp);
            }
            return lSize;
        }


        /// <summary>
        /// 埃
        /// </summary>
        /// <param name="strFileName">埃郎</param>
        public void Delete(string strFileName)
        {
            if (!bConnected)
            {
                Connect();
            }
            SendCommand("DELE " + strFileName);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }

        /// <summary>
        /// ㏑(狦穝郎籔Τ郎,盢滦籠Τ郎)
        /// </summary>
        /// <param name="strOldFileName">侣郎</param>
        /// <param name="strNewFileName">穝郎</param>
        public void Rename(string strOldFileName, string strNewFileName)
        {
            if (!bConnected)
            {
                Connect();
            }
            SendCommand("RNFR " + strOldFileName);
            if (iReplyCode != 350)
            {
                throw new IOException(strReply.Substring(4));
            }
            // 狦穝郎籔Τ郎,盢滦籠Τ郎
            SendCommand("RNTO " + strNewFileName);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }
        #endregion

        #region 肚㎝更
        /// <summary>
        /// 更у郎
        /// </summary>
        /// <param name="strFileNameMask">郎で皌﹃</param>
        /// <param name="strFolder">セヘ魁(ぃ眔\挡)</param>
        public void Get(string strFileNameMask, string strFolder)
        {
            if (!bConnected)
            {
                Connect();
            }
            string[] strFiles = Dir(strFileNameMask);
            foreach (string strFile in strFiles)
            {
                if (!strFile.Equals(""))//ㄓ弧strFiles程じ琌﹃
                {
                    Get(strFile, strFolder, strFile);
                }
            }
        }


        /// <summary>
        /// 更郎
        /// </summary>
        /// <param name="strRemoteFileName">璶更郎</param>
        /// <param name="strFolder">セヘ魁(ぃ眔\挡)</param>
        /// <param name="strLocalFileName">玂セ郎</param>
        public void Get(string strRemoteFileName, string strFolder, string strLocalFileName)
        {
            if (!bConnected)
            {
                Connect();
            }
            SetTransferType(TransferType.Binary);
            if (strLocalFileName.Equals(""))
            {
                strLocalFileName = strRemoteFileName;
            }
            if (!File.Exists(strFolder + "\\" + strLocalFileName))
            {
                File.Delete(strFolder + "\\" + strLocalFileName);
            }
            FileStream output = new
                FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
            Socket socketData = CreateDataSocket();
            SendCommand("RETR " + strRemoteFileName);
            if (!(iReplyCode == 150 || iReplyCode == 125
                || iReplyCode == 226 || iReplyCode == 250))
            {
                OracleDB.Write(strReply.Substring(4));
                throw new IOException(strReply.Substring(4));
            }
            while (true)
            {
                int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                output.Write(buffer, 0, iBytes);
                if (iBytes <= 0)
                {
                    break;
                }
            }
            output.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    OracleDB.Write(strReply.Substring(4));
                    throw new IOException(strReply.Substring(4));
                }
            }
        }


        /// <summary>
        /// 肚у郎
        /// </summary>
        /// <param name="strFolder">セヘ魁(ぃ眔\挡)</param>
        /// <param name="strFileNameMask">郎で皌じ(*㎝?)</param>
        public void Put(string strFolder, string strFileNameMask)
        {
            string[] strFiles = Directory.GetFiles(strFolder, strFileNameMask);
            foreach (string strFile in strFiles)
            {
                //strFile琌Ч俱郎(隔畖)
                Put(strFile);
            }
        }


        /// <summary>
        /// 肚郎
        /// </summary>
        /// <param name="strFileName">セ郎</param>
        public void Put(string strFileName)
        {
            if (!bConnected)
            {
                Connect();
            }
            Socket socketData = CreateDataSocket();
            SendCommand("STOR " + Path.GetFileName(strFileName));
            if (!(iReplyCode == 125 || iReplyCode == 150))
            {
                throw new IOException(strReply.Substring(4));
            }
            FileStream input = new
                FileStream(strFileName, FileMode.Open);
            int iBytes = 0;
            while ((iBytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                socketData.Send(buffer, iBytes, 0);
            }
            input.Close();
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
            }
        }

        #endregion

        #region ヘ魁巨
        /// <summary>
        /// 承ヘ魁
        /// </summary>
        /// <param name="strDirName">ヘ魁</param>
        public void MkDir(string strDirName)
        {
            if (!bConnected)
            {
                Connect();
            }
            SendCommand("MKD " + strDirName);
            if (iReplyCode != 257)
            {
                throw new IOException(strReply.Substring(4));
            }
        }


        /// <summary>
        /// 埃ヘ魁
        /// </summary>
        /// <param name="strDirName">ヘ魁</param>
        public void RmDir(string strDirName)
        {
            if (!bConnected)
            {
                Connect();
            }
            SendCommand("RMD " + strDirName);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }


        /// <summary>
        /// э跑ヘ魁
        /// </summary>
        /// <param name="strDirName">穝ヘ魁</param>
        public void ChDir(string strDirName)
        {
            if (strDirName.Equals(".") || strDirName.Equals(""))
            {
                return;
            }
            if (!bConnected)
            {
                Connect();
            }
            SendCommand("CWD " + strDirName);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
            this.strRemotePath = strDirName;
        }

        #endregion

        #region ?场?秖
        /// <summary>
        /// 狝?竟?氮獺(?氮?)
        /// </summary>
        private string strMsg;
        /// <summary>
        /// 狝?竟?氮獺(?氮?)
        /// </summary>
        private string strReply;
        /// <summary>
        /// 狝?竟?氮?
        /// </summary>
        private int iReplyCode;
        /// <summary>
        /// ?︽北?钡socket
        /// </summary>
        private Socket socketControl;
        /// <summary>
        /// ??家Α
        /// </summary>
        private TransferType trType;
        /// <summary>
        /// 钡Μ㎝?癳?誹???
        /// </summary>
        private static int BLOCK_SIZE = 512;
        Byte[] buffer = new Byte[BLOCK_SIZE];
        /// <summary>
        /// ??よΑ
        /// </summary>
        Encoding ASCII = Encoding.ASCII;
        #endregion

        #region ?场ㄧ?
        /// <summary>
        /// ?︽?氮才﹃??strReply㎝strMsg
        /// ?氮???iReplyCode
        /// </summary>
        private void ReadReply()
        {
            strMsg = "";
            strReply = ReadLine();
            iReplyCode = Int32.Parse(strReply.Substring(0, 3));
        }
        /// <summary>
        /// ミ秈︽戈硈钡socket
        /// </summary>
        /// <returns>戈硈钡socket</returns>
        private Socket CreateDataSocket()
        {
            SendCommand("PASV");
            if (iReplyCode != 227)
            {
                throw new IOException(strReply.Substring(4));
            }
            int index1 = strReply.IndexOf('(');
            int index2 = strReply.IndexOf(')');
            string ipData =
                strReply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV strReply: " +
                        strReply);
                }
                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV strReply: " +
                            strReply);
                    }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." +
                parts[2] + "." + parts[3];
            int port = (parts[4] << 8) + parts[5];
            Socket s = new
                Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new
                IPEndPoint(IPAddress.Parse(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Can't connect to remote server");
            }
            return s;
        }


        /// <summary>
        /// 闽超socket硈钡(ノ祅魁玡)
        /// </summary>
        private void CloseSocketConnect()
        {
            if (socketControl != null)
            {
                socketControl.Close();
                socketControl = null;
            }
            bConnected = false;
        }

        /// <summary>
        /// 弄Socket┮Τ﹃
        /// </summary>
        /// <returns>莱氮絏﹃︽</returns>
        private string ReadLine()
        {
            while (true)
            {
                int iBytes = socketControl.Receive(buffer, buffer.Length, 0);
                strMsg += ASCII.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] mess = strMsg.Split(seperator);
            if (strMsg.Length > 2)
            {
                strMsg = mess[mess.Length - 2];
                //seperator[0]琌10,だ︽才腹琌パ13㎝0舱Θ,だ筳10瘤⊿Τ﹃,
                //穦だ皌﹃倒(琌程)﹃皚,
                //┮程mess琌⊿ノ﹃
                //ぐ或ぃ钡mess[0],Τ程︽﹃莱氮絏籔戈癟ぇ丁Τ
            }
            else
            {
                strMsg = mess[0];
            }
            if (!strMsg.Substring(3, 1).Equals(" "))//﹃タ絋琌莱氮絏(220秨繷,钡,钡拜﹃)
            {
                return ReadLine();
            }
            return strMsg;
        }


        /// <summary>
        /// 祇癳㏑莉莱氮絏㎝程︽莱氮﹃
        /// </summary>
        /// <param name="strCommand">㏑</param>
        private void SendCommand(String strCommand)
        {
            Byte[] cmdBytes =
                Encoding.ASCII.GetBytes((strCommand + "\r\n").ToCharArray());
            socketControl.Send(cmdBytes, cmdBytes.Length, 0);
            ReadReply();
        }

        #endregion

    }
}

