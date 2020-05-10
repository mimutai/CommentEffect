using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;

public class CommentVFX : MonoBehaviour
{
    public GameObject CommentText_Prefab;
    public VisualEffect CommentVisualEffect;


    private void Update()
    {
        //テスト用にスペースキーを押して表示させるようにした
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowComments(new Vector3(Random.Range(-15, 15), Random.Range(-10, 10), 0), "コメントテキスト");
        }
    }

    private void ShowComments(Vector3 position, string comment_text)
    {
        // Comment Text Burst
        GameObject text = Instantiate(CommentText_Prefab, position, Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = comment_text;
        text.GetComponent<Animator>().SetTrigger("Show");

        // Visual Effect Burst
        CommentVisualEffect.SetVector3("BurstPosition", position);
        CommentVisualEffect.Play();
    }
}
