using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour
{
    private string m_saveFilePath;
    public HighScoreData HighScoreData = new HighScoreData();

    void Awake()
    {
        m_saveFilePath = Path.Combine(Application.persistentDataPath, "highscores.js");
        LoadScores();
    }

    public void AddScore(string playerName, int score)
    {
        if (HighScoreData == null)
        {
            Debug.Log("HighScoreData is null");
        }

        // highscoreentries = null
        if (HighScoreData.HighScoreEntries == null)
        {
            Debug.Log("HighScoreData.HighScoreEntries is null"); 
        }
        
        HighScoreData.HighScoreEntries.Add(new HighScoreEntry(playerName, score));

        HighScoreData.HighScoreEntries.Sort((a, b) => b.score.CompareTo(a.score));

        if (HighScoreData.HighScoreEntries.Count > 10)
        {
            HighScoreData.HighScoreEntries = HighScoreData.HighScoreEntries.GetRange(0, 10);
        }

        SaveScores();
    }
    
    public List<HighScoreEntry> GetHighScores()
    {
        return HighScoreData.HighScoreEntries;
    }

    public void SaveScores()
    {
        string json = JsonUtility.ToJson(HighScoreData, true);
        File.WriteAllText(m_saveFilePath, json);
        Debug.Log("Scores saved to: " + m_saveFilePath);
    }

    private void LoadScores()
    {
        if (File.Exists(m_saveFilePath))
        {
            string json = File.ReadAllText(m_saveFilePath);
            HighScoreData = JsonUtility.FromJson<HighScoreData>(json);
            Debug.Log("Loaded high scores from file.");
        }
        else
        {
            HighScoreData = new HighScoreData();
            Debug.Log("No high score file found - starting new list.");
        }
    }
}