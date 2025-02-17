using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component to be attached to the game object that is able to gradually change the alpha parameter 
/// from the CanvasGroup component of the same object.
/// </summary>
public class FadeScript : MonoBehaviour
{
    /// <summary>
    /// The speed of alpha transition. The higher the number - the faster the transition.
    /// </summary>
    public float fadeSpeed = 1;
    /// <summary>
    /// If true - fade in. If false - fade out.
    /// </summary>
    public bool fadeDirection = true;

    /// <summary>
    /// Variable that holds current in-transition state.
    /// </summary>
    private bool isInTransition = false;
    /// <summary>
    /// A CanvasGroup component attached to the same game object as this script.
    /// The alpha property of this component is being manipulated by this script.
    /// </summary>
    private CanvasGroup canvasGroup;

    /// <summary>
    /// Called when the script object is initialized.
    /// </summary>
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Switches the fade direction to true - fade in.
    /// The Update calls handle the actual transition.
    /// </summary>
    public void FadeIn()
    {
        isInTransition = true;
        fadeDirection = true;
    }

    /// <summary>
    /// Switches the fade direction to false - fade out.
    /// The Update calls handle the actual transition.
    /// </summary>
    public void FadeOut()
    {
        isInTransition = true;
        fadeDirection = false;
    }

    /// <summary>
    /// Check if transition is in progress
    /// </summary>
    /// <returns></returns>
    public bool IsInTransition()
    {
        return isInTransition;
    }

    /// <summary>
    /// Update is called once per frame. Initiates either FadeIn or FadeOut
    /// If alpha is already 1 and current fade direction is true (fade in) - skip transition.
    /// If alpha is already 0 and current fade direction is false (fade out) - skip transition.
    /// </summary>
    private void Update()
    {
        if ((fadeDirection && canvasGroup.alpha == 1) || (!fadeDirection && canvasGroup.alpha == 0))
            return;

        FadeTo(fadeDirection ? 1.0f : 0.0f);
    }

    /// <summary>
    /// Gradually changes the CanvasGroup alpha value to the targeted value.
    /// </summary>
    /// <param name="target">The target alpha value that the current alpha would be changed to over time, based on the fade speed property.</param>
    /// <returns></returns>
    private void FadeTo(float target)
    {
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime * fadeSpeed);
        isInTransition = canvasGroup.alpha != 1 && canvasGroup.alpha != 0;
    }
}
