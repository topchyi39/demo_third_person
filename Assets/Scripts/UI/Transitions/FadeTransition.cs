using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Transitions
{
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
    public class FadeTransition : Transition
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private float inTime;
        [SerializeField] private float outTime;
        
        public override void Show()
        {
            SmoothFade(0f, 1f, inTime);
            canvasGroup.blocksRaycasts = true;
            canvas.enabled = true;
        }

        public override void Hide()
        {
            canvasGroup.blocksRaycasts = false;
            SmoothFade(1f, 0f, outTime);
            canvas.enabled = false;
        }
        

        public override async UniTask HideAsync()
        {
            var time = 0f;
            canvasGroup.blocksRaycasts = false;
            await SmoothFade(1f, 0f, outTime);
        }

        private async Task SmoothFade(float startValue, float endValue, float duration)
        {
            var time = 0f;
            while (time <= outTime)
            {
                var value = Mathf.Lerp(startValue, endValue, time / outTime);
                canvasGroup.alpha = value;
                await UniTask.WaitForEndOfFrame(this);
                time += Time.deltaTime;
            }

            canvasGroup.alpha = endValue;
        }
    }
}