using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OTHER TEST");
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        Debug.Log("TEST");
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name is not set in SceneTransitionTrigger!");
        }
    }
}
