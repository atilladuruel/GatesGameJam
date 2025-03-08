using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayTypeSO", menuName = "Scriptable Objects/DayTypeSO")]
public class DayTypeSO : ScriptableObject
{
    
   public List<DaysRoutine> DayRoutines;


    [System.Serializable]
    public struct DaysRoutine
    {
        public string DayName;
        public List<PatentOwnerSO> patentOwnerSO;
    }
}
