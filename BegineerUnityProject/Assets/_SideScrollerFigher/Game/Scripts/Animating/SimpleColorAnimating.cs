using System;
using System.Collections;
using UnityEngine;

namespace _SideScrollerFigher.Game.Scripts.Animating
{
    public class SimpleColorAnimating : MonoBehaviour
    {
        public Color fromColor;
        public Color toColor;

        public Renderer currentRenderer;
        
        private void Update()
        {
            var lerpedColor = Color.Lerp(fromColor, toColor, Mathf.PingPong(Time.time, 1));
            currentRenderer.material.color = lerpedColor;
        }
    }
}