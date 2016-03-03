using UnityEngine;
using Steamworks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

public class onlineGameplay : MonoBehaviour {

    protected Callback<P2PSessionRequest_t> Callback_newConnection;
    public List<CSteamID> lobby_members_;
    public List<string> lobby_names_;

    void Start () {
        lobby_names_ = new List<string>();
        lobby_members_ = new List<CSteamID>();
        Callback_newConnection = Callback<P2PSessionRequest_t>.Create(OnNewConnection);
    }

    void Update()
    {
        getNetworkData();
    }

    void getNetworkData()
    {
        uint msgSize;
        while(SteamNetworking.IsP2PPacketAvailable(out msgSize))
        {
            byte[] packet = new byte[msgSize];
            CSteamID steamIDRemote;
            uint bytesRead = 0;
            if (SteamNetworking.ReadP2PPacket(packet, msgSize, out bytesRead, out steamIDRemote))
            {
                int TYPE = packet[0];
                string msg = System.Text.Encoding.UTF8.GetString(SubArray(packet, 1, packet.Length-1));
                
                switch (TYPE)
                {
                    case 1: //Packet type 1 == GOT CHAT MESSAGE
                        Debug.log(lobby_names_[getPlayerIndex(steamIDRemote)] + " says: "+msg);
                        break;
                    default: Debug.Log("BAD PACKET"); break;
                }
                
            }
        }
    }

    int getPlayerIndex(CSteamID input)
    {
        for (int i = 0; i < lobby_members_.Count; i++)
            if (lobby_members_[i] == input)
                return i;
        return -1;
    }

    public void net_broadcast(int TYPE, string message)
    {
        for(int i=0; i<lobby_members_.Count; i++)
        {
            if (lobby_members_[i] == SteamUser.GetSteamID())
                continue;
            byte[] b_message = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] sendBytes = new byte[b_message.Length + 1];
            sendBytes[0] = (byte)TYPE;
            b_message.CopyTo(sendBytes, 1);
            SteamNetworking.SendP2PPacket(lobby_members_[i], sendBytes, (uint)sendBytes.Length, EP2PSend.k_EP2PSendReliable);
        }
    }
	
    void OnNewConnection(P2PSessionRequest_t result)
    {
        foreach(CSteamID id in lobby_members_)
            if (id == result.m_steamIDRemote)
            {
                SteamNetworking.AcceptP2PSessionWithUser(result.m_steamIDRemote);
                return;
            }
    }

    // IMPORTED
    public T[] SubArray<T>(T[] data, int index, int length)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }
}
