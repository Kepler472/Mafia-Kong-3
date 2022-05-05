using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour, ISaveable
{

    private int lives;
    private int level;
    public int score;
    private int highscore = 0;
    public bool levelFinished = false;

    public Transform respawnPoint;
    public GameObject playerPrefab;

    public static Manager instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        highscore = PlayerPrefs.GetInt("highscore");
    }

    void Update(){
        if(score > highscore){
            highscore = score;
            StoreHighScore();
        }
    }

    public void Respawn(){
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }

    public void NewGame()
    {
        lives = 3;
        score = 0;
        level = 1;

        // LoadJsonData(this);
        LoadLevel(level);
    }

    private void LoadLevel(int index)
    {
        levelFinished = true;
        level = index;

        Camera camera = Camera.main;

        if (camera != null)
        {
            camera.cullingMask = 0;
        }

        Invoke(nameof(LoadScene), 1f);
    }

    private void LoadScene()
    {
        levelFinished = false;
        SceneManager.LoadScene(level);
    }

    public void LevelComplete()
    {
        score += 1000;
        Debug.Log("Lives " + lives);

        // int nextLevel = level + 1;
        // if (nextLevel < SceneManager.sceneCountInBuildSettings)
        // {
        //     LoadLevel(nextLevel);
        // }
        // else
        // {
            LoadLevel(1);
        // }
    }

    public void LevelFailed(bool hitTonny = false, bool hasFallen = false)
    {
        lives--;
        HeartManager.instance.UpdateHeartBar();
        if (hasFallen){
            Respawn();
        }

        if (hitTonny)
        {
            lives = 0;
        }

        if (lives <= 0)
        {
            NewGame();
        }
        else
        {
            // SaveJsonData(this);
        }
    }
    public int GetLives()
    {
        return lives;
    }
    public void SetLives(int lives)
    {
        this.lives = lives;
    }

    private static void SaveJsonData(Manager manager)
    {
        SaveData sd = new SaveData();
        manager.PopulateSaveData(sd);

        if (FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }

    private static void LoadJsonData(Manager manager)
    {
        if (FileManager.LoadFromFile("SaveData.dat", out string json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            manager.LoadFromSaveData(sd);
            Debug.Log("Load successful");
        }
    }

    public void PopulateSaveData(SaveData SaveData)
    {
        SaveData.score = score;
        SaveData.lives = lives;
        SaveData.level = level;
    }
    public void LoadFromSaveData(SaveData SaveData)
    {
        score = SaveData.score;
        lives = SaveData.lives;
        level = SaveData.level;
    }

    public void StoreHighScore(){
        PlayerPrefs.SetInt("highscore", highscore);
        print (PlayerPrefs.GetInt ("highscore", highscore));
        PlayerPrefs.Save();
    }

}
