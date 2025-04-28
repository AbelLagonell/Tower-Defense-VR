using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Util {
    public class ChangeToMain : MonoBehaviour{
        private void Start() {
            StartCoroutine(nameof(BackToMenu));
        }

        IEnumerator BackToMenu() {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("MainMenu");
        }
        
    }
    
}