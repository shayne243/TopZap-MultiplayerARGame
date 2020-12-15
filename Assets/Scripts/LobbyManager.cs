using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Login UI")]
    public InputField playerNameInputField;
    public GameObject uI_LoginGameObject;

    [Header("Lobby UI")]
    public GameObject uI_LobbyGameObject;
    public GameObject uI_3DGameObject;

    [Header("Connenction Status")]
    public GameObject uI_ConnectionStatusGameObject;
    public Text connectionStatusText;
    public bool showConnectionStatus = false;


    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
        uI_LobbyGameObject.SetActive(false);
        uI_3DGameObject.SetActive(false);
        uI_ConnectionStatusGameObject.SetActive(false);

        uI_LoginGameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (showConnectionStatus)
        {
            connectionStatusText.text = "Connection Status " + PhotonNetwork.NetworkClientState;
        }
    }

    #endregion

    #region UI Calllback Methods
    public void OnEnterGameButtonClicker()
    {


        string playerName = playerNameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            showConnectionStatus = true;
            uI_LobbyGameObject.SetActive(false);
            uI_3DGameObject.SetActive(false);
            uI_ConnectionStatusGameObject.SetActive(true);

            uI_LoginGameObject.SetActive(false);
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;

                PhotonNetwork.ConnectUsingSettings();

            }
        }
        else
        {
            Debug.Log("Player is Invalid!");
        }
    }

    #endregion

    #region PHOTON Callback Methods
    public override void OnConnected()
    {
        Debug.Log("We are connected to Internet");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is Connected to Photon Server");
        uI_LobbyGameObject.SetActive(true);
        uI_3DGameObject.SetActive(true);
        uI_ConnectionStatusGameObject.SetActive(false);

        uI_LoginGameObject.SetActive(false);
    }
    #endregion
}
