using TMPro;
using UnityEngine;

public class UserRank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public void SetUserRank(int rank, string userName, string highScore)
    {
        rankText.text = rank.ToString();
        userNameText.text = userName;
        highScoreText.text = highScore;
    }

}
