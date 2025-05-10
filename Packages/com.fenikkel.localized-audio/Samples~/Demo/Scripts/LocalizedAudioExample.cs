using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using LocalizedAudio;

namespace Fenikkel.LocalizedAudio.Example
{
    public class LocalizedAudioExample : MonoBehaviour
    {
        const string TABLE_COLLECTION_NAME = "AudioDemoTable";
        const string TABLE_ENTRY_NAME = "DemoEntry";

        [SerializeField] LocalizedAudioClip _LocalizedAudioClip;

        [SerializeField] AudioClip _EnglishClip;
        [SerializeField] AudioClip _FrenchClip;
        [SerializeField] AudioClip _SpanishClip;

        Coroutine _Coroutine;

        private void Awake()
        {
            InitAndSetupLocalization();
        }

        public void PreloadLocalizedAudioClip()
        {
            if (_LocalizedAudioClip.IsEmpty)
            {
                Debug.LogWarning("Please, create a Asset Table and asign an entry of a AudioClip in _LocalizedAudioClip");
            }

            LocalizedAudioController.Preload(_LocalizedAudioClip); // Or -> LocalizedAudioController.Instance.PreloadCaptions(_LocalizedTextAsset);
        }

        public void PlayLocalizedAudioClip()
        {
            if (_LocalizedAudioClip.IsEmpty)
            {
                Debug.LogWarning("Please, create a Asset Table and asign an entry of a AudioClip in _LocalizedAudioClip");
            }

            LocalizedAudioController.Play(_LocalizedAudioClip); // Or -> LocalizedAudioController.Instance.PlayCaptions(_LocalizedTextAsset);
        }

        public void StopLocalizedAudioClip()
        {
            LocalizedAudioController.Stop();
        }

        private void InitAndSetupLocalization()
        {
            // Localization settings
            LocalizationSettings localizationSettings = LocalizationUtilities.CreateLocalizationSettings("Assets/Settings/Localization", new SystemLanguage[] { SystemLanguage.Spanish, SystemLanguage.English, SystemLanguage.French });

            if (localizationSettings != null)
            {
                if (_Coroutine != null)
                {
                    StopCoroutine(_Coroutine);
                }

                _Coroutine = StartCoroutine(CreateTables(localizationSettings));
            }
        }

        IEnumerator CreateTables(LocalizationSettings localizationSettings)
        {
            yield return new WaitUntil(() => localizationSettings.GetInitializationOperation().IsDone);

            List<Locale> locales = localizationSettings.GetAvailableLocales().Locales;

            AssetTableCollection assetTableCollection = LocalizationUtilities.CreateAssetTableCollection(TABLE_COLLECTION_NAME, "AudioDemo", "Assets/Settings/Localization/Tables", locales);

            Dictionary<LocaleIdentifier, AudioClip> dictionary = new Dictionary<LocaleIdentifier, AudioClip>();
            dictionary.Add(new LocaleIdentifier(SystemLanguage.Spanish), _SpanishClip);
            dictionary.Add(new LocaleIdentifier(SystemLanguage.French), _FrenchClip);
            dictionary.Add(new LocaleIdentifier(SystemLanguage.English), _EnglishClip);

            bool success = LocalizationUtilities.CreateAssetTableEntry<AudioClip>(assetTableCollection, TABLE_ENTRY_NAME, dictionary);

            if (success)
            {
                _LocalizedAudioClip.SetReference(assetTableCollection.TableCollectionNameReference, TABLE_ENTRY_NAME);
                EditorUtility.SetDirty(this);
            }

            _Coroutine = null;
        }
    }
}
