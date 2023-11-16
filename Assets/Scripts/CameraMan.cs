using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Morpho
{

    public class CameraMan : MonoBehaviour
    {
        private Camera cam;
        private Vector3 currentPosition;
        private bool isShaking;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            currentPosition = cam.transform.position;
        }

        public void SetVignetteColor(Color color)
        {
            if (FindObjectOfType<Volume>().profile.TryGet(out Vignette vignette)) // for e.g set vignette intensity to .4f
            {
                vignette.color.value = color;
            }
        }

        public void PlayCameraShake(float intensity, float duration, float hapticStrength = 1)
        {
            if(!isShaking)
            {
                isShaking = true;
                StartCoroutine(CameraShake(intensity, Mathf.Abs(duration), hapticStrength));
            }
        }

        private IEnumerator CameraShake(float intensity, float duration, float hapticStrength)
        {
            float time = 0;
            while (time < duration)
            {
                yield return new WaitForEndOfFrame();
                cam.transform.position += new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), 0) * (duration / (time + 0.0001f));
                yield return new WaitForEndOfFrame();
                cam.transform.position = currentPosition;
                time += Time.deltaTime * 2;
                HapticFeedbackController.SetVibration(duration / (time + 0.0001f) * hapticStrength);
            }

            cam.transform.position = currentPosition;
            HapticFeedbackController.SetVibration(0);
            isShaking = false;
        }
    }
}