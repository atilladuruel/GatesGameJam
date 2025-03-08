using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]private List<PatentOwnerSO> patentOwners;
    [SerializeField]private UIItems UIElements;

    [SerializeField] private List<string> blockedWords;

    GameManager gameManager;

    [SerializeField] GameObject nexCustomerButton;
     

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void LoadDayDataTtest(List<PatentOwnerSO> newDayData , string dayIndex)
    {
        UIElements.dayCounter.text = "Days : " + dayIndex.ToString();
        patentOwners.Clear();
        foreach (var item in newDayData)
        {
            patentOwners.Add(item);
        }
        isFirstTime = false;
        OpenCustomerButton();
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
       // AudioManager.Instance.PlaySFX("Paper");
        var patentOwner = patentOwners[0];
        var patent = patentOwner.patents[0];

        UIElements.ownerImage.sprite = patentOwner.ownerSprite;
        UIElements.ownerName.text = patentOwner.ownerName;

        UIElements.patentImage.sprite = patent.patentSprite;
        UIElements.patentName.text = patent.patentName;
        UIElements.patentDescription.text = patent.patentDescription;
    }

    void ClearData()
    {

        UIElements.ownerImage.sprite = null;
        UIElements.ownerName.text = "";

        UIElements.patentImage.sprite = null;
        UIElements.patentName.text = "";
        UIElements.patentDescription.text = "";
    }
    public void NextPatentOwner()
    {
        if (patentOwners.Count==0)
        {
            gameManager.NextDay();
            return;
        }

        patentOwners.RemoveAt(0);
        GetOwnerPatent();
    }

    bool isWordControl = false;
    public void ControlPatent(bool isTrue)
    {
        var patent = patentOwners[0].patents[0];
        ClearData();
        if (patent.isTruePatent != isTrue) {

            Debug.Log("failllsss");
            OpenCustomerButton();
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
        OpenCustomerButton();
    }

  private void OpenCustomerButton() {
        nexCustomerButton.SetActive(true);
    }

    bool isFirstTime = false;
    public void NextPatent()
    {
        
        nexCustomerButton.SetActive(false);

        if (!isFirstTime)
        {
            GetOwnerPatent();
            isFirstTime = true;

            return;
        }
        NextPatentOwner();
    }

}
