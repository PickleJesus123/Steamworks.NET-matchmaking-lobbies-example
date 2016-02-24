using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using System.Collections;

public class lobbyserverTEST : MonoBehaviour {

    protected Callback<LobbyCreated_t> Callback_lobbyCreated;
    protected Callback<LobbyMatchList_t> Callback_lobbyList;
    protected Callback<LobbyEnter_t> Callback_lobbyEnter;

    protected CSteamID firstLobbyListed;

    // Use this for initialization
    void Start () {

        Callback_lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        Callback_lobbyList = Callback<LobbyMatchList_t>.Create(OnGetLobbiesList);
        Callback_lobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

        if (SteamAPI.Init())
            Debug.Log("Steam API init -- SUCCESS!");
        else
            Debug.Log("Steam API init -- failure ...");
    }

    void Update()
    {
        SteamAPI.RunCallbacks();

        // Create new lobby
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Trying to create lobby ...");
            SteamAPICall_t try_toHost = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, 8);
        }

        // List lobbies
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Trying to get list of available lobbies ...");
            SteamAPICall_t try_getList = SteamMatchmaking.RequestLobbyList();
        }

        // Join lobby at index 0 (testing purposes)
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Trying to join FIRST listed lobby ...");
            SteamAPICall_t try_joinLobby = SteamMatchmaking.JoinLobby(firstLobbyListed);
        }
    }

    void OnLobbyCreated(LobbyCreated_t result)
    {
        if (result.m_eResult == EResult.k_EResultOK)
            Debug.Log("Lobby created -- SUCCESS!");
        else
            Debug.Log("Lobby created -- failure ...");
    }

    void OnGetLobbiesList(LobbyMatchList_t result)
    {
        Debug.Log("Found " + result.m_nLobbiesMatching + " lobbies!");
        firstLobbyListed = SteamMatchmaking.GetLobbyByIndex(0);
    }

    void OnLobbyEntered(LobbyEnter_t result)
    {
        if (result.m_EChatRoomEnterResponse == 1)
        {
            Debug.Log("Lobby joined successfully!");
            int numPlayers = SteamMatchmaking.GetNumLobbyMembers(firstLobbyListed);

            Debug.Log("\t Number of players : " + numPlayers);
        }
        else
            Debug.Log("Failed to join lobby.");
    }

}
