using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    /* Scripts */
    public TwitchIRCClient _twitchIRCClient;
    public OpenURL _openURL_Channel;

    /* UI */
    public InputField Channel_IF;
    public InputField UserName_IF;
    public InputField OAuth_IF;
    public Text ConnectButton_Text;

    public void Start()
    {
        UpdateConnectStatus();
    }

    private void UpdateConnectStatus()
    {
        if (_twitchIRCClient.IsConnected())
        {
            /* 接続しているとき */
            ConnectButton_Text.text = "<color=#B90202>Disconnect</color>";
        }
        else
        {
            /* 接続していないとき */
            ConnectButton_Text.text = "<color=#01B91F>Connect</color>";
        }
    }


    /* Event */

    public void OnEndEdit_Channel()
    {
        string channel_text = Channel_IF.text;
        _openURL_Channel.SetLink("https://www.twitch.tv/" + channel_text);
        _twitchIRCClient.SetChannel(channel_text);
    }

    public void OnEndEdit_UserName()
    {
        _twitchIRCClient.SetUserName(UserName_IF.text);
    }

    public void OnEndEdit_OAuth()
    {
        _twitchIRCClient.SetOAuth(OAuth_IF.text);
    }

    public void OnClick_ConnectButton()
    {
        if (_twitchIRCClient.IsConnected())
        {
            /* 接続しているとき */
            _twitchIRCClient.Disconnect();　//切断する
        }
        else
        {
            /* 接続していないとき */
            _twitchIRCClient.Connect(); //接続する
        }

        UpdateConnectStatus();
    }
}
