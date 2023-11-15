using UnityEngine;
using UnityEngine.UI;

namespace Morpho
{
    public class UIPauseMenu : MonoBehaviour
    {
        public Button ReturnButton;
        public Button RetryButton;
        public Button MainMenuButton;

        private void Start()
        {
            ReturnButton.onClick.AddListener(() =>
            {
                Show(false);
            });

            RetryButton.onClick.AddListener(() =>
            {
                Show(false);
                GameManager.ResetLevel();
            });

            MainMenuButton.onClick.AddListener(() =>
            {
                Show(false);
                GameManager.ToMainMenu();
            });
        }

        public void Show(bool show)
        {
            GameManager.Player.EnableController(!show);
            GameManager.GetRadio().EnableMutedSound(show);
            gameObject.SetActive(show);
        }
    }
}