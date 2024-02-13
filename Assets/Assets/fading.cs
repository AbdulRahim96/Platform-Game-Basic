using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class fading : MonoBehaviour
{
    public Animator fadeAnim;
    void Start()
    {
        fadeAnim.StopPlayback();
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator fade(UnityEvent uv)
    {
        fadeAnim.enabled = true;
        yield return new WaitForSeconds(1.5f);
        uv.Invoke();
    }

    public void fadeinOut(UnityEvent ue)
    {
        fadeAnim.Play("fade");
        StartCoroutine(fade(ue));
    }
}
