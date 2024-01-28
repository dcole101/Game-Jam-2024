using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    public AnimationClip cameraClipC;
    public AnimationClip cameraClipV;

    public Animator cameraAnimator;

    void Start()
    {
        //cameraAnimator = GetComponent<Animator>();

        // Disable the Animator component on start
        cameraAnimator.enabled = false;
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.C))
        {
            // Enable the Animator component and play the animation
            EnableAnimatorAndPlayAnimation(cameraClipC);
            Debug.Log("Camera clip 1 played");
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            // Enable the Animator component and play the animation
            EnableAnimatorAndPlayAnimation(cameraClipV);
            Debug.Log("Camera clip 2 played");
        }*/
    }

    public void JesterCamAnim()
    {
        Debug.Log("UPdate Cam");
        if(cameraAnimator == null)
        {
            Debug.Log("Camera V not found");
        }
        EnableAnimatorAndPlayAnimation(cameraClipV);
    }

    public void KingCamAnim()
    {
        Debug.Log("Update Cam");
        EnableAnimatorAndPlayAnimation(cameraClipC);
    }

    void EnableAnimatorAndPlayAnimation(AnimationClip animationClip)
    {
        if (animationClip == null)
        {
            Debug.LogError("AnimationClip is not assigned.");
            return;
        }

        // Enable the Animator component
        cameraAnimator.enabled = true;

        // Play the specified animation clip
        cameraAnimator.Play(animationClip.name);

        // You might also need to disable the Animator again if you only want the animation to play once
        // Invoke("DisableAnimator", animationClip.length);
    }

    void DisableAnimator()
    {
        // Disable the Animator component after the animation has played
        cameraAnimator.enabled = false;
    }
}
