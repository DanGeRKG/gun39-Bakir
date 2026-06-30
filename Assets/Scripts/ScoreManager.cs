using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;

    private int totalScore = 0, pinsDownInCurrentThrow = 0, currentThrow = 1, firstThrowPins = 0, bonusThrowsCount = 0;
    private Pin[] allPins;
    private BallSwipe ballScript;

    void Awake() { Instance = this; }

    void Start()
    {
        allPins = FindObjectsOfType<Pin>();
        ballScript = FindObjectOfType<BallSwipe>();
    }

    public void AddPinDown() { pinsDownInCurrentThrow++; }

    public void EndThrow()
    {
        int pinsHit = pinsDownInCurrentThrow;
        string message = "";

        if (bonusThrowsCount > 0) { totalScore += pinsHit; bonusThrowsCount--; }
        totalScore += pinsHit;

        if (pinsHit == 10 && currentThrow == 1) { message = "ÑÒÐÀÉÊ!"; bonusThrowsCount = 2; EndFrame(); }
        else if (currentThrow == 2 && (firstThrowPins + pinsHit == 10)) { message = "ÑÏÀÐ!"; bonusThrowsCount = 1; EndFrame(); }
        else if (currentThrow == 1) { firstThrowPins = pinsHit; currentThrow = 2; pinsDownInCurrentThrow = 0; foreach (Pin p in allPins) p.HideFallenPin(); ballScript.ResetBall(); }
        else { EndFrame(); }

        if (scoreText != null)
        {
            scoreText.text = (message != "" ? message + "\n" : "") + "Î÷êè: " + totalScore;
            if (message != "") StartCoroutine(ClearMessage());
        }
    }

    IEnumerator ClearMessage() { yield return new WaitForSeconds(2.0f); scoreText.text = "Î÷êè: " + totalScore; }

    void EndFrame()
    {
        currentThrow = 1; pinsDownInCurrentThrow = 0; firstThrowPins = 0;
        foreach (Pin p in allPins) p.ResetPin();
        ballScript.ResetBall();
    }
}