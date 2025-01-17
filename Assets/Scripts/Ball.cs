using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField] private List<GameObject> labirentler; // Tüm labirentlerin listesi
    [SerializeField] private GameObject ballPrefab; // Top objesinin prefab'ý
    [SerializeField] private TextMeshProUGUI level; // Level yazýsý
    private int currentIndex = 0; // Geçerli labirent indeksi

    private void Start()
    {
      
        // Ýlk labirenti aktif yap ve diðerlerini pasif yap
        SetActiveLabirent(currentIndex);
        UpdateLevelText();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ýlgili finish etiketi tetiklendiðinde bir sonraki labirente geçiþ yap
        if (other.CompareTag($"Finish{currentIndex + 1}"))
        {
            TransitionToNextLabirent();
        }
    }

    private void TransitionToNextLabirent()
    {
        if (currentIndex < labirentler.Count - 1)
        {
            // Mevcut labirenti pasif yap
            labirentler[currentIndex].SetActive(false);

            // Bir sonraki labirenti aktif yap
            currentIndex++;
            SetActiveLabirent(currentIndex);

            // Topu yeni labirente taþý
            RespawnBall();

            // Level metnini güncelle
            UpdateLevelText();

            
        }
       
    }

    private void SetActiveLabirent(int index)
    {
        // Labirentlerin geçiþini kontrol et
        for (int i = 0; i < labirentler.Count; i++)
        {
            labirentler[i].SetActive(i == index); // Sadece istenen labirenti aktif yap
        }
    }

    
    private void RespawnBall()
    {
        // Sabit pozisyonu belirle (Labirentin lokasyonu + offset)
        Vector3 localPosition = new Vector3(-1.93f, -0.07f, -1.86f);

        // Mevcut topu bul ve sil
        GameObject oldBall = GameObject.FindWithTag("Ball");
        if (oldBall != null)
        {
            Destroy(oldBall); // Mevcut topu yok et
        }

        // Yeni topu mevcut labirentin child objesi olarak oluþtur
        GameObject currentLabirent = labirentler[currentIndex]; // Mevcut labirent
        GameObject newBall = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);

        // Yeni topu labirentin içine yerleþtir
        newBall.transform.SetParent(currentLabirent.transform); // Labirent child olarak ata
        newBall.transform.localPosition = localPosition; // Sabit konumu ayarla (labirente göre)
        newBall.transform.localRotation = Quaternion.identity; // Dönüklüðü sýfýrla

        // Yeni topun tag'ini ayarla
        newBall.tag = "Ball";

        // Yeni topun Ball scriptini al ve gerekli bilgileri aktar
        Ball newBallScript = newBall.GetComponent<Ball>();
        if (newBallScript != null)
        {
            newBallScript.level = level;
            newBallScript.labirentler = labirentler; // Labirent listesini aktar
            newBallScript.currentIndex = currentIndex; // Geçerli indexi ayarla
        }

       
    }




    private void UpdateLevelText()
    {
        if (level != null)
        {
            level.text = (currentIndex + 1).ToString(); // Level metnini güncelle
        }
        else
        {
            Debug.LogWarning("Level TextMeshPro objesi atanmadý.");
        }
    }
}
