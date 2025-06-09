using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel : PanelBase
{
    [SerializeField] private UserRank currentUserRank;
    [SerializeField] private GameObject userRankHolder;
    [SerializeField] private Button closeButton;

    private List<UserRank> userRankList = new List<UserRank>();

    protected override void Awake()
    {
        base.Awake();
        OnGetUserRankListInit();
        closeButton.onClick.AddListener(HidePanel);
    }
    public UserRank CurrentUserRank
    {
        get { return currentUserRank; }
        set { currentUserRank = value; }
    }
    public List<UserRank> UserRankList
    {
        get { return userRankList; }
        set { userRankList = value; }
    }
    private void OnGetUserRankListInit()
    {
        for (int i = 0; i < userRankHolder.transform.childCount; i++)
        {
            userRankList.Add(userRankHolder.transform.GetChild(i)
                .GetComponent<UserRank>());
        }
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
        PlayFabManager.Instance.GetLeaderboard();
        PlayFabManager.Instance.GetCurrentUserRank();
    }
}
