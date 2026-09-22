using UnityEngine;

public class LevelLogger : MonoBehaviour
{
    static LevelLogger instance;

    public static LevelLogger Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("LevelLogger");
                instance = go.AddComponent<LevelLogger>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    int currentLevel = -1;
    int attempts;
    float levelStartTime;

    public void LevelStarted(int levelIndex)
    {
        if (levelIndex != currentLevel)
        {
            currentLevel = levelIndex;
            attempts = 0;
            levelStartTime = Time.time;
        }
        attempts++;

        Debug.Log($"[LevelLogger] Level {currentLevel} started - Attempt #{attempts}");
    }

    public void LevelCompleted(int lifeRemaining)
    {
        float elapsed = Time.time - levelStartTime;
        Debug.Log($"[LevelLogger] Level {currentLevel} completed - Attempts: {attempts}, Time: {elapsed:F2}s, Life remaining: {lifeRemaining}");

        currentLevel = -1;
    }

    public void LevelFailed(int lifeRemaining)
    {
        float elapsed = Time.time - levelStartTime;
        Debug.Log($"[LevelLogger] Level {currentLevel} failed - Attempts: {attempts}, Time: {elapsed:F2}s, Life remaining: {lifeRemaining}");
    }
}
