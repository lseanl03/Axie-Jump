using DG.Tweening.Core.Easing;
using NUnit.Framework;
using System;
using UnityEditor.PackageManager.Requests;
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
                if(NotEnoughPoints(request.lostValue)) return false;

                gameManager.Primogems += request.getValue;
                gameManager.OnGetPrimogem(transform, request.getValue);

                gameManager.UpdatePoint(-request.lostValue);
                gameManager.OnGetPoint(transform, -request.lostValue);
                break;
            case RequestType.FoodsForPrimogems:
                if(NotEnoughPrimogems(request.lostValue)) return false;

                gameManager.Points += request.getValue;
                gameManager.OnGetPoint(transform, request.getValue);

                gameManager.UpdatePrimogem(-request.lostValue);
                gameManager.OnGetPrimogem(transform, -request.lostValue);
                break;

            case RequestType.PointBuffForPrimogems:
                if (NotEnoughPrimogems(request.lostValue)) return false;

                PoolManager.Instance.GetObjFromTrunk(
                    PoolType.Buff_Point, transform.position, transform.parent);
                break;

            case RequestType.PrimogemBuffForFoods:
                if (NotEnoughPoints(request.lostValue)) return false;

                PoolManager.Instance.GetObjFromTrunk(
                    PoolType.Buff_Primogem, transform.position, transform.parent);
                break;
            case RequestType.SpeedBuffForFoods:
                if (NotEnoughPoints(request.lostValue)) return false;

                PoolManager.Instance.GetObjFromTrunk(
                    PoolType.Buff_Speed, transform.position, transform.parent);
                break;
        }
        return true;
    }

    private bool NotEnoughPoints(int target)
    {
        var gameManager = GameManager.Instance;

        if (gameManager.Points < target)
        {
            GamePlayUIManager.Instance.GameCanvas.RequestPanel
                .ShowNotificationText("Không đủ điểm để đổi!");
            return true;
        }
        return false;
    }

    private bool NotEnoughPrimogems(int target)
    {
        var gameManager = GameManager.Instance;
        if (gameManager.Primogems < target)
        {
            GamePlayUIManager.Instance.GameCanvas.RequestPanel
                .ShowNotificationText("Không đủ nguyên thạch để đổi!");
            return true;
        }
        return false;
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


