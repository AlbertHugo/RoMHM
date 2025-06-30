using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Audio;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;

    private string[] sentences;
    private Sprite[] images;
    public TextMeshProUGUI dialogueName;
    private string[] names;
    private Color[] colors;
    private Color lightBlue = new Color(0, 255, 255, 255);

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private string currentSentence;
    private Coroutine typingCoroutine;

    public AudioClip typingSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
    if (GlobalVariables.waveCounter == 17 && sentences == null)
    {
        sentences = new string[] {
            "WELL WELL WELL. . . LOOK WHAT WE'VE GOT HERE",//1
            "A LIL' ANNOYING HUMAN WIMP",//2
            "LOOK WHO'S TALKING.",//3
            "UGH. . .",//4
            "I'M GONNA KILL YOU, SLIPPERY LIL' BASTARD!",//5
            "HA! IS THAT ALL YOU'VE GOT?",//6
            "I THOUGHT THE JERK WHO KEPT THIS DISTRICT WAS GONNA BE AN ACTUAL THREAT",//7
            "NOT A STUPID GYM RAT~",//8
            "UGH. . .",//9
            "CAN'T WAIT TO SMASH THAT LIL' CONFIDENCE OF YOURS",//10
            "I'LL SURELY L. O. V. E. DOING IT",//11
            "HA. AS IF YOU WERE ACTUALLY CAPABLE OF DOING IT",//12
            "I BET YOU CAN'T EVEN-",//13
            "AND THEN PROCEED TO YOUR FRIEND",//14
            ". . .",//15
            "W-WHAT. . .",//16
            "WHAT DID YOU JUST SAY?",//17
            "HEH HEH HEH. . .",//18
            "AWW~ YOU HUMANS ARE ALL A BUNCHA DEAFS",//19
            "I'M GONNA REPEAT IT JUST ONE MORE TIME FOR YOU, LIL' WIMP",//20
            "ONCE I GET RID OF THAT STUPID ASS OF YOURS.",//21
            "I'M GOING AFTER YOUR LIL' AND HELLA ANNOYING FRIEND!",//22
            "SO? HEARD IT NOW? OR ARE YOU STILL A DEAF WEAKO~?",//23
            ". . .",//24
            "I'M GOING TO WIPE THAT LITTLE SMILE OUT OF YOUR FACE. . .",//25
            "THAT'S MORE LIKE IT!",//26
            "HAHAHAHAHAHHAHAHA! BRING IT ON, WEAKO!"//27
        };

        images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Kleber convinced talkin'"),//1
            Resources.Load<Sprite>("sprites/Kleber convinced"),//2
            Resources.Load<Sprite>("sprites/Maverick cheeky talkin'"),//3
            Resources.Load<Sprite>("sprites/Kleber annoyed"),//4
            Resources.Load<Sprite>("sprites/Kleber excited talkin'"),//5
            Resources.Load<Sprite>("sprites/Maverick determined talkin'"),//6
            Resources.Load<Sprite>("sprites/Maverick convinced talkin'"),//7
            Resources.Load<Sprite>("sprites/Maverick cheeky talkin'"),//8
            Resources.Load<Sprite>("sprites/Kleber default"),//9
            Resources.Load<Sprite>("sprites/Kleber annoyed talkin'"),//10
            Resources.Load<Sprite>("sprites/Kleber excited talkin'"),//11
            Resources.Load<Sprite>("sprites/Maverick convinced talkin'"),//12
            Resources.Load<Sprite>("sprites/Maverick smug talkin'"),//13
            Resources.Load<Sprite>("sprites/Kleber cheeky talkin'"),//14
            Resources.Load<Sprite>("sprites/Maverick furious"),//15
            Resources.Load<Sprite>("sprites/Maverick furious talkin'"),//16
            Resources.Load<Sprite>("sprites/Maverick angry talkin'"),//17
            Resources.Load<Sprite>("sprites/Kleber irreverent"),//18
            Resources.Load<Sprite>("sprites/Kleber irreverent talkin'"),//19
            Resources.Load<Sprite>("sprites/Kleber cheeky talkin'"),//20
            Resources.Load<Sprite>("sprites/Kleber cheeky"),//21
            Resources.Load<Sprite>("sprites/Kleber cheeky talkin'"),//22
            Resources.Load<Sprite>("sprites/Kleber convinced talkin'"),//23
            Resources.Load<Sprite>("sprites/Maverick furious"),//24
            Resources.Load<Sprite>("sprites/Maverick furious talkin'"),//25
            Resources.Load<Sprite>("sprites/Kleber excited"),//26
            Resources.Load<Sprite>("sprites/Kleber excited talkin'")//27
        };
        names = new string[]{
                "KLEBER",
                "KLEBER",
                "MAVERICK",
                "KLEBER",
                "KLEBER",
                "MAVERICK",
                "MAVERICK",
                "MAVERICK",
                "KLEBER",
                "KLEBER",
                "KLEBER",
                "MAVERICK",
                "MAVERICK",
                "KLEBER",
                "MAVERICK",
                "MAVERICK",
                "MAVERICK",
                "KLEBER",
                "KLEBER",
                "KLEBER",
                "KLEBER",
                "KLEBER",
                "KLEBER",
                "MAVERICK",
                "MAVERICK",
                "KLEBER",
                "KLEBER"
            };
            colors = new Color[]{
                Color.red,
                Color.red,
                lightBlue,
                Color.red,
                Color.red,
                lightBlue,
                lightBlue,
                lightBlue,
                Color.red,
                Color.red,
                Color.red,
                lightBlue,
                lightBlue,
                Color.red,
                lightBlue,
                lightBlue,
                lightBlue,
                Color.red,
                Color.red,
                Color.red,
                Color.red,
                Color.red,
                Color.red,
                lightBlue,
                lightBlue,
                Color.red,
                Color.red
            };

            StartDialogue();
    }

            if (dialoguePanel.activeInHierarchy &&
                (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L)||Input.GetKey(KeyCode.C)))
            {
                if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                isTyping = false;
                dialogueText.text = currentSentence;
            }
            else
            {
                index++;
                if (index < sentences.Length)
                {
                    currentSentence = sentences[index];
                    dialogueImage.sprite = images[index];
                    dialogueName.text = names[index];
                    dialogueName.color = colors[index];
                    typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
                }
                else
                {
                    EndDialogue();
                }
            }
            }
            }

public void StartDialogue()
{
    index = 0;
    dialoguePanel.SetActive(true);
    currentSentence = sentences[index];
    dialogueImage.sprite = images[index];
    dialogueName.text = names[index];
    dialogueName.color = colors[index];
    typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
}

IEnumerator TypeSentence(string sentence)
{
    isTyping = true;
    dialogueText.text = "";

    foreach (char letter in sentence.ToCharArray())
    {
        dialogueText.text += letter;
        audioSource.Play();
        yield return new WaitForSeconds(typingSpeed);
    }

    isTyping = false;
}

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        sentences = null;
        GlobalVariables.waveCounter = 20;
    }
}
