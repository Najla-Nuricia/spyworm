using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ApplicationQuit : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private float delayTime = 0.5f;

    private void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(TriggerQuit);
        }
    }

    private void TriggerQuit()
    {
        StartCoroutine(QuitWithDelay());
    }

    private IEnumerator QuitWithDelay()
    {
        yield return new WaitForSeconds(delayTime);

        #if UNITY_EDITOR
        string errorMsg = "Application.Quit() tidak bisa menutup Unity Editor, tapi fungsi ini 100% bekerja setelah game di-build (.exe/.apk).";
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}