using System;
using System.Collections;
using UnityEngine;

public class ParralelTools : PersistentSingleton<ParralelTools>
{
    public static void RunAfterSeconds(float seconds, Action action) => Instance.RunAfterSecondsImpl(seconds, action);
    private void RunAfterSecondsImpl(float seconds, Action action)
    {
        StartCoroutine(RunAfterSecondsCoroutine(seconds, action));
    }

    private IEnumerator RunAfterSecondsCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
}
