using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField] private List<GameObject> labirentler; // T�m labirentlerin listesi
    [SerializeField] private GameObject ballPrefab; // Top objesinin prefab'�
    [SerializeField] private TextMeshProUGUI level; // Level yaz�s�
    private int currentIndex = 0; // Ge�erli labirent indeksi

    private void Start()
    {
      
        // �lk labirenti aktif yap ve di�erlerini pasif yap
        SetActiveLabirent(currentIndex);
        UpdateLevelText();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �lgili finish etiketi tetiklendi�inde bir sonraki labirente ge�i� yap
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

            // Topu yeni labirente ta��
            RespawnBall();

            // Level metnini g�ncelle
            UpdateLevelText();

            
        }
       
    }

    private void SetActiveLabirent(int index)
    {
        // Labirentlerin ge�i�ini kontrol et
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

        // Yeni topu mevcut labirentin child objesi olarak olu�tur
        GameObject currentLabirent = labirentler[currentIndex]; // Mevcut labirent
        GameObject newBall = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);

        // Yeni topu labirentin i�ine yerle�tir
        newBall.transform.SetParent(currentLabirent.transform); // Labirent child olarak ata
        newBall.transform.localPosition = localPosition; // Sabit konumu ayarla (labirente g�re)
        newBall.transform.localRotation = Quaternion.identity; // D�n�kl��� s�f�rla

        // Yeni topun tag'ini ayarla
        newBall.tag = "Ball";

        // Yeni topun Ball scriptini al ve gerekli bilgileri aktar
        Ball newBallScript = newBall.GetComponent<Ball>();
        if (newBallScript != null)
        {
            newBallScript.level = level;
            newBallScript.labirentler = labirentler; // Labirent listesini aktar
            newBallScript.currentIndex = currentIndex; // Ge�erli indexi ayarla
        }

       
    }




    private void UpdateLevelText()
    {
        if (level != null)
        {
            level.text = (currentIndex + 1).ToString(); // Level metnini g�ncelle
        }
        else
        {
            Debug.LogWarning("Level TextMeshPro objesi atanmad�.");
        }
    }
}
