using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Line
{
    public string line;
    public Sprite frame;
}

public class DialogueManager : MonoBehaviour
{              
    public Image fullFrameImage;         // one Image that swaps A/B
    public TextMeshProUGUI textUI;

    public List<Line> lines = new List<Line>();
    //public bool autoStart = true;

    public float typeDelay = 0.02f;
    public float flipInterval = 0.08f;

    public KeyCode advanceKey = KeyCode.Space;
    public bool clickToAdvance = true;

    private int index = -1;
    private bool isTyping;
    private string fullText;              

    void Start()
    {
        Color c = fullFrameImage.color;
        c.a = 1f;
        fullFrameImage.color = c;

        StartDialogue();
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

        ApplyFrame(0);

        ShowNextLine();
    }

    private void ShowNextLine()
    {
        index++;
        if (index >= lines.Count)
        {
            return; // done
        }

        ApplyFrame(index);

        StopAllCoroutines();
        StartCoroutine(TypeLine(lines[index].line));
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

    private void ApplyFrame(int currLine)
    {
        if (!fullFrameImage) return;
        fullFrameImage.sprite = lines[currLine].frame;
    }
}
