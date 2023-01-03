using System;
using System.Collections;
using UnityEngine;

namespace ScriptableOutline.Scripts
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class OutlineController : MonoBehaviour
    {
        [Range(0f, 1f)] public float animationDuration = 0.25f;
        [Range(0f, 20f)] public float maxWidth = 5f;

        public bool isVisible;
        public Color outlineColor = Color.white;

        private bool _lastVisibleState;
        private Color _lastOutlineColor;

        private OutlineInstance _outlineInstance;

        private void Awake()
        {
            _outlineInstance = gameObject.GetComponent<OutlineInstance>();
        }

        private void Update()
        {
            if (GetMaxOutlineWidth() != maxWidth)
            {
                SetMaxOutlineWidth(maxWidth);
            }

            if (_lastVisibleState != isVisible)
            {
                StartCoroutine(UpdateState(isVisible));
                _lastVisibleState = isVisible;
            }

            if (_lastOutlineColor != outlineColor)
            {
                StartCoroutine(UpdateColor(outlineColor));
                _lastOutlineColor = outlineColor;
            }
        }

        private IEnumerator UpdateState(bool isActive)
        {
            float duration = animationDuration;
            var timer = 0f;

            while (timer < duration)
            {
                var startPos = GetOutlineValue();
                float step = maxWidth * Time.deltaTime / duration;

                SetOutlineValue(isActive ? startPos + step : startPos - step);

                timer += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator UpdateColor(Color color)
        {
            float duration = animationDuration;
            var timer = 0f;

            while (timer < duration)
            {
                var startColor = GetOutlineColor();

                SetOutlineColor(Color.Lerp(startColor, color, Time.deltaTime / (duration - timer)));

                timer += Time.deltaTime;
                yield return null;
            }
        }


        private void SetMaxOutlineWidth(float width)
        {
            _outlineInstance.outlineFillMaterial.SetFloat(Constants.OutlineMaxWidthName, width);
        }

        private float GetMaxOutlineWidth()
        {
            return _outlineInstance.outlineFillMaterial.GetFloat(Constants.OutlineMaxWidthName);
        }

        private void SetOutlineColor(Color color)
        {
            _outlineInstance.outlineFillMaterial.SetColor(Constants.OutlineColorName, color);
        }

        private Color GetOutlineColor()
        {
            var value = _outlineInstance.outlineFillMaterial
                .GetColor(Constants.OutlineColorName);

            return value;
        }

        private void SetOutlineValue(float value)
        {
            _outlineInstance.outlineFillMaterial.SetFloat(Constants.OutlineWidthName, value);
        }

        private float GetOutlineValue()
        {
            var value = _outlineInstance.outlineFillMaterial
                .GetFloat(Constants.OutlineWidthName);
            return Math.Clamp(value, 0, maxWidth);
        }
    }
}