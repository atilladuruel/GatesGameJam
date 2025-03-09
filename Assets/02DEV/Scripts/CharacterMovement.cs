using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    public Image characterImage; // Karakterin UI Image bile�eni
    public float moveDuration = 2f; // Hareket s�resi
    public RectTransform targetTransform; // Hedef nokta
    public float bobbingAmount = 10f; // Yukar�-a�a�� sal�n�m miktar�
    public float bobbingDuration = 0.5f; // Yukar�-a�a�� sal�n�m s�resi
    public float initialYPosition = 0f; // Ba�lang�� y pozisyonu
    
    private Vector2 startPosition;
    private bool isMoving = false; // Hareket kontrol�

    void Start()
    {
        RectTransform rectTransform = characterImage.rectTransform;
        startPosition = rectTransform.anchoredPosition;
        initialYPosition = rectTransform.anchoredPosition.y;
        // Oyunun ba��nda sa�a y�r�t
        WalkRight();
    }

    public void WalkRight(Action onComplete = null)
    {
        if (isMoving) return; // E�er �u anda hareket ediyorsa yeni hareket ba�latma
        characterImage.rectTransform.localScale = new Vector3(1, 1, 1);

        isMoving = true;
        Action PassOnComplete = () => isMoving = false; // Hareket tamamland�ktan sonra isMoving'i false yap
        PassOnComplete += () => onComplete?.Invoke(); // Callback �a��r (hareket tamamland�)
        MoveCharacter(targetTransform.anchoredPosition,  PassOnComplete); // 1: Sa� y�n
    }

    public void WalkLeft(Action onComplete = null)
    {
        if (isMoving) return; // E�er �u anda hareket ediyorsa yeni hareket ba�latma
        characterImage.rectTransform.localScale = new Vector3(-1, 1, 1);
        isMoving = true;
        Action PassOnComplete = () => isMoving = false; // Hareket tamamland�ktan sonra isMoving'i false yap
        PassOnComplete += () => characterImage.rectTransform.anchoredPosition = startPosition; // Karakteri ba�lang�� noktas�na geri getir
        PassOnComplete += () => onComplete?.Invoke(); // Callback �a��r (hareket tamamland�)
        
        MoveCharacter(startPosition,  PassOnComplete); // -1: Sol y�n
    }

    private void MoveCharacter(Vector2 targetPosition, Action onComplete = null)
    {
        var rectTransform = characterImage.rectTransform;
        rectTransform.DOAnchorPosY(initialYPosition, .2f)
            .SetEase(Ease.OutQuad).OnComplete(() =>
            {
                rectTransform.DOAnchorPos(targetPosition, moveDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        characterImage.rectTransform.DOAnchorPosY(targetPosition.y, 0.2f).SetEase(Ease.OutQuad); // Y eksenini s�f�rla
                        onComplete?.Invoke(); // Callback �a��r (hareket tamamland�)
                    });

                // Yukar�-a�a�� sal�n�m efekti
                rectTransform.DOAnchorPosY(startPosition.y + bobbingAmount, bobbingDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(Mathf.CeilToInt(moveDuration / bobbingDuration), LoopType.Yoyo);
                
            }); // Y eksenini s�f�rla
    }

    private void StopAllTweens()
    {
        characterImage.rectTransform.DOKill(); // T�m hareketleri iptal et
    }
}