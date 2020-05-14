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
        
    }

    void Update()
    {

    }

    public void MessageReceived(string message)
    {
        commentVFX.ShowCommentsFireworks(new Vector3(Random.Range(-15f, 15f), Random.Range(-8f, 8f), 0), message);

    }

    public void ApplicationExit()
    {
        twitchIRCClient.Disconnect();
        Application.Quit();
        Debug.Log("Quit");
    }
}


