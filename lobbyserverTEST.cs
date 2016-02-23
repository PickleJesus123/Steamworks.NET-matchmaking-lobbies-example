using UnityEngine;
using Steamworks;
using System.Collections;

public class lobbyserverTEST : MonoBehaviour {

    protected Callback<LobbyCreated_t> m_Callback_lobbyCreated;

	// Use this for initialization
	void Start () {

        m_Callback_lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);

        if (SteamAPI.Init())
            Debug.Log("Steam API init -- SUCCESS!");
        else
            Debug.Log("Steam API init -- failure ...");
    }

    void Update()
    {
        SteamAPI.RunCallbacks();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("trying to create lobby ...");
            SteamAPICall_t tryHost = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, 8);
        }
    }

    void OnLobbyCreated(LobbyCreated_t result)
    {
        if (result.m_eResult == EResult.k_EResultOK)
        {
            Debug.Log("Lobby created -- SUCCESS!");
        }
        else
        {
            Debug.Log("Lobby created -- failure ...");
        }
    }
	
}
