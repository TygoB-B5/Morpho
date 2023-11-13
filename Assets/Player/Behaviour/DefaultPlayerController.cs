using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Morpho
{
    public class DefaultPlayerController : PlayerController
    {
        public DefaultPlayerController(Player player)
            : base(player)
        {
            Parent.transform.localScale = Vector3.one * 
        }

        public override void StartController()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateController()
        {
            throw new System.NotImplementedException();
        }

        public 
    }
}