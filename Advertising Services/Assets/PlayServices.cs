﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System;
using GooglePlayGames.BasicApi.SavedGame;

public class PlayServices : MonoBehaviour
{
    public static PlayServices instanceObj;
    int pts = 0;
    public Text scoreText;
    public static PlayGamesPlatform platformGame;

    // Start is called before the first frame update
    void Start()
    {
      if (platformGame == null)
       {       
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            // enables saving game progress.
            //.EnableSavedGames()
            // requests the email address of the player be available.
            // Will bring up a prompt for consent.
            .RequestEmail()
            // requests a server auth code be generated so it can be passed to an
            //  associated back end server application and exchanged for an OAuth token.
            .RequestServerAuthCode(false)
            // requests an ID token be generated.  This OAuth token can be used to
            //  identify the player to other services such as Firebase.
            .RequestIdToken()
            .Build();

            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.DebugLogEnabled = true;

            platformGame = PlayGamesPlatform.Activate();
       }
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


    public void IncrementAchievements(string achID, int stepsForIncrementing)
    {
        // increment achievement (achievement ID "Cfjewijawiu_QA") by 5 steps
        PlayGamesPlatform.Instance.IncrementAchievement(
            GPGSIds.achievement_achievementforfirstplace, stepsForIncrementing, (bool success) => {
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
                    Debug.Log("Failed to update score");
                }
            });
        }
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

    void ShowSelectUI()
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


        }
        else
        {
            // handle cancel or error


        }
    }

    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.


        }
        else
        {
            // handle error


        }
    }

    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data



        }
        else
        {
            // handle error



        }
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
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

    void DeleteGameData(string filename)
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
