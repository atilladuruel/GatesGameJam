using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] DayTypeSO dayRoutine;
    [SerializeField] BlockListSO BlockListSO;

    LevelManager levelManager;
    
    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
        LoadCurrentDay();
    }

    private void LoadCurrentDay()
    {
        //LevelManager.GetDataEvent.Invoke(dayRoutine.DayRoutines[0].patentOwnerSO);
        levelManager.LoadDayDataTtest(dayRoutine.DayRoutines[0].patentOwnerSO);
        levelManager.LoadBlockList(BlockListSO.blockedLists[0]);
    }
}
