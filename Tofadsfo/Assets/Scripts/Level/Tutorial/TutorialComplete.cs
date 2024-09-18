using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialComplete : MonoBehaviour
{
    public void LoadNextevel()
    {
        GameSaver.Setlevel(SceneManager.GetActiveScene().buildIndex + 1);
        GameSaver.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
