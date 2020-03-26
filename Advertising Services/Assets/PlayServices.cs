﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class PlayServices : MonoBehaviour
{
    public static PlayServices instanceObj;
    int pts = 0;
    public Text scoreText;
    public string leaderboardGame;

    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
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

        PlayGamesPlatform.Activate();

        SignInToServices();
    }

    public void DisplayAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void UnlockAchievements(string achID)
    {
        // unlock achievement (achievement ID "Cfjewijawiu_QA")
        Social.ReportProgress(achID, 100.0f, (bool success) => {
            // handle success or 


        });
    }

    public void IncrementAchievements(string achID, int stepsToIncrement)
    {
        // increment achievement (achievement ID "Cfjewijawiu_QA") by 5 steps
        PlayGamesPlatform.Instance.IncrementAchievement(
            achID, stepsToIncrement, (bool success) => {
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
        scoreText.text = "Score: " + pts;
    }

    public void AddLeaderboardScore()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(pts, leaderboardGame, (bool success) => {
                // handle success or failure

            if (success)
            {
                scoreText.text = "Score: " + pts;
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
        Social.ShowLeaderboardUI();
    }

    public void LogOut()
    {
        PlayGamesPlatform.Instance.SignOut();

        if (Social.localUser.authenticated == false)
        {
            Debug.Log("User has signed out of Google Play Services");
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
        
    }
}
