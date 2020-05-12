using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentEffect : MonoBehaviour
{
    TwitchIRCClient twitchIRCClient;
    CommentVFX commentVFX;

    private void Awake()
    {
        twitchIRCClient = GetComponent<TwitchIRCClient>();
        twitchIRCClient.MessageReceived.AddListener(MessageReceived);


        commentVFX = GetComponent<CommentVFX>();
    }

    void Start()
    {
        twitchIRCClient.Connect();
    }

    void Update()
    {

    }

    public void MessageReceived(string message)
    {
        commentVFX.ShowComments(new Vector3(Random.Range(-15, 15), Random.Range(-10, 10), 0), message);
    }
}


