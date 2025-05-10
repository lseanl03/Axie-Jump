using System.Collections;
using UnityEngine;
using TMPro;
using LocalizedAudio;

namespace Fenikkel.LocalizedAudio.Example
{
    public class TimeText : MonoBehaviour
    {
        TextMeshProUGUI _Text;
        AudioSource _AudioSource;
        Coroutine _Coroutine;

        void Start()
        {
            _AudioSource = LocalizedAudioController.Instance.GetComponent<AudioSource>();
            _Text = GetComponent<TextMeshProUGUI>();

            LocalizedAudioController.Instance.OnStartAudio.AddListener(StartTimmer);
            LocalizedAudioController.Instance.OnEndAudio.AddListener(StopTimmer);
            LocalizedAudioController.Instance.OnPreloadedAudio.AddListener(OnPreload);
        }


        private void StartTimmer()
        {
            if (_Coroutine != null)
            {
                Debug.Log("Already working");
                return;
            }

            _Coroutine = StartCoroutine(AudioTimmerCoroutine());
        }

        private void StopTimmer()
        {
            if (_Coroutine != null)
            {
                StopCoroutine(_Coroutine);
                _Coroutine = null;
                _Text.text = "Stopped";
            }
        }

        IEnumerator AudioTimmerCoroutine()
        {

            while (true)
            {
                _Text.text = $"Playing: {_AudioSource.time.ToString("F2")}";
                yield return null;
            }

        }


        private void OnPreload()
        {

            _Text.text = "Preloaded";

        }
    }
}
