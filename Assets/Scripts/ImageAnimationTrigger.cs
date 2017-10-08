using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAnimationTrigger : MonoBehaviour
{

    private Animator mAnimator;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    [ContextMenu("Play animation")]
    public void TriggerAnimation()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        mAnimator.Play("TakenImageAnimation");

       yield return new WaitForSeconds(mAnimator.GetCurrentAnimatorStateInfo(0).length);

        UiHandler.Instance.SetPlayerNameActive(true);
    }
}
