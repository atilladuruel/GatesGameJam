using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    [SerializeField ]GameObject image;

    [SerializeField] Animator _anim;

    
    public void OpenButtonImage()
    {
        image.gameObject.SetActive(true);
    }
}
