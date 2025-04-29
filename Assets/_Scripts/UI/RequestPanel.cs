using TMPro;
using UnityEngine;
using static EventManager;

public class RequestPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI requestText;
    [SerializeField] private TextMeshProUGUI noteText;

    private void OnEnable()
    {
        EventManager.onOpenRequest += OnOpenRequest;
    }

    private void OnDisable()
    {
        EventManager.onOpenRequest -= OnOpenRequest;

    }

    public void Reject()
    {

    }

    public void Agree()
    {

    }

    private void OnOpenRequest()
    {

    }
}
