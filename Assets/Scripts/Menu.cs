
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    [Header("Screens")] 
    public GameObject mainScreen;
    public GameObject lobbyScreen;
    public GameObject lobbyBrowserScreen;
    public GameObject createRoomScreen;

    [Header("MainScreen")] 
    public Button createRoomButton;
    public Button findRoomButton;

    [Header("LobbyScreen")] 
    public TextMeshProUGUI playerListText;

    public TextMeshProUGUI roomInfoText;
    public Button startGameButton;


    [Header("Lobby Browser")] 
    public RectTransform roomListContainer;

    public GameObject roomButtonPrefab;
    private List<GameObject> roomButtons = new List<GameObject>();
    private List<RoomInfo> roomList = new List<RoomInfo>();

    private void Start()
    {
        createRoomButton.interactable=false;
        findRoomButton.interactable = false;
        PhotonNetwork.NickName = "GunMan" + Random.Range(0, 10);

        Cursor.lockState = CursorLockMode.None;
        
        //Are we in Game?
        if (PhotonNetwork.InRoom)
        {
            // TODO: GO to lobby if in Game
            //  Make the room visible
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
            
        }
    }

    void SetScreen(GameObject screen)
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);
        lobbyBrowserScreen.SetActive(false);
        createRoomScreen.SetActive(false);

        screen.SetActive(true);
    }

    public void OnBackButton()
    {
        SetScreen(mainScreen);
    }
    
    
    //Main Screen

    public void OnPlayerNameValueChanged(TMP_InputField playerNameInput)
    {
        PhotonNetwork.NickName = playerNameInput.text.Length > 0 ? playerNameInput.text : "GunMan"+Random.Range(0, 10);
        // Debug.Log(PhotonNetwork.NickName);
    }

    public override void OnConnectedToMaster()
    {
        createRoomButton.interactable = true;
        findRoomButton.interactable = true;
    }

    public void OnCreateRoomButton()
    {
        SetScreen(createRoomScreen);
    }

    public void OnFindRoomButton()
    {
        SetScreen(lobbyBrowserScreen);
    }

    public void OnQuitGameButton()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
    
    
    // Create Room Screen
    public void OnCreateButton(TMP_InputField roomNameInput)
    {
        NetworkManager.Instance.CreateRoom(roomNameInput.text);
        
    }
}
