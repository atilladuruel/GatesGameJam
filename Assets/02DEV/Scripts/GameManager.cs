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
        //LevelManager.GetDataEvent()
        levelManager.LoadDayDataTtest(dayRoutine.DayRoutines[0].patentOwnerSO);
        levelManager.LoadBlockList(BlockListSO.blockedLists[0]);
    }

    public void NextDay()
    {
        dayRoutine.DayRoutines.RemoveAt(0);
        BlockListSO.blockedLists.RemoveAt(0);
        LoadCurrentDay();
    }
}
