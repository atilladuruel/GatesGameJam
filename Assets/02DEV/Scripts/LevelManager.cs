using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]private List<PatentOwnerSO> patentOwners;
    [SerializeField]private UIItems UIElements;

    [SerializeField] private List<string> blockedWords;
 //   public static Action<List<PatentOwnerSO>> GetDataEvent;

  //  private void OnEnable()
  //  {
  //      GetDataEvent += LoadDayData;
 //   }

 //   private void OnDisable()
  //  {
   //     GetDataEvent -= LoadDayData;
  //  }


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

    public void LoadBlockList(BlockListSO.BlockedList blockedList)
    {
        blockedWords.Clear();

        foreach (var item in blockedList.blockedNameWord)
        {
            blockedWords.Add(item);
        }

        foreach (var item in blockedList.blockedDescriptionWord)
        {
            blockedWords.Add(item);
        }
    }

    void GetOwnerPatent()
    {
        var patentOwner = patentOwners[0];
        var patent = patentOwner.patents[0];

        UIElements.ownerImage.sprite = patentOwner.ownerSprite;
        UIElements.ownerName.text = patentOwner.ownerName;

        UIElements.patentImage.sprite = patent.patentSprite;
        UIElements.patentName.text = patent.patentName;
        UIElements.patentDescription.text = patent.patentDescription;
    }

    [ContextMenu("Test")]
    public void NextPatentOwner()
    {
        patentOwners.RemoveAt(0);
        GetOwnerPatent();
    }

    bool isWordControl = false;
    public void ControlPatent(bool isTrue)
    {
        var patent = patentOwners[0].patents[0];

        if (patent.isTruePatent != isTrue) {

            Debug.Log("failllsss");
            NextPatentOwner();
            return;
        }     
     
        foreach (var item in blockedWords)
        {
            if (patent.patentName.Contains(item) || patent.patentDescription.Contains(item))
            {

                Debug.Log("failllsss " + item + " Found");
                isWordControl = true;
                break;
            }
        }

        if (!isWordControl)
        {
            Debug.Log("Congratz");
        }

        isWordControl = false;
        NextPatentOwner();
    }
}
