using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;

public class PlayServices : MonoBehaviour
{
    public static PlayServices instanceObj;
    int pts = 0;
    public Text scoreText;
    public Text saveText;
    public static PlayGamesPlatform platformGame;
    private bool isSavingData = false;

    // Start is called before the first frame update
    void Start()
    {     
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            // requests the email address of the player be available.
            // Will bring up a prompt for consent.
            .RequestEmail()
            // requests a server auth code be generated so it can be passed to an
            //  associated back end server application and exchanged for an OAuth token.
            //.RequestServerAuthCode(false)
            // requests an ID token be generated.  This OAuth token can be used to
            //  identify the player to other services such as Firebase.
            //.RequestIdToken()
            .Build();

            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.DebugLogEnabled = true;

            PlayGamesPlatform.Activate();
    }

    public void DisplayAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void UnlockAchievements()
    {
        // unlock achievement (achievement ID "Cfjewijawiu_QA")
        Social.ReportProgress(GPGSIds.achievement_achievementforfirstplace, 100.0f, (bool success) => {
            // handle success or failure
        });
    }


    public void IncrementAchievements()
    {
        // increment achievement (achievement ID "Cfjewijawiu_QA") by 5 steps
        PlayGamesPlatform.Instance.IncrementAchievement(
            GPGSIds.achievement_mile_fifty_club, 10, (bool success) => {
            // handle success or failure

        });
    }

    public void SignInToServices()
    {
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure

            if(success == true)
            {
                Debug.Log("Successfully logged in to Google Play Services");
            }
            else
            {
                Debug.Log("Login Failed");
            }
        });
    }

    public void AddPoints()
    {
        pts = pts + 10;
        scoreText.text = "Score: " + pts.ToString();
    }

    public void AddLeaderboardScore()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(pts, GPGSIds.leaderboard_pointsleaderboard, (bool success) => {
                // handle success or failure

                if (success)
                {
                    pts = 0;
                    scoreText.text = "Score: " + pts.ToString();
                }
                else
                {
                    scoreText.text = "Score could not be updated!";
                    Debug.Log("Failed to update score");
                }
            });
        }
    }

    public void playStats()
    {
        ((PlayGamesLocalUser)Social.localUser).GetStats((rc, stats) =>
        {
            // -1 means cached stats, 0 is succeess
            // see  CommonStatusCodes for all values.
            if (rc <= 0 && stats.HasDaysSinceLastPlayed())
            {
                Debug.Log("It has been " + stats.DaysSinceLastPlayed + " days");
            }
        });
    }
    
    public void DisplayLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_pointsleaderboard);
        //Social.ShowLeaderboardUI();
    }

    public void LogOut()
    {
        PlayGamesPlatform.Instance.SignOut();

        if (Social.localUser.authenticated == false)
        {
            Debug.Log("User has signed out of Google Play Services");
        }
    }

   public void ShowSelectUI()
    {
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }


    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
            Debug.Log("Saved game has been selected");

        }
        else
        {
            // handle cancel or error
            Debug.LogError("Saved game cannot be selected");

        }
    }

   public void OpenSavedGame(bool gameIsSaved)
    {
        if (Social.localUser.authenticated)
        {
            isSavingData = gameIsSaved;
        
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution("PointsGame", DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        }
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        { //Write
            if (isSavingData)
            {
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

                byte[] data = ASCIIEncoding.ASCII.GetBytes(GetSavingString());

                SavedGameMetadataUpdate updateSave = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Saved at: " + DateTime.Now.ToString()).Build();

                savedGameClient.CommitUpdate(game, updateSave, data, SaveUpdate);

                saveText.text = "Game data written! " + status.ToString();

                // handle reading or writing of saved game.
                Debug.Log("Saved game written on cloud");

            }
            else
            {
                // Read
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

                savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);

                saveText.text = "Game data read on cloud! " + status.ToString();

                Debug.Log("Saved game read on cloud");

            }
        }
    }

    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            saveText.text = "Data saved successfully";
            Debug.Log("Data saved successfully: " + status);
        }
        else
        {
            saveText.text = status.ToString() + " failed to save";
            Debug.Log("Data failed to save: " + status);
        }

    }

   public void LoadStringSave(string savingData)
    {
        savingData = scoreText.text;
    }

   public void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string savedGameData = ASCIIEncoding.ASCII.GetString(data);

            LoadStringSave(savedGameData);

            saveText.text = "Data read successfully! " + savedGameData;

            // handle processing the byte array data
            Debug.Log("Game data is being read");
        }
        else
        {
            saveText.text = "Game data read failed! " + status.ToString();
            // handle error
            Debug.LogError("Game data cannot be read!");
        }
    }

   public void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime) {
        
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            saveText.text = "Save successful";
            // handle reading or writing of saved game.
            Debug.Log("Save successful");
        }
        else
        {
            saveText.text = "Saving failed! " + status.ToString();
            // handle error
            Debug.LogError("Save failed! Could not save game!");
        }
    }

    public Texture2D getScreenshot()
    {
        // Create a 2D texture that is 1024x700 pixels from which the PNG will be
        // extracted
        Texture2D screenShot = new Texture2D(1024, 700);

        // Takes the screenshot from top left hand corner of screen and maps to top
        // left hand corner of screenShot texture
        screenShot.ReadPixels(
            new Rect(0, 0, Screen.width, (Screen.width / 1024) * 700), 0, 0);
        return screenShot;
    }

   public void DeleteGameData(string filename)
    {
        // Open the file to get the metadata.
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, DeleteSavedGame);
    }

    public void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.Delete(game);
        }
        else
        {
            // handle error
        }
    }

    private string GetSavingString()
    {
        string saveData = "";

        saveData += scoreText.ToString();

        return saveData;
    }

    private void Awake()
    {
        instanceObj = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(pts >= 100)
        {
            UnlockAchievements();
        }
    }
}
