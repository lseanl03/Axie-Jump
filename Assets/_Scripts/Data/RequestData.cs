using NUnit.Framework;
using System;
using UnityEngine;

public class RequestData : MonoBehaviour
{
}

[Serializable]
public class Request
{
    public string requestText;
    public string noteText;
    public int pointsLost;
}
