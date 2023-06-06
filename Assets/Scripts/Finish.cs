using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static GameObject FinishLine;

    private void Start()=> FinishLine = transform.gameObject;

    private void OnTriggerEnter(Collider hit)
    {
        if (!hit.gameObject.CompareTag("Player")) return;
        Variables.FirstTouch = 0;
        StartCoroutine(UIManager.Instance.FinishUpdate());
    }
}