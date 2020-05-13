using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentTextBehaviour : MonoBehaviour
{
    private float destoryTime = 6.5f;

    void Start()
    {
        Destroy(this.gameObject, destoryTime);
    }

    public void RunDelayDisplay(float delayTime) => StartCoroutine(DelayDisplay(delayTime));
    public IEnumerator DelayDisplay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GetComponent<Animator>().SetTrigger("Show");
    }
}
