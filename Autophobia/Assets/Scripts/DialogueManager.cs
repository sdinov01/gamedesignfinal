using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Sprite frameA;                
    public Sprite frameB;                

    public Image fullFrameImage;         // one Image that swaps A/B
    public TextMeshProUGUI textUI;

    public List<string> lines = new List<string>();
    public bool autoStart = true;

    public float typeDelay = 0.02f;
    public float flipInterval = 0.08f;

    public KeyCode advanceKey = KeyCode.Space;
    public bool clickToAdvance = true;

    private int index = -1;
    private bool isTyping;
    private string fullText;
    private bool currFrame;                 

    void Start()
    {
        if (fullFrameImage && frameA) {
            Color c = fullFrameImage.color;
            c.a = 1f;
            fullFrameImage.color = c;
            fullFrameImage.sprite = frameA;
        }
        if (autoStart) StartDialogue();
    }

    void Update()
    {
        bool pressed = Input.GetKeyDown(advanceKey) || (clickToAdvance && Input.GetMouseButtonDown(0));
        if (!pressed) return;

        if (isTyping)
        {
            isTyping = false;
            if (textUI) textUI.text = fullText;
        }
        else
        {
            ShowNextLine();
        }
    }

    public void StartDialogue()
    {
        StopAllCoroutines();

        index = -1;
        currFrame = false;
        ApplyFrame(false);

        ShowNextLine();
    }

    private void ShowNextLine()
    {
        index++;
        if (index >= lines.Count)
        {
            return; // done
        }

        currFrame = !currFrame;      // swap once per new line
        ApplyFrame(currFrame);

        StopAllCoroutines();
        StartCoroutine(TypeLine(lines[index]));
    }

    private IEnumerator TypeLine(string s)
    {
        isTyping = true;
        fullText = s;
        if (textUI) textUI.text = "";

        foreach (char c in s)
        {
            float delay = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                          ? typeDelay * 0.1f : typeDelay;

            if (!isTyping) break;
            if (textUI) textUI.text += c;
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;

        if (textUI) textUI.text = fullText;
    }

    private void ApplyFrame(bool b)
    {
        if (!fullFrameImage) return;
        fullFrameImage.sprite = (b && frameB) ? frameB : frameA;
    }
}
