using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    
    public string startLevel;

    IEnumerator Wait() {
        yield return new WaitForSeconds(10);
        
    }

    public void NewGame() {
        Application.LoadLevel(startLevel);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
