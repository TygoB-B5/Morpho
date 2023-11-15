using UnityEngine;

namespace Morpho
{
    public class Radio : MonoBehaviour
    {
        public AudioSource AudioSource;
        public AudioSource SoundSource;
        public AudioLowPassFilter LowPass;
        public AudioClip Jump, Push, Walk, Thwomp, AbilityChange;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            AudioSource.Play();
        }

        public void EnableMutedSound(bool enabled)
        {
            LowPass.cutoffFrequency = enabled ? 1000 : 6000;
        }

        public void PlayJump()
        {
            SoundSource.PlayOneShot(Jump);
        }

        public void PlayPush()
        {
            SoundSource.PlayOneShot(Push);
        }

        public void PlayWalk()
        {
            SoundSource.PlayOneShot(Walk);
        }

        public void PlayThwomp()
        {
            SoundSource.PlayOneShot(Thwomp);
        }

        public void PlayAbilityChange()
        {
            SoundSource.PlayOneShot(AbilityChange);
        }
    }
}