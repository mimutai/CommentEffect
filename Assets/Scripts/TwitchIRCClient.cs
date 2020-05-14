using System.Collections;
using System.Collections.Generic;

using System;
using System.Net.Sockets;
using System.IO;

using UnityEngine;

public class TwitchIRCClient : MonoBehaviour
{
    private bool isConnected = false;

    private TcpClient tcpClient;
    private NetworkStream networkStream;
    private StreamReader streamReader;
    private StreamWriter streamWriter;

    public class MessageReceivedEventHandler : UnityEngine.Events.UnityEvent<string> { }
    [NonSerialized]
    public MessageReceivedEventHandler MessageReceived = new MessageReceivedEventHandler();

    //broadcast
    public string twitchIP = "irc.chat.twitch.tv";
    public int port = 6667;
    public string channel = string.Empty;

    //login
    public string username = string.Empty;
    public string oauth = string.Empty;

    /* 外部から値を設定 */
    public void SetChannel(string channel) => this.channel = channel;
    public void SetUserName(string username) => this.username = username;
    public void SetOAuth(string oauth) => this.oauth = oauth;

    /* 接続状態を返す */
    public bool IsConnected() => isConnected;
    

    public void Connect()
    {
        if (channel == string.Empty || username == string.Empty || oauth == string.Empty)
        {
            Debug.LogWarning("[TwitchIRCClient] 必要な情報を入力してください");
            return;
        }

        try
        {
            //Connect to Twitch IRC
            tcpClient = new TcpClient(twitchIP, port);

            networkStream = tcpClient.GetStream();

            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
            

            if (oauth.StartsWith("oauth:")) oauth = oauth.Remove(0, 6); //oauthから始まる場合その文字を削除
            streamWriter.WriteLine("PASS oauth:" + oauth);

            streamWriter.WriteLine("NICK " + username);
            streamWriter.WriteLine("JOIN #" + channel);

            streamWriter.Flush();

            isConnected = true;
            Debug.Log("Connected");
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    public void Disconnect()
    {
        if (!isConnected) return;

        isConnected = false;

        streamReader.Close();
        streamWriter.Close();

        networkStream.Close();
        tcpClient.Close();

        Debug.Log("Disconnected");
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void Update()
    {
        ReadMessage();
    }

    public void ReadMessage()
    {
        if (!isConnected || !networkStream.DataAvailable) return;

        try
        {
            string msg = streamReader.ReadLine();

            /* チャットメッセージをパース */
            if (msg.Contains("PRIVMSG #"))
            {
                MessageReceived.Invoke(ParseRecivedMesssage(msg));
            }

            /* 接続確認に対して応答 */
            if (msg.Contains("PING :tmi.twitch.tv"))
            {
                streamWriter.WriteLine("PONG :tmi.twitch.tv");
                streamWriter.Flush();
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    private string ParseRecivedMesssage(string msg)
    {
        if (msg == null) return null;

        int channelNameLength = channel.Length;
        int targetIndex = msg.IndexOf("PRIVMSG #" + channel);

        string comment = msg.Substring(targetIndex + channelNameLength + 11); //11は"PRIVMSG #"," ",":"の分

        return comment;
    }
}

