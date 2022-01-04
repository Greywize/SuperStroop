using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    // Static instance accessible from anywhere
    public static InterfaceManager Instance;
    // Simple singleton setup
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public static CanvasInterface current;
    public CanvasInterface menuScreen;
    public CanvasInterface gameScreen;
    public CanvasInterface endScreen;

    public List<string> words;
    public List<Color> colors;

    // The lists must match in order and size
    [SerializeField] TMP_Text colorWord;
    [SerializeField] Image progressBar;

    [SerializeField] TMP_Text avgResponseText;
    [SerializeField] TMP_Text bestResponseText;

    [SerializeField] TMP_Text accuracyText;
    [SerializeField] TMP_Text streakText;

    [SerializeField] TMP_Text scoreText;

    // Keeps track of the correct color
    [HideInInspector]
    public int colorIndex;

    public void RandomizeColorWord()
    {
        // Get a random index of the color list
        colorIndex = Random.Range(0, colors.Count);
        colorWord.color = colors[colorIndex];

        // Get a word index not equal to the color index
        int wordIndex = Random.Range(0, words.Count);
        while (wordIndex == colorIndex)
        {
            wordIndex = Random.Range(0, words.Count);
        }
        colorWord.text = words[wordIndex];
    }
    public void SetResultElements(float avgResponse, float bestResponse, float accuracy, int streak, int score)
    {
        avgResponseText.text = avgResponse.ToString("F0") + "ms";
        bestResponseText.text = bestResponse.ToString("F0") + "ms";

        accuracyText.text = accuracy + "%";
        streakText.text = streak.ToString();

        scoreText.text = score.ToString();
    }
    public void ReturnToMenu()
    {
        current.Navigate(menuScreen);
    }
}