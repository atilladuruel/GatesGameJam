using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO/PatentOwnerSO", menuName = "Scriptable Objects/PatentSO")]
public class PatentOwnerSO : ScriptableObject
{
   public string ownerName;
   public Sprite ownerSprite;
    
   public List<Patents> patents;

    [System.Serializable]
    public struct Patents {
        public string patentName;
        public string patentDescription;
        public Sprite patentSprite;
        public bool isTruePatent;
    }

}
