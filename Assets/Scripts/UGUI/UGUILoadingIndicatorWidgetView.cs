using System.Collections;
using UnityEngine;

namespace UGUI
{
    public sealed class UGUILoadingIndicatorWidgetView : UGUIWidgetView<ILoadingIndicatorWidget>
    {
        private Coroutine m_AnimationRoutine;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            m_AnimationRoutine = StartCoroutine(AnimateRoutine());
        }

        protected override void OnDisable()
        {
            StopCoroutine(m_AnimationRoutine);
            m_AnimationRoutine = null;
            base.OnDisable();
        }

        private IEnumerator AnimateRoutine()
        {
            while (true)
            {
                var eulerAngles = transform.eulerAngles;
                eulerAngles.z -= Time.deltaTime * 50f;
                transform.eulerAngles = eulerAngles;
                yield return null;
            }
        }
    }
}