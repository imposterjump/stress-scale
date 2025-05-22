using System.Collections.Generic;
using System.IO;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
    private string filePath;

    // Player data
    public string Username { get; private set; }
    public int Coins { get; private set; }
    public int Hints { get; private set; }
    public int Eliminations { get; private set; }
    public int Skips { get; private set; }
    public List<int> HighScores { get; private set; } = new List<int>();

    void Awake()
    {
        // singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // PUBLIC API ---------------------------------------------------

    public void SetUsername(string name)
    {
        Username = name;
        SaveData();
    }

    public bool TryBuyHint(int amount = 1)
    {
        int cost = 3 * amount;
        if (Coins < cost) return false;
        Coins -= cost;
        Hints += amount;
        SaveData();
        return true;
    }

    public bool TryBuySkip(int amount = 1)
    {
        int cost = 2 * amount;
        if (Coins < cost) return false;
        Coins -= cost;
        Skips += amount;
        SaveData();
        return true;
    }

    public bool TryBuyElimination(int amount = 1)
    {
        int cost = 1 * amount;
        if (Coins < cost) return false;
        Coins -= cost;
        Eliminations += amount;
        SaveData();
        return true;
    }

    public bool UseHint()
    {
        if (Hints <= 0) return false;
        Hints--;
        SaveData();
        return true;
    }

    public bool UseSkip()
    {
        if (Skips <= 0) return false;
        Skips--;
        SaveData();
        return true;
    }

    public bool UseElimination()
    {
        if (Eliminations <= 0) return false;
        Eliminations--;
        SaveData();
        return true;
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        SaveData();
    }

    public void AddHighScore(int score)
    {
        HighScores.Add(score);
        HighScores.Sort((a, b) => b.CompareTo(a)); // highest first
        if (HighScores.Count > 10) HighScores.RemoveAt(HighScores.Count - 1);
        SaveData();
    }

    // DATA I/O -----------------------------------------------------

    private void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var pd = JsonUtility.FromJson<PlayerData>(json);
            Username     = pd.username;
            Coins        = pd.coins;
            Hints        = pd.hints;
            Eliminations = pd.eliminations;
            Skips        = pd.skips;
            HighScores   = new List<int>(pd.highScores ?? new int[0]);
        }
        else
        {
            // first run defaults
            Username = "Player";
            Coins = 40;
            Hints = Eliminations = Skips = 0;
            HighScores.Clear();
            SaveData();
        }
    }

    private void SaveData()
    {
        var pd = new PlayerData {
            username     = this.Username,
            coins        = this.Coins,
            hints        = this.Hints,
            eliminations = this.Eliminations,
            skips        = this.Skips,
            highScores   = this.HighScores.ToArray()
        };
        string json = JsonUtility.ToJson(pd, true);
        File.WriteAllText(filePath, json);
    }

    [System.Serializable]
    private class PlayerData
    {
        public string username;
        public int coins;
        public int hints;
        public int eliminations;
        public int skips;
        public int[] highScores;
    }
}