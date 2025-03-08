using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlackListSO", menuName = "Scriptable Objects/BlackListSO")]
public class BlockListSO : ScriptableObject
{

    public List<BlockedList> blockedLists;

    [System.Serializable]
    public struct BlockedList
    {
        public string dayIndex;
        public List<string> blockedNameWord;
        public List<string> blockedDescriptionWord;
    }
}
