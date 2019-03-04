using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



public class tyoSocketClient
{
    class tyoSocketClientHttpProxyWorker
    {
        public long id = 0;
        public byte[] msg = null;
        public SocketClientMessage callback = null;
        public bool isSend = false;
        public bool isCallback = false;
        public string content = "";
    }

    public delegate void SocketClientMessage(string _message);
    List<tyoSocketClientHttpProxyWorker> httpProxyWorkerList = new List<tyoSocketClientHttpProxyWorker>();

    string serverIP = "";
    int serverPort = 0;
    LiteNetLib.EventBasedNetListener listener = null;
    LiteNetLib.NetManager client = null;
    LiteNetLib.NetPeer clientPeer = null;
    bool connectFlag = false;

    public tyoSocketClient()
    {

    }
    public void ConnectToServer(string _ip, int _port)
    {
        serverIP = _ip;
        serverPort = _port;

        listener = new LiteNetLib.EventBasedNetListener();
        listener.NetworkErrorEvent += OnNetworkError;
        listener.NetworkReceiveEvent += OnNetworkReceive;
        listener.NetworkReceiveUnconnectedEvent += OnNetworkReceiveUnconnected;
        listener.PeerConnectedEvent += OnPeerConnected;
        listener.PeerDisconnectedEvent += OnPeerDisconnected;
        listener.NetworkLatencyUpdateEvent += OnNetworkLatencyUpdate;


        client = new LiteNetLib.NetManager(listener, "MagicHouseAPP");
        client.Start();
        client.Connect(_ip,_port);
    }

    void OnPeerConnected(LiteNetLib.NetPeer peer)
    {
        tyoCore.Log("Connect Success !");
        clientPeer = peer;
        connectFlag = true;
    }

    void OnPeerDisconnected(LiteNetLib.NetPeer peer, LiteNetLib.DisconnectInfo disconnectInfo)
    {
        tyoCore.Log("Connect Faild !");
        connectFlag = false;
        clientPeer = null;

        
        if (!connectFlag && serverIP.Length > 0 && serverPort > 0)
        {
            ConnectToServer(serverIP, serverPort);

            for (int i = 0; i < httpProxyWorkerList.Count; ++i)
            {
                httpProxyWorkerList[i].isSend = false;
            }
        }
    }

    void OnNetworkError(LiteNetLib.NetEndPoint endPoint, int socketErrorCode)
    {

    }

    void OnNetworkReceive(LiteNetLib.NetPeer peer, LiteNetLib.Utils.NetDataReader reader)
    {
        byte[] buffer = new byte[reader.AvailableBytes];
        reader.GetBytes(buffer,reader.AvailableBytes);

        string s = System.Text.ASCIIEncoding.Unicode.GetString(buffer);

        Dictionary<string, string> _dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(s);

        if (_dictionary.ContainsKey("id") && _dictionary.ContainsKey("content"))
        {
            long _id = long.Parse(_dictionary["id"]);
            tyoSocketClientHttpProxyWorker _worker = httpProxyWorkerList.Find(t => t.id == _id);

            if (_worker != null)
            {
                _worker.content = _dictionary["content"];
                _worker.isCallback = true;
            }
        }
    }

    void OnNetworkReceiveUnconnected(LiteNetLib.NetEndPoint remoteEndPoint, LiteNetLib.Utils.NetDataReader reader, LiteNetLib.UnconnectedMessageType messageType)
    {

    }

    void OnNetworkLatencyUpdate(LiteNetLib.NetPeer peer, int latency)
    {

    }

    public void Update()
    {
        for (int i = 0; i < httpProxyWorkerList.Count; ++i)
        {
            if (httpProxyWorkerList[i].isCallback)
            {
                httpProxyWorkerList[i].callback(httpProxyWorkerList[i].content);
                httpProxyWorkerList.RemoveAt(i);
            }
            else
            {
                if (connectFlag && !httpProxyWorkerList[i].isSend)
                {
                    LiteNetLib.Utils.NetDataWriter _msg = new LiteNetLib.Utils.NetDataWriter();
                    
                    _msg.Put(httpProxyWorkerList[i].msg);

                    clientPeer.Send(_msg,LiteNetLib.SendOptions.ReliableOrdered);

                    httpProxyWorkerList[i].isSend = true;
                }
            }
        }

        client.PollEvents();
    }

    public void AddHttpProxyRequest(long _id, byte[] _msg, SocketClientMessage _callback)
    {
        tyoSocketClientHttpProxyWorker _worker = new tyoSocketClientHttpProxyWorker();

        _worker.id = _id;
        _worker.msg = _msg;
        _worker.callback = _callback;

        httpProxyWorkerList.Add(_worker);
    }
}