using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentTextBehaviour : MonoBehaviour
{
    public float destoryTime = 5;

    void Start()
    {
        Destroy(this.gameObject, destoryTime);
    }
}
