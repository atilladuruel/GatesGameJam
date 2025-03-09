using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<PatentOwnerSO> patentOwners;
    [SerializeField] private UIItems UIElements;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private List<string> blockedWords;

    [SerializeField] private Animator scrollAnimator;

    [SerializeField] private Animator approveButton;
    [SerializeField] private Animator rejectButton;
    GameManager gameManager;

    [SerializeField] GameObject nexCustomerButton;
    [SerializeField] GameObject gameEndMenu;

    [SerializeField] GameObject first;
    [SerializeField] GameObject second;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
       
    }

    public void LoadDayDataTtest(List<PatentOwnerSO> newDayData, string dayIndex)
    {
        UIElements.dayCounter.text = "Days : " + dayIndex.ToString();
        patentOwners.Clear();
        foreach (var item in newDayData)
        {
            patentOwners.Add(item);
        }
        isFirstTime = false;
        wrongCount = 0;
        trueCount = 0;
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
        AudioManager.Instance.PlaySFX(SFX.Paper);
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
        first.SetActive(false);
        second.SetActive(false);
        // UIElements.ownerImage.sprite = null;
        characterMovement.WalkLeft();
        scrollAnimator.SetTrigger(Close);
        UIElements.ownerName.text = "";

        UIElements.patentImage.sprite = null;
        UIElements.patentName.text = "";
        UIElements.patentDescription.text = "";
    }
    public void NextPatentOwner()
    {
        patentOwners.RemoveAt(0);
        if (patentOwners.Count == 0)
        {
            CalculateGameEnd();
            return;
        }
       
        GetOwnerPatent();
    }

    bool isWordControl = false;

    int wrongCount,trueCount;

    public void ControlPatent(bool isTrue)
    {

        if (isTrue)
        {
            approveButton.SetTrigger("isSelect");
        }
        else
            rejectButton.SetTrigger("isSelect");


        
          
            var patent = patentOwners[0].patents[0];

           // 

            if (patent.isTruePatent != isTrue)
            {
                wrongCount++;
                Debug.Log("failllsss");
                DOVirtual.DelayedCall(2f, () => {
                ClearData();
                OpenCustomerButton();
            });
            return;
            }

            foreach (var item in blockedWords)
            {
                if (patent.patentName.Contains(item) || patent.patentDescription.Contains(item))
                {
                    wrongCount++;
                    Debug.Log("failllsss " + item + " Found");
                    isWordControl = true;
                    break;
                }
            }

            if (!isWordControl)
            {
                Debug.Log("Congratz");
            }
            trueCount++;
            isWordControl = false;

        DOVirtual.DelayedCall(2f, () => {
            ClearData();
            OpenCustomerButton();
        });
           
        

        
    }

    private void OpenCustomerButton()
    {
        
        nexCustomerButton.SetActive(true);
    }

    bool isFirstTime = false;
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    public void NextPatent()
    {
        

        nexCustomerButton.SetActive(false);      

             UIElements.ownerImage.sprite = patentOwners[0].ownerSprite;
        
            characterMovement.WalkLeft(OnComplete);
            scrollAnimator.SetTrigger(Open);

            if (!isFirstTime)
            {
                GetOwnerPatent();
                return;

            }

            void OnComplete()
            {
                characterMovement.WalkRight();
                isFirstTime = true;

            }
            NextPatentOwner();
        
      
    }


    void CalculateGameEnd()
    {
        PlayerPrefs.SetInt("WrongCount", wrongCount);
        PlayerPrefs.SetInt("TrueCount",trueCount);
        gameEndMenu.SetActive(true);
    }

    public void NextDay()
    {
        ClearData();
        gameManager.NextDay();
        gameEndMenu.SetActive(false);
    }

}
