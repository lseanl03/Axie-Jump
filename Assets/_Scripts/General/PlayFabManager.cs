using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class PlayFabManager : Singleton<PlayFabManager>
{
    [SerializeField] private string userName;
    [SerializeField] private string leaderboardName = "HighScore";
    [SerializeField] private string characterIndexKey = "CharacterIndex";
    [SerializeField] private string primogemKey = "PR";
    private Coroutine loadedDataCoroutine;
    
    private void Start()
    {
        Login();
    }
    #region Action
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
    public void SubmitUserNameName(string userName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSubmitUserName, OnError);
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
    public void LoginWithCustomID(string customId)
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
    private void GetHighScore()
    {
        var request = new GetPlayerStatisticsRequest
        {
            StatisticNames = new List<string> { leaderboardName }
        };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetHighScore, OnError);
    }
    public void SetCharacterIndex(int index)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { characterIndexKey, index.ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSaveCharacterIndex, OnError);
    }
    public void AddPrimogem(int amount)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = primogemKey,
            Amount = amount
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddPrimogem, OnError);
    }
    public void SubPrimogem(int amount)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = primogemKey,
            Amount = amount
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubPrimogem, OnError);
    }
    public void GetPrimogem()
    {
        var request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, OnGetPrimogem, OnError);
    }
    public void GetCharacterIndex()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetCharacterIndex, OnError);
    }
    #endregion




    #region CallBack
    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }
    private void OnLogin(LoginResult result)
    {
        Debug.Log($"Login: {result.InfoResultPayload.PlayerProfile.DisplayName}");
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
        if(loadedDataCoroutine != null) StopCoroutine(loadedDataCoroutine);
        loadedDataCoroutine = StartCoroutine(LoadedDataCoroutine());
    }
    private void OnSubmitUserName(UpdateUserTitleDisplayNameResult result)
    {
    }
    private void OnSubmitHighScore(UpdatePlayerStatisticsResult result)
    {
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
    private void OnGetHighScore(GetPlayerStatisticsResult result)
    {
        foreach (var statistic in result.Statistics)
        {
            if (statistic.StatisticName == leaderboardName)
            {
                GameManager.Instance.HighScore = statistic.Value;
                UIManager.Instance.UICanvas.MainMenuPanel.SetHighScore(statistic.Value);
            }
        }
    }
    private void OnSaveCharacterIndex(UpdateUserDataResult result)
    {
    }
    private void OnGetCharacterIndex(GetUserDataResult result)
    {
        int index = 0;
        if (result.Data != null && result.Data.ContainsKey(characterIndexKey))
        {
            int.TryParse(result.Data[characterIndexKey].Value, out index);
        }
        CharacterManager.Instance.GetCurrentCharacter(index);
    }
    private void OnSubPrimogem(ModifyUserVirtualCurrencyResult result)
    {
    }
    private void OnAddPrimogem(ModifyUserVirtualCurrencyResult result)
    {
    }
    private void OnGetPrimogem(GetUserInventoryResult result)
    {
        foreach (var currency in result.VirtualCurrency)
        {
            GameManager.Instance.PrimogemOwn = currency.Value;
            UIManager.Instance.UICanvas.MainMenuPanel.SetPrimogem(currency.Value);
        }
    }
    #endregion

    private IEnumerator LoadedDataCoroutine()
    {
        if (userName != null)
        {
            GetCharacterIndex();
            yield return new WaitForSeconds(0.5f);
            GetPrimogem();
            yield return new WaitForSeconds(0.5f);
            GetHighScore();
            yield return new WaitForSeconds(0.5f);
            GetLeaderboard();
            UIManager.Instance.UICanvas.LevelTransiton.Open();
        }
    }
}
