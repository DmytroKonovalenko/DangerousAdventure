using UnityEngine;
using UnityEngine.SceneManagement;
namespace ScriptsPlayer
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject optionsWindow;

        private void Start()
        {
            GameController.Instance.AudioManager.PlayMusic(true);
            Time.timeScale = 1f;
        }

        public void ButtonPlay()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void ButtonOptions()
        {

        }
        public void ShowWindow(GameObject window)
        {
            window.GetComponent<Animator>().SetBool("Open", true);
        }
        public void HideWindow(GameObject window)
        {
            window.GetComponent<Animator>().SetBool("Open", false);
        }

        public void ButtonExit()
        {
            Application.Quit();
        }
    }
}