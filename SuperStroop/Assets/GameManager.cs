using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static instance accessible from anywhere
    public static GameManager Instance;
    // Simple singleton setup
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        Instance = this;
    }

    // We only need the InterfaceManager to see this
    [HideInInspector]
    public int currentRound = 0;
    int rounds;
    int maxRounds;
    // The amount of time spent in the current round
    float roundTime;
    // The time at which the round started
    float roundStartTime;

    float accuracy;
    int currentStreak;
    int bestStreak;

    List<float> responseTimes = new List<float>();

    public void StartGame(int rounds)
    {
        // Remember this in case we want to restart
        this.rounds = rounds;

        // Ensure we start at 0
        currentRound = 0;
        maxRounds = rounds;

        InterfaceManager.Instance.menuScreen.Navigate(InterfaceManager.Instance.gameScreen);

        InterfaceManager.Instance.RandomizeColorWord();

        roundTime = 0;
        roundStartTime = Time.realtimeSinceStartup;
    }
    public void RestartGame()
    {
        InterfaceManager.Instance.endScreen.Hide();
        StartGame(rounds);
    }
    public void EndGame()
    {
        GetResults();
        InterfaceManager.current.Navigate(InterfaceManager.Instance.endScreen);

        accuracy = 0;
        responseTimes.Clear();
        currentStreak = 0;
        bestStreak = 0;
    }
    public void CheckColor(int index)
    {
        // Correct!
        if (InterfaceManager.Instance.colorIndex == index)
        {
            accuracy++;
            currentStreak++;
            bestStreak = currentStreak;
        }
        else
        {
            currentStreak = 0;
        }

        // Calcualte time for this round
        roundTime = Time.realtimeSinceStartup - roundStartTime;
        if (roundTime <= 1 && roundTime > 0.05f)
        {
            responseTimes.Add(roundTime);
        }

        if (currentRound < maxRounds - 1)
        {
            currentRound++;
            InterfaceManager.Instance.RandomizeColorWord();

            roundTime = 0;
            roundStartTime = Time.realtimeSinceStartup;
        }
        else
        {
            EndGame();
        }
    }
    void GetResults()
    {
        float sum = 0;
        // Best response time
        float bestResponse = 1;

        foreach(float responseTime in responseTimes)
        {
            sum += responseTime;

            if (bestResponse > responseTime)
                bestResponse = responseTime;
        }

        // Average response time
        float avgResponse = (sum / responseTimes.Count) * 1000;
        // Accuracy
        if (accuracy != 0)
            accuracy = (accuracy / maxRounds) * 100;
        // Final score
        int score = (1000 - (int)avgResponse) * (int)(accuracy / 10);

        InterfaceManager.Instance.SetResultElements(avgResponse, bestResponse * 1000, accuracy, bestStreak, score);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}