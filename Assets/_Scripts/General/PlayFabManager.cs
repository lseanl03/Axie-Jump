using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayFabManager : Singleton<PlayFabManager>
{
    [SerializeField] private string userName;
    [SerializeField] private string titleId = "B827D";
    [SerializeField] private string leaderboardName = "HighScore";
    private void Start()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        Login();
    }
    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }
    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLogin, OnError);
    }

    private void OnLogin(LoginResult result)
    {
        Debug.Log($"Login successful with {result.PlayFabId}");

        if(result.InfoResultPayload != null)
        {
            userName = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (String.IsNullOrEmpty(userName))
        {
            UIManager.Instance.UICanvas.EnterUserNamePanel.ShowEnterUserNamePanel();
        }
        else
        {
            UIManager.Instance.UICanvas.EnterUserNamePanel.HideEnterUserNamePanel();
        }
    }

    public void SubmitUserNameName(string userName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSubmitUserName, OnError);
    }

    private void OnSubmitUserName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Create UserName:" + result.DisplayName);
    }

    public void SubmitHighScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = leaderboardName,
                    Value = score
                }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSubmitHighScore, OnError);
    }

    private void OnSubmitHighScore(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("High score submitted successfully.");
    }

    private void LoginWithCustomID(string customId)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLogin, OnError);
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardName,
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnError);
    }

    private void OnGetLeaderboard(GetLeaderboardResult result)
    {
        List<UserRank> userRankList = UIManager.Instance.UICanvas.LeaderboardPanel.UserRankList;
        var currentUserRank = UIManager.Instance.UICanvas.LeaderboardPanel.CurrentUserRank;
        for (int i = 0; i < result.Leaderboard.Count; i++)
        {
            var userRank = result.Leaderboard[i];
            if (userRank.DisplayName == userName)
            {
                currentUserRank.SetUserRank(
                    userRank.Position + 1, 
                    userRank.DisplayName, 
                    userRank.StatValue.ToString());
            }
            userRankList[i].SetUserRank(
                userRank.Position + 1, 
                userRank.DisplayName, 
                userRank.StatValue.ToString());
        }
    }
}
