using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class PortalManager : MonoBehaviour
{
    public GameObject sceneSelectionPanel;
    public Button levelButton; 
    public Button level1Button; 
    public Button level2Button; 
    void Start()
    {
        sceneSelectionPanel.SetActive(false);

        levelButton.onClick.AddListener(() => LoadScene("Poziom"));
        level1Button.onClick.AddListener(() => LoadScene("Poziom 1"));
        level2Button.onClick.AddListener(() => LoadScene("Poziom 2"));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            sceneSelectionPanel.SetActive(true); 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            sceneSelectionPanel.SetActive(false); 
        }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
