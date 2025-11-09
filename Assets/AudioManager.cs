using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer mixer;

    [Header("Optional UI Sliders (only needed in Menu)")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider bulletSlider;


    void Start()
    {
        // Load saved volume values (or default to 1)
        float master = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float bullet = PlayerPrefs.GetFloat("BulletVolume", 1f);

        ApplyVolume("MasterVolume", master);
        ApplyVolume("MusicVolume", music);
        ApplyVolume("BulletVolume", bullet);

        // Update sliders if assigned
        if (masterSlider != null) masterSlider.value = master;
        if (musicSlider != null) musicSlider.value = music;
        if (bulletSlider != null) bulletSlider.value = bullet;
    }

    // Safe method to apply a value to mixer
    private void ApplyVolume(string parameter, float value)
    {
        // Clamp to avoid log(0)
        value = Mathf.Clamp(value, 0.0001f, 1f);
        mixer.SetFloat(parameter, Mathf.Log10(value) * 20);
    }

    // Called by sliders
    public void SetMasterVolume(float value)
    {
        ApplyVolume("MasterVolume", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        ApplyVolume("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetBulletVolume(float value)
    {
        ApplyVolume("BulletVolume", value);
        PlayerPrefs.SetFloat("BulletVolume", value);
    }
}
