# Localized Audio

Multi-lingual audio system.

<p align="center">
  <img src="https://github.com/Fenikkel/LocalizedCaptions/assets/41298931/94209c2f-d76d-4819-8a1b-1b5790a72095" alt="Art image"/>
</p>



&nbsp;
## Usage
1. Make sure you have a LocalizationSettings.
2. Create an AssetTableCollection for your audios.
3. Create an entry to drag your audio clips on it.
4. Drag and drop the LocalizedAudio.prefab to any scene.
5. To play the audio, you can make an static call from any script: 

```
    LocalizedAudioClip localizedAudioClip;
    LocalizedAudioController.Play(localizedAudioClip);
```
6. Preload your LocalizedAudioClip for skip loading deleays:
```
    LocalizedAudioClip localizedAudioClip;
    LocalizedAudioController.Preload(localizedAudioClip);
```
7. Stop any active audio with:
```
    LocalizedAudioController.Stop();
```

&nbsp;
## Installation
Add the custom package to your project via:
- [Unity Asset Store](https://u3d.as/3cdP)

or

- Package Manager -> + -> Add package from git URL -> https://github.com/Fenikkel/LocalizedAudio.git


<p align="center">
    <img src="https://github.com/Fenikkel/SimpleTween/assets/41298931/0f447b8c-85ca-4205-9915-ca7203dc4741" alt="Instructions" height="384">
</p>


&nbsp;
## Technical details

- [Supported audio files](https://docs.unity3d.com/2023.3/Documentation/Manual/AudioFiles.html)

&nbsp;
## Compatibility
- Unity Version: 2019.4 (LTS) or higher
- Any pipeline (Build-in, URP, HDRP, etc)

&nbsp;
## Dependencies
- [Localization](https://docs.unity3d.com/Packages/com.unity.localization@1.4/manual/index.html)
- [Addressables](https://docs.unity3d.com/Packages/com.unity.addressables@2.0/manual/index.html)
- [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)

&nbsp;
## Support
⭐ Star if you like it  
❤️️ Follow me for more
