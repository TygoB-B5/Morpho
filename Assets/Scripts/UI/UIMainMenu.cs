using UnityEngine;
using UnityEngine.UI;

namespace Morpho
{
    public class UIMainMenu : MonoBehaviour
    {
        public Button PlayButton;
        public Button CreditsButton;
        public Button ExitButton;

        private void Start()
        {
            GameManager.InitIfNotAlraedy();
            PlayButton.onClick.AddListener(() => GameManager.StartGame());
            CreditsButton.onClick.AddListener(() => GameManager.ToCredits());
            ExitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}