using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

#if UNITY_EDITOR
using UnityEditor;
#endif


/* 
 * Suggestion: If you can, preload the tables you gonna use so the first time doen't have a load delay
 * 
 * 
 */

namespace LocalizedAudio
{

    [RequireComponent(typeof(AudioSource), typeof(LocalizeAudioClipEvent))]
    public class LocalizedAudioController : MonoBehaviour
    {
        public UnityEvent OnStartAudio;
        [Space]
        public UnityEvent OnEndAudio;
        [Space]
        public UnityEvent<AudioClip> OnAudioChanged; // On language changed?
        [Space]
        public UnityEvent OnPreloadedAudio;

        LocalizeAudioClipEvent _LocalizeAudioClipEvent;
        AudioSource _AudioSource;

        Coroutine _AudioCoroutine;
        Coroutine _PreloadCoroutine;
        Coroutine _FadeCoroutine;
        UnityAction<AudioClip> _OnLanguageChange;

        #region Singleton
        private static LocalizedAudioController _Instance = null;
        public static LocalizedAudioController Instance
        {
            get
            {
                return _Instance;
            }
        }
        #endregion

        private void Awake()
        {
            CheckSingleton();

            Init();
        }

        #region Controls
        public static Coroutine Preload(LocalizedAudioClip localizedAudioClip)
        {
            if (Instance == null)
            {
                Debug.LogWarning($"No {typeof(LocalizedAudioController)} on the scene ");
                return null;
            }

            return Instance.PreloadAudio(localizedAudioClip);

        }

        public Coroutine PreloadAudio(LocalizedAudioClip localizedAudioClip)
        {
            if (_PreloadCoroutine != null)
            {
                StopCoroutine(_PreloadCoroutine);
                _PreloadCoroutine = null;
                Debug.LogWarning("Localized Audio: Another preload was on course.");
            }

            _PreloadCoroutine = StartCoroutine(PreloadAudioCoroutine(localizedAudioClip));

            return _PreloadCoroutine;
        }

        private IEnumerator PreloadAudioCoroutine(LocalizedAudioClip localizedAudioClip)
        {
            if (localizedAudioClip == null || localizedAudioClip.IsEmpty)
            {
                Debug.LogWarning($"Empty <b>LocalizedAudioAsset</b> variable.");
                _PreloadCoroutine = null;
                yield break;
            }

            _LocalizeAudioClipEvent.AssetReference = localizedAudioClip;


            // CurrentLoadingOperationHandle.Status --> None
            yield return new WaitUntil(() => localizedAudioClip.CurrentLoadingOperationHandle.IsDone);
            // CurrentLoadingOperationHandle.Status --> Success

            // TODO: Make it async to improve the performance
            AssetTable assetTable = LocalizationSettings.AssetDatabase.GetTable(localizedAudioClip.TableReference);
            SharedTableData.SharedTableEntry sharedTableEntry = assetTable.SharedData.GetEntryFromReference(localizedAudioClip.TableEntryReference);
            string entryName = sharedTableEntry != null ? sharedTableEntry.Key : null;
            string tableCollectionName = localizedAudioClip.TableReference.TableCollectionName;



            try
            {
                if (localizedAudioClip.CurrentLoadingOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    if (sharedTableEntry == null)
                    {
                        Debug.LogWarning($"Missing reference to the <b>TableEntry</b> in the table \"<b>{tableCollectionName}</b>\".\n<b><i>You deleted the TableEntry</i></b>");
                    }
                    else
                    {
                        // AudioClip result = localizedDefaultAsset.CurrentLoadingOperationHandle.Result;
                        OnPreloadedAudio.Invoke();
                    }

                }
                else
                {
                    Debug.LogWarning($"Error to load the captions in the table \"<b>{tableCollectionName}</b>\" in the entry \"<b>{entryName}</b>\" in the language \"<b>{LocalizationSettings.SelectedLocale}</b>\".");
                }
            }
            catch
            {
                Debug.LogWarning($"Seems that entry \"<b>{entryName}</b>\" in your table \"<b>{tableCollectionName}</b>\" have some missing/broken references or the asset it's not Addressable.\n<b><i>You may need to make the asset Addressable or redo the table.</i></b>");

                _LocalizeAudioClipEvent.AssetReference = null;
                yield break;

            }
            finally
            {
#if UNITY_EDITOR
                EditorUtility.SetDirty(_LocalizeAudioClipEvent); // Refresh the inspector
#endif

                _PreloadCoroutine = null;
            }

            // Debug.Log("Preloaded");
        }

        public static void Play(LocalizedAudioClip localizedAudioClip)
        {
            if (Instance == null)
            {
                Debug.LogWarning($"No {typeof(LocalizedAudioController)} on the scene ");
                return;
            }

            Instance.PlayAudio(localizedAudioClip);
        }

