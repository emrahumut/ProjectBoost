using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float DelayTime = 1f;
    [SerializeField] AudioClip SuccessClip;
    [SerializeField] AudioClip FailureClip;
    AudioSource audioSource;

    bool isTransitioning = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "Obstacle":
                StartCrashSequence();
                break;
            case "Finish":
                StartNextSceneSequence();
                break;
            default:
                break;
        }

    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        // TODO add sfx upon crash
        // TODO add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(FailureClip);
        Invoke("ReloadLevel", DelayTime);
    }

    private void StartNextSceneSequence()
    {
        isTransitioning = true;
        // TODO add sfx upon crash
        // TODO add particle effect upon crash

        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(SuccessClip);
        Invoke("LoadNextScene", DelayTime);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
