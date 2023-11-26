using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text time;

    private void Awake()
    {
        time = GetComponent<TMP_Text>();
    }

    void Start()
    {
        StartCoroutine(Time());
    }


    IEnumerator Time()
    {
        while (true)
        {
            time.text = $"{DateTime.Now} {DateTime.Now.DayOfWeek}";
            yield return new WaitForSeconds(1);
        }
    }
}
