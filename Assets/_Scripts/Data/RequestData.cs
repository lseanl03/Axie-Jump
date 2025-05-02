using NUnit.Framework;
using System;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "RequestData", menuName = "ScriptableObjects/RequestData", order = 1)]
public class RequestData : ScriptableObject
{
    public Request[] bearMomRequests;
    public Request[] bearDadRequests;
    public Request GetBearMomRequest()
    {
        int randomIndex = UnityEngine.Random.Range(0, bearMomRequests.Length);
        GameManager.Instance.CurrentRequest = bearMomRequests[randomIndex];
        return bearMomRequests[randomIndex];
    }

    public Request GetBearDadRequest()
    {
        int randomIndex = UnityEngine.Random.Range(0, bearDadRequests.Length);
        GameManager.Instance.CurrentRequest = bearDadRequests[randomIndex];
        return bearDadRequests[randomIndex];
    }

    public bool AgreeRequestWithType(Request request, Transform transform = null)
    {
        var gameManager = GameManager.Instance;
        switch (request.requestType)
        {
            case RequestType.PrimogemsForFoods:
                if(gameManager.Points < request.lostValue)
                {
                    GamePlayUIManager.Instance.GameCanvas.RequestPanel
                        .ShowNotificationText("Không đủ điểm để đổi!");
                    return false;
                }
                gameManager.Primogems += request.getValue;
                gameManager.OnGetPrimogem(transform, request.getValue);

                gameManager.UpdatePoint(-request.lostValue);
                gameManager.OnGetPoint(transform, -request.lostValue);

                break;
        }
        return true;
    }
}

[Serializable]
public class Request
{
    [TextArea] public string requestText;
    public RequestType requestType;
    public int getValue;
    public int lostValue;
}


