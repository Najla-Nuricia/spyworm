using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIButtonEffects : MonoBehaviour
{
    [Header("Daftar Tombol")]
    [SerializeField] private List<Button> uiButtons = new List<Button>();

    [Header("Pengaturan Animasi")]
    [SerializeField] private float hoverScaleMultiplier = 1.15f;
    [SerializeField] private float clickScaleMultiplier = 0.9f;
    [SerializeField] private float animationSpeed = 0.1f;
    [SerializeField] private float clickRotationAngle = 10f;

    [Header("Pengaturan Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    private Dictionary<Button, Vector3> originalScales = new Dictionary<Button, Vector3>();
    private Dictionary<Button, Quaternion> originalRotations = new Dictionary<Button, Quaternion>();
    private Dictionary<Button, Coroutine> activeCoroutines = new Dictionary<Button, Coroutine>();

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        foreach (Button btn in uiButtons)
        {
            if (btn != null)
            {
                originalScales[btn] = btn.transform.localScale;
                originalRotations[btn] = btn.transform.localRotation;

                EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
                if (trigger == null)
                {
                    trigger = btn.gameObject.AddComponent<EventTrigger>();
                }

                EventTrigger.Entry entryEnter = new EventTrigger.Entry();
                entryEnter.eventID = EventTriggerType.PointerEnter;
                entryEnter.callback.AddListener((data) => { OnHoverEnter(btn); });
                trigger.triggers.Add(entryEnter);

                EventTrigger.Entry entryExit = new EventTrigger.Entry();
                entryExit.eventID = EventTriggerType.PointerExit;
                entryExit.callback.AddListener((data) => { OnHoverExit(btn); });
                trigger.triggers.Add(entryExit);

                btn.onClick.AddListener(() => { OnButtonClick(btn); });
            }
        }
    }

    private void OnHoverEnter(Button btn)
    {
        if (this == null || btn == null || !btn.interactable) return;

        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }

        StartAnimation(btn, originalScales[btn] * hoverScaleMultiplier, originalRotations[btn]);
    }

    private void OnHoverExit(Button btn)
    {
        if (this == null || btn == null || !btn.interactable) return;
        StartAnimation(btn, originalScales[btn], originalRotations[btn]);
    }

    private void OnButtonClick(Button btn)
    {
        if (this == null || btn == null) return;

        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        StartAnimation(btn, originalScales[btn] * clickScaleMultiplier, originalRotations[btn] * Quaternion.Euler(0, 0, clickRotationAngle), true);
    }

    private void StartAnimation(Button btn, Vector3 targetScale, Quaternion targetRotation, bool isClick = false)
    {
        if (this == null || btn == null) return;

        if (activeCoroutines.ContainsKey(btn) && activeCoroutines[btn] != null)
        {
            StopCoroutine(activeCoroutines[btn]);
        }
        activeCoroutines[btn] = StartCoroutine(AnimateButton(btn, targetScale, targetRotation, isClick));
    }

    private System.Collections.IEnumerator AnimateButton(Button btn, Vector3 targetScale, Quaternion targetRotation, bool isClick)
    {
        Vector3 startScale = btn.transform.localScale;
        Quaternion startRotation = btn.transform.localRotation;
        float time = 0;

        while (time < 1f)
        {
            if (this == null || btn == null) yield break;

            time += Time.deltaTime / animationSpeed;
            btn.transform.localScale = Vector3.Lerp(startScale, targetScale, time);
            btn.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, time);
            yield return null;
        }

        if (isClick)
        {
            yield return new WaitForSeconds(0.05f);

            time = 0;
            Vector3 clickScale = btn.transform.localScale;
            Quaternion clickRotation = btn.transform.localRotation;

            while (time < 1f)
            {
                if (this == null || btn == null) yield break;

                time += Time.deltaTime / animationSpeed;
                btn.transform.localScale = Vector3.Lerp(clickScale, originalScales[btn], time);
                btn.transform.localRotation = Quaternion.Lerp(clickRotation, originalRotations[btn], time);
                yield return null;
            }
        }
    }
}