        public void PlayAudio(LocalizedAudioClip localizedAudioClip)
        {
            StopLocalizedAudio();

            _AudioCoroutine = StartCoroutine(PlayAudioCoroutine(localizedAudioClip));
        }

        private IEnumerator PlayAudioCoroutine(LocalizedAudioClip localizedAudioClip)
        {
            if (localizedAudioClip == null || localizedAudioClip.IsEmpty)
            {
                Debug.LogWarning($"Empty <b>LocalizedAudioClip</b> variable.\n<i>Stopping</i>");
                _AudioCoroutine = null;
                yield break;
            }

            yield return PreloadAudio(localizedAudioClip);

            if (_LocalizeAudioClipEvent.AssetReference == null || _LocalizeAudioClipEvent.AssetReference.IsEmpty)
            {
                Debug.LogWarning($"Something went wrong during the load of <b>{localizedAudioClip}</b>.\n<i>Stopping</i>");
                _AudioCoroutine = null;
                yield break;
            }

            OnStartAudio.Invoke();

            // Init setup
            _LocalizeAudioClipEvent.OnUpdateAsset.AddListener(_OnLanguageChange);

            AudioClip audioClip = _LocalizeAudioClipEvent.AssetReference.LoadAsset();

            if (audioClip == null)
            {
                Debug.LogWarning($"The language {LocalizationSettings.SelectedLocale} doesn't have an <b>AudioClip</b> asigned.\n<i>Stopping</i>");
                _AudioCoroutine = null;
                yield break;
            }

            ChangeAudioClip(audioClip);

            yield return new WaitWhile(() => _AudioSource.time == 0f);

            yield return new WaitWhile(() => _AudioSource.isPlaying);

            OnEndAudio.Invoke();

            ResetVariables();
            _AudioCoroutine = null;
        }

        public static void Stop()
        {
            if (Instance == null)
            {
                Debug.LogWarning($"No {typeof(LocalizedAudioController)} on the scene ");
                return;
            }

            Instance.StopLocalizedAudio();
        }

        public void StopLocalizedAudio()
        {
            if (_AudioCoroutine != null)
            {
                Debug.LogWarning("Stopping the localized audio.");

                StopCoroutine(_AudioCoroutine);
                _AudioCoroutine = null;
            }

            ResetVariables();
            OnEndAudio.Invoke();
        }

        private void ResetVariables()
        {
            _LocalizeAudioClipEvent.OnUpdateAsset.RemoveListener(_OnLanguageChange);

            if (_FadeCoroutine != null)
            {
                StopCoroutine(_FadeCoroutine);
                _FadeCoroutine = null;
            }

            _AudioSource.Stop();
            _AudioSource.clip = null;
        }
        #endregion

        #region Update events

        private void ChangeAudioClip(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning($"The language {LocalizationSettings.SelectedLocale} doesn't have an <b>AudioClip</b> asigned");
                return;
            }

            if (_AudioSource.isPlaying && _AudioSource.time < clip.length)
            {
                float currentTime = _AudioSource.time;
                _AudioSource.clip = clip;
                _AudioSource.time = currentTime;
                _AudioSource.Play();
            }
            else
            {
                _AudioSource.clip = clip;
                _AudioSource.time = 0; // Important to do the time reset
                _AudioSource.Play();
            }

            OnAudioChanged.Invoke(clip);
        }
        #endregion

        #region Init
        private void Init()
        {
            _LocalizeAudioClipEvent = GetComponent<LocalizeAudioClipEvent>();
            _AudioSource = GetComponent<AudioSource>();

            /* Audio config recommendations */
            if (_AudioSource.loop)
            {
                Debug.Log($"<i>Disabling the <b>Loop</b> on the AudioSource of {_AudioSource.name}.</i>");
                _AudioSource.loop = false;
            }

            if (_AudioSource.playOnAwake)
            {
                Debug.Log($"<i>Disabling the <b>PlayOnAwake</b> on the AudioSource of {_AudioSource.name}.</i>");
                _AudioSource.playOnAwake = false;
            }

            /* Automatization */
            if (_LocalizeAudioClipEvent.OnUpdateAsset.GetPersistentEventCount() != 0)
            {
                Debug.LogWarning("Be careful, you added a event to OnUpdateAsset. We ALREADY update the AudioClip via script.\nCount: " + _LocalizeAudioClipEvent.OnUpdateAsset.GetPersistentEventCount());
            }

            /* Update events */
            _OnLanguageChange = ChangeAudioClip;
        }

        private void CheckSingleton()
        {
            // Check if this instance is a duplicated
            if (_Instance != null && _Instance != this)
            {
                Debug.LogWarning($"Multiple instances of <b>{GetType().Name}</b>\nDestroying the component in <b>{name}</b>.");
                Destroy(this);
                return;
            }

            // Set this instance as the selected
            _Instance = this;
        }
        #endregion

    }
}
