using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    public static TimeManager singleton;
    [SerializeField] float slowTimeSpeed = .5f;
    [SerializeField] float slowAudioPitch = .75f;
    [SerializeField] float physicsUpdateTime = 0.01667f;
    [SerializeField] float slowChromaticAberration = .5f;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Volume globalVolume;
    [SerializeField] ChromaticAberration chromaticAberration;

    void Awake(){
        if(singleton == null){
            singleton = this;
        }else{
            Destroy(this.gameObject);
        }
        globalVolume.profile.TryGet(out chromaticAberration);
    }
    void Start()
    {
        Time.fixedDeltaTime = physicsUpdateTime;
        Time.timeScale = 1;

    }

    public void SlowTime(){
        Time.timeScale = slowTimeSpeed;
        audioMixer.SetFloat("MasterPitch", slowAudioPitch);
        Time.fixedDeltaTime = Time.timeScale * physicsUpdateTime;
        chromaticAberration.intensity.value = slowChromaticAberration;
    }

    // public void PauseTime(){
    //     Time.timeScale = 0.0001f;
    // }

    public void ResumeTime(){
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * physicsUpdateTime;
        audioMixer.SetFloat("MasterPitch",1f);
        chromaticAberration.intensity.value = 0;
    }


}
