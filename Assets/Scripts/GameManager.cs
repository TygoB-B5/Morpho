using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Morpho
{
    public class HapticFeedbackController
    {
        // Import the Windows API functions for XInput
        [DllImport("xinput1_3.dll")]
        public static extern uint XInputSetState(uint dwUserIndex, ref XINPUT_VIBRATION pVibration);

        // Structure to represent the vibration parameters
        [StructLayout(LayoutKind.Explicit)]
        public struct XINPUT_VIBRATION
        {
            [FieldOffset(0)] public ushort wLeftMotorSpeed;
            [FieldOffset(2)] public ushort wRightMotorSpeed;
        }

        // Player index (controller number)
        private const uint playerIndex = 0; // Change this based on the player/controller you want to affect

        public static void SetVibration(float percentage)
        {
            XINPUT_VIBRATION vibration = new XINPUT_VIBRATION();
            vibration.wLeftMotorSpeed = (ushort)(percentage * 65535.0f); // Max intensity for left motor
            vibration.wRightMotorSpeed = (ushort)(percentage * 65535.0f); // Max intensity for right motor

            XInputSetState(playerIndex, ref vibration);
        }
    }


    public class InputController
    {
        private readonly float Deadzone = 0.1f;

        public Vector2 GetMovementInput()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Mathf.Abs(input.x) < Deadzone)
                input.x = 0;
            if (Mathf.Abs(input.y) < Deadzone)
                input.y = 0;

            return input;
        }

        public bool IsJumping()
        {
            return Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump");
        }

        public bool IsMenuButton()
        {
            return Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause");
        }
    }

    public class GameManager : PersistentSingleton<GameManager>
    {
        public static InputController InputManager => Instance.inputManager;

        public static Player Player
        {
            get
            {
                if (Instance.player == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>("Player"));
                    DontDestroyOnLoad(go);
                    Instance.player = go.GetComponent<Player>();
                }

                return Instance.player;
            }
        }

        private InputController inputManager;
        private GameObject pauseScreen;
        private Player player;
        private Radio radio;

        public static void InitIfNotAlraedy()
        {
            Instance.enabled = true;
        }

        protected override void Awake()
        {
            base.Awake();

            inputManager = new InputController();
            GetRadio().EnableMutedSound(true);
            GetPauseScreen().Show(false);
        }

        private void Update()
        {
            if(inputManager.IsMenuButton() && SceneManager.GetActiveScene().name != "MainMenu")
            {
                GetPauseScreen().Show(!GetPauseScreen().gameObject.activeSelf);
            }
        }

        public static void StartGame() => Instance.StartGameImpl();
        private void StartGameImpl()
        {
            GetRadio().EnableMutedSound(false);
            SceneManager.LoadScene("Level0");
        }

        public static void ProgressToNextLevel() => Instance.ProgressToNextLevelImpl();
        private void ProgressToNextLevelImpl()
        {
            string scene = SceneManager.GetActiveScene().name.Substring(5);

            if (int.TryParse(scene, out int num))
            {
                if(!ToLevel(num + 1))
                {
                    FinishGame();
                }
            }
            else
            {
                Debug.LogError("Something went wrong with the Level picking");
            }

        }

        public static void FinishGame() => Instance.FinishGameImpl();
        private void FinishGameImpl()
        {
            ToMainMenu();
            GetRadio().EnableMutedSound(true);
        }

        public static void ToMainMenu() => Instance.ToMainMenuImpl();
        private void ToMainMenuImpl()
        {
            Player.transform.position = Vector3.one * -1000;
            SceneManager.LoadScene("MainMenu");
        }

        public static void ToCredits() => Instance.ToCreditsImpl();
        private void ToCreditsImpl()
        {
            SceneManager.LoadScene("Credits");
        }

        public static bool ToLevel(int index) => Instance.ToLevelImpl(index);
        private bool ToLevelImpl(int index)
        {
            bool valid = Application.CanStreamedLevelBeLoaded($"Level{index}");

            if (valid)
            {
                SceneManager.LoadScene($"Level{index}");
            }

            return valid;
        }

        public static void ResetLevel() => Instance.ResetLevelImpl();
        private void ResetLevelImpl()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        public static UIPauseMenu GetPauseScreen() => Instance.EnablePauseScreenImpl();
        private UIPauseMenu EnablePauseScreenImpl()
        {
            if(pauseScreen == null)
            {
                pauseScreen = Instantiate(Resources.Load<GameObject>("PauseScreen"));
                DontDestroyOnLoad(pauseScreen);
            }

            return pauseScreen.GetComponent<UIPauseMenu>();
        }

        public static CameraMan GetCameraMan()
        {
            if(Camera.main.TryGetComponent<CameraMan>(out CameraMan man))
            {
                return man;
            }

            return Camera.main.AddComponent<CameraMan>();
        }


        public static Radio GetRadio() => Instance.GetRadioImpl();
        private Radio GetRadioImpl()
        {
            if (radio == null)
            {
                radio = Instantiate(Resources.Load<GameObject>("Radio")).GetComponent<Radio>();
                DontDestroyOnLoad(radio.gameObject);
            }

            return radio;
        }
    }
}