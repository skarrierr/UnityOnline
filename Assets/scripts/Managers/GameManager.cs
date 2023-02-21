using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPlayer1;

    [SerializeField]
    private GameObject spawnPlayer2;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", spawnPlayer1.transform.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Player", spawnPlayer2.transform.position, Quaternion.identity);
        }
    }
}
