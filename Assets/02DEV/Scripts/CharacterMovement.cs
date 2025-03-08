using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    public Image characterImage; // Karakterin UI Image bileþeni
    public float moveDistance = 400f; // Hareket mesafesi (örneðin 400 piksel)
    public float moveDuration = 2f; // Hareket süresi
    public float bobbingAmount = 10f; // Y ekseni boyunca salýným miktarý
    public float bobbingDuration = 0.3f; // Salýným süresi

    private Vector2 startPosition;
    private Vector2 rightTargetPosition;
    private Vector2 leftTargetPosition;
    private bool isMoving = false; // Hareket kontrolü

    void Start()
    {
        RectTransform rectTransform = characterImage.rectTransform;
        startPosition = rectTransform.anchoredPosition;

        // Sað ve sol hedef noktalarýný belirle
        rightTargetPosition = new Vector2(startPosition.x + moveDistance, startPosition.y);
        leftTargetPosition = new Vector2(startPosition.x - moveDistance, startPosition.y);

        // Oyunun baþýnda saða yürüt
        WalkRight();
    }

    public void WalkRight()
    {
        if (isMoving) return; // Eðer þu anda hareket ediyorsa yeni hareket baþlatma

        isMoving = true;
        MoveCharacter(rightTargetPosition, 1, () => isMoving = false); // 1: Sað yön
    }

    public void WalkLeft()
    {
        if (isMoving) return; // Eðer þu anda hareket ediyorsa yeni hareket baþlatma

        isMoving = true;
        MoveCharacter(leftTargetPosition, -1, () => isMoving = false); // -1: Sol yön
    }

    private void MoveCharacter(Vector2 targetPosition, int direction, System.Action onComplete = null)
    {
        characterImage.rectTransform.DOAnchorPos(targetPosition, moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                characterImage.rectTransform.DOAnchorPosY(startPosition.y, 0.2f).SetEase(Ease.OutQuad); // Y eksenini sýfýrla
                onComplete?.Invoke(); // Callback çaðýr (hareket tamamlandý)
            });

        // Yukarý-aþaðý salýným efekti
        characterImage.rectTransform.DOAnchorPosY(startPosition.y + bobbingAmount, bobbingDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(Mathf.CeilToInt(moveDuration / bobbingDuration), LoopType.Yoyo);

        // Karakteri saða/sola çevirmek için scale ayarla
        characterImage.rectTransform.localScale = new Vector3(direction, 1, 1);
    }

    private void StopAllTweens()
    {
        characterImage.rectTransform.DOKill(); // Tüm hareketleri iptal et
    }
}
