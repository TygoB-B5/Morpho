using UnityEngine;

namespace Morpho
{
    public class Radio : MonoBehaviour
    {
        public AudioSource AudioSource;
        public AudioLowPassFilter LowPass;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            AudioSource.Play();
        }

        public void EnableMutedSound(bool enabled)
        {
            LowPass.cutoffFrequency = enabled ? 1000 : 6000;
        }
    }
}