using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<RectTransform>().position = new Vector3(0, -5);
        var rt = GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().sizeDelta = new Vector2(rt.x * 0.0625f, rt.y * 0.0625f);
    }


    public void ShowDialog(string text, float duration)
    {
        Debug.Log("ShowDialog");
        Debug.Log(this.ToString() + ": " + GetComponent<RectTransform>().position.ToString());
        StartCoroutine(DialogWorker(text, duration));
    }

    private IEnumerator DialogWorker(string text, float duration)
    {
        /* var position = GetComponent<RectTransform>().localPosition;
        position.x = 67;
        position.y = -400;
        position.z = 0; */
        //GetComponent<RectTransform>().localPosition = position;
        GetComponentInChildren<Text>().text = text;
        yield return new WaitForSeconds(duration);

        //GetComponent<RectTransform>().localPosition = position;
        GetComponentInChildren<Text>().text = "";
    }
}
