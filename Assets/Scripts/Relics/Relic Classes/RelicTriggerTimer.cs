using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using Unity.VisualScripting;

public class RelicTriggerTimer {
    public event Action OnTimerFinished;
    private Coroutine timer;

    public RelicTriggerTimer (float amount) {
        timer = CoroutineManager.Instance.StartCoroutine(CountDown(amount));
    }

    public IEnumerator CountDown (float amount) {
        yield return new WaitForSeconds(amount);
        OnTimerFinished?.Invoke();
    }

    public void Cancel() {
        if (timer != null) {
            CoroutineManager.Instance.StopCoroutine(timer);
            timer = null;
        }
    }
}