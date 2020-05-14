using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url = string.Empty;

    public void Clicked_OpenURL()
    {
        Application.OpenURL(url);
    }

    public void SetLink(string url)
    {
        this.url = url;
    }
}
