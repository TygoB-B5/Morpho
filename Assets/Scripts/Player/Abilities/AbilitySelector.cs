using UnityEngine;

namespace Morpho
{
    public class AbilitySelector : MonoBehaviour
    {
        public string Ability;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Ability == "DefaultPlayerController") GameManager.Player.SetController(new DefaultPlayerController(GameManager.Player));
            if (Ability == "SmallPlayerControlller") GameManager.Player.SetController(new SmallPlayerControlller(GameManager.Player));
        }
    }
}