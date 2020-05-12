using System.Collections;
using System.Collections.Generic;

using System;
using System.Net.Sockets;
using System.IO;

using UnityEngine;

public class TwitchIRCClient
{
    private bool isConnecting;

    private TcpClient tcpClient;
    private NetworkStream networkStream;
    private StreamReader streamReader;
    private StreamWriter streamWriter;

    public TwitchIRCClient(string ip, int port, string channel, string username, string oauth)
    {
        try
        {
            //Connect to Twitch IRC
            tcpClient = new TcpClient(ip, port);

            networkStream = tcpClient.GetStream();

            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);

            if (oauth.StartsWith("oauth:")) oauth.Remove(0, 6); //oauthから始まる場合その文字を削除
            streamWriter.WriteLine("PASS oauth:" + oauth);

            streamWriter.WriteLine("NICK " + username);
            streamWriter.WriteLine("JOIN #" + channel);

            streamWriter.Flush();

            isConnecting = true;
            Debug.Log("Connected");
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    public void Disconnect()
    {
        if (!isConnecting) return;

        isConnecting = false;

        tcpClient.Close();
        networkStream.Close();
        streamReader.Close();
        streamWriter.Close();

        Debug.Log("Disconnected");
    }

    public string ReadMessage()
    {
        try
        {
            string msg = streamReader.ReadLine();
            return msg;
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            return null;
        }
    }

}

