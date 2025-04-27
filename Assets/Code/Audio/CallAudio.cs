using UnityEngine;

public class CallAudio : MonoBehaviour {
    AudioManager audioManager;
    
    public void Start() {
        audioManager = AudioManager.Instance;
    }

    public void PlayBGM(string audioName) {
        audioManager.PlayBGM(audioName);
    }

    public void PlaySFX(string soundName) {
        audioManager.PlaySFX(soundName);
    }

    public void PlayUI(string uiName) {
        audioManager.PlayUI(uiName);
    }
}