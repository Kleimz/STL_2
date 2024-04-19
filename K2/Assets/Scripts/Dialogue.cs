using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Dialogue : Interactable
{
    public TextMeshProUGUI textComponent;
    public Line[] lines;
    public float textSpeed;
    [SerializeField] GameObject dialogueBox;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        lines = GetComponentsInChildren<Line>();
    }

    public void OnActivate()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartDialogue()
    {
        index = 0;

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueBox = GameObject.FindGameObjectWithTag(lines[index].dialogueSpeaker.ToString());
        dialogueBox = dialogueBox.GetComponent<InteractionManager>().dialogueCanvas;
        dialogueBox.SetActive(true);
        textComponent = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = "";
        foreach (char c in lines[index].chosenLine.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);

        }
    }

    void nextLine()
    {
        dialogueBox.SetActive(false);
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }

    public override void Interact(InteractionManager i)
    {
        nextLine();
    }

    public override void SetInteractable(InteractionManager interactionManager)
    {
        interactionManager.currentInteractable = this;
    }

    public override void SetUnInteractable(InteractionManager interactionManager)
    {
        throw new System.NotImplementedException();
    }
}

