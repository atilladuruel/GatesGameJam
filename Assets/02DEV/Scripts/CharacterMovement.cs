using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    public Image characterImage; // Karakterin UI Image bile�eni
    public float moveDistance = 400f; // Hareket mesafesi (�rne�in 400 piksel)
    public float moveDuration = 2f; // Hareket s�resi
    public float bobbingAmount = 10f; // Y ekseni boyunca sal�n�m miktar�
    public float bobbingDuration = 0.3f; // Sal�n�m s�resi

    private Vector2 startPosition;
    private Vector2 rightTargetPosition;
    private Vector2 leftTargetPosition;
    private bool isMoving = false; // Hareket kontrol�

    void Start()
    {
        RectTransform rectTransform = characterImage.rectTransform;
        startPosition = rectTransform.anchoredPosition;

        // Sa� ve sol hedef noktalar�n� belirle
        rightTargetPosition = new Vector2(startPosition.x + moveDistance, startPosition.y);
        leftTargetPosition = new Vector2(startPosition.x - moveDistance, startPosition.y);

        // Oyunun ba��nda sa�a y�r�t
        WalkRight();
    }

    public void WalkRight()
    {
        if (isMoving) return; // E�er �u anda hareket ediyorsa yeni hareket ba�latma

        isMoving = true;
        MoveCharacter(rightTargetPosition, 1, () => isMoving = false); // 1: Sa� y�n
    }

    public void WalkLeft()
    {
        if (isMoving) return; // E�er �u anda hareket ediyorsa yeni hareket ba�latma

        isMoving = true;
        MoveCharacter(leftTargetPosition, -1, () => isMoving = false); // -1: Sol y�n
    }

    private void MoveCharacter(Vector2 targetPosition, int direction, System.Action onComplete = null)
    {
        characterImage.rectTransform.DOAnchorPos(targetPosition, moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                characterImage.rectTransform.DOAnchorPosY(startPosition.y, 0.2f).SetEase(Ease.OutQuad); // Y eksenini s�f�rla
                onComplete?.Invoke(); // Callback �a��r (hareket tamamland�)
            });

        // Yukar�-a�a�� sal�n�m efekti
        characterImage.rectTransform.DOAnchorPosY(startPosition.y + bobbingAmount, bobbingDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(Mathf.CeilToInt(moveDuration / bobbingDuration), LoopType.Yoyo);

        // Karakteri sa�a/sola �evirmek i�in scale ayarla
        characterImage.rectTransform.localScale = new Vector3(direction, 1, 1);
    }

    private void StopAllTweens()
    {
        characterImage.rectTransform.DOKill(); // T�m hareketleri iptal et
    }
}
