using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject leaderboardMenu;
    [SerializeField] private UserRank currentUserRank;
    [SerializeField] private GameObject userRankHolder;

    private List<UserRank> userRankList = new List<UserRank>();
    private void Awake()
    {
        OnGetUserRankListInit();
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
    public void ShowLeaderboardPanel()
    {
        leaderboardMenu.SetActive(true);
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);

        PlayFabManager.Instance.GetLeaderboard();
        PlayFabManager.Instance.GetCurrentUserRank();
        AudioManager.Instance.PlayButtonClick();


    }
    public void HideCharacterPanel()
    {
        leaderboardMenu.SetActive(false);
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);

        AudioManager.Instance.PlayCloseClick();

    }
}
