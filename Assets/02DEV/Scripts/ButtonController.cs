using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    [SerializeField ]Image image;

    [SerializeField] Animator _anim;

    
    public void OpenButtonImage()
    {
        image.gameObject.SetActive(true);
    }
}
