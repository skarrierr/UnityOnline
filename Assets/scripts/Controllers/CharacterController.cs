using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController : MonoBehaviourPun, IPunObservable
{
    [Header("Stats")]
    [SerializeField]
    private float speed = 600f;

    [SerializeField]
    private float jumpforce = 400;

    private Rigidbody rb;
    private float desiredMovementAxis = 0;

    private PhotonView pv;
    private Vector3 enemyPosition = Vector3.zero;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();

        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 20;

    }
    private void Update()
    {
        if (pv.IsMine)
        {
            CheckInputs();
        }
        else
        {
            SmoothReplicate();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(desiredMovementAxis * Time.deltaTime * speed, rb.velocity.y);
    }
    

    private void CheckInputs() {
        desiredMovementAxis = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && Mathf.Approximately(rb.velocity.y, 0f))
        {
            rb.AddForce(new Vector2(0f, jumpforce));
        }
    }

    private void SmoothReplicate()
    {
        transform.position = Vector3.Lerp(transform.position, enemyPosition, Time.deltaTime * 20);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) 
        {
            stream.SendNext(transform.position);
        } 
        else if (stream.IsReading)
        {
            enemyPosition = (Vector3)stream.ReceiveNext();
        }
    }
}
