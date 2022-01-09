using System.Collections;
using UnityEngine;

namespace UniLib.Util
{
    /// <summary>
    /// アニメーションユーティリティ
    /// </summary>
    public static class AnimationUtil
    {
        /// <summary>
        /// アニメーション再生して待機
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="animName"></param>
        /// <returns></returns>
        public static IEnumerator PlayAnim(Animator animator, string animName)
        {
            if (animator == null)
            {
                yield break;
            }
            
            animator.Play(animName,0,0);
            animator.speed = 1;
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            yield return null; // ステートの反映に1フレームいる。
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return null;
            }
        }
        
        /// <summary>
        /// 冒頭ポーズ状態で再生する
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="animName"></param>
        /// <param name="normalizedTime"></param>
        public static void PlayAnimPause(Animator animator, string animName, float normalizedTime)
        {
            animator.Play(animName,0,normalizedTime);
            animator.speed = 0;
        }
    }
}
