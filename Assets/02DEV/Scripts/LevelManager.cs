using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]private List<PatentOwnerSO> patentOwners;
    [SerializeField]private UIItems UIElements;

    public static Action<List<PatentOwnerSO>> GetDataEvent;

    private void OnEnable()
    {
        GetDataEvent += LoadDayData;
    }

    private void OnDisable()
    {
        GetDataEvent -= LoadDayData;
    }


    private void LoadDayData(List<PatentOwnerSO> newDayData)
    {
        patentOwners.Clear();
        foreach (var item in newDayData)
        {
            patentOwners.Add(item);
        }
        
        GetOwnerPatent();
    }

    public void LoadDayDataTtest(List<PatentOwnerSO> newDayData)
    {
        patentOwners.Clear();
        foreach (var item in newDayData)
        {
            patentOwners.Add(item);
        }

        GetOwnerPatent();
    }



    void GetOwnerPatent()
    {
        UIElements.ownerImage.sprite = patentOwners[0].ownerSprite;
        UIElements.ownerName .text= patentOwners[0].ownerName;

        UIElements.patentImage.sprite = patentOwners[0].patents[0].patentSprite;
        UIElements.patentName.text = patentOwners[0].patents[0].patentName;
        UIElements.patentDescription.text = patentOwners[0].patents[0].patentDescription;
    }

    [ContextMenu("Test")]
    public void NextPatentOwner()
    {
        patentOwners.RemoveAt(0);
        GetOwnerPatent();
    }
}
