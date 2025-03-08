using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameEndController : MonoBehaviour
{
   [SerializeField] List<GameObject> endTitles;

   [SerializeField] TextMeshProUGUI trueText;
   [SerializeField] TextMeshProUGUI wrongText;
    [SerializeField] TextMeshProUGUI resultText;



    private void Start()
    {
        SetData();
    }

    private void SetData()
    {
        int trueCount = PlayerPrefs.GetInt("TrueCount");
        int wrongCount = PlayerPrefs.GetInt("WrongCount");

        trueText.text = trueCount.ToString();
        wrongText.text = wrongCount.ToString();

        var result = trueCount - wrongCount;

        resultText.text = result.ToString();

        if (result > 0)
        {
            endTitles[0].SetActive(true);
        }
        else endTitles[1].SetActive(true);
    }

    private void OnDisable()
    {
        foreach (var item in endTitles)
        {
            item.SetActive(false);
        }
    }
}
