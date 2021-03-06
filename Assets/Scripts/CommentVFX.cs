using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;

public class CommentVFX : MonoBehaviour
{
    public GameObject CommentText_Prefab;
    public VisualEffect CommentVisualEffect;
    public VisualEffect CommentVisualEffect_Fireworks;


    private void Update()
    {
        //テスト用にスペースキーを押して表示させるようにした
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ShowComments(new Vector3(Random.Range(-15, 15), Random.Range(-10, 10), 0), "コメントテキスト");
            ShowCommentsFireworks(new Vector3(Random.Range(-15f, 15f), Random.Range(-8f, 8f), 0), "コメントテキスト");
        }
    }

    public void ShowComments(Vector3 position, string comment_text)
    {
        // Comment Text Burst
        GameObject text = Instantiate(CommentText_Prefab, position, Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = comment_text;
        text.GetComponent<Animator>().SetTrigger("Show");

        // Visual Effect Burst
        CommentVisualEffect.SetVector3("BurstPosition", position);
        CommentVisualEffect.SendEvent("Burst VFX01");
    }

    public void ShowCommentsFireworks(Vector3 position, string comment_text)
    {
        // Visual Effect Burst
        float lifetime = Random.Range(1.8f, 2.5f);
        CommentVisualEffect_Fireworks.SetFloat("Lifetime", lifetime);
        CommentVisualEffect_Fireworks.SetVector3("BurstPosition", position);
        CommentVisualEffect_Fireworks.SendEvent("FireworksSpawn");

        // Comment Text Burst
        GameObject text = Instantiate(CommentText_Prefab, position, Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = comment_text;
        text.GetComponent<CommentTextBehaviour>().RunDelayDisplay(lifetime);
    }
}
