using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Boss3Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;

    public TextMeshProUGUI dialogueName;
    private string[] names;
    private string[] sentences;
    private Sprite[] images;
    private Color[] colors;
    private Color lightBlue = new Color(0, 255, 255, 255);

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private bool dialogueHappened = true;
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
        if (GlobalVariables.dialogueCounter == 1 && sentences == null && dialogueHappened)
        {
            dialogueHappened = false;
            sentences = new string[] {
            "YOU SHRIMPS DIDN'T COUNT WITH MY ASTUCITY!"
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Kleber excited talkin'") //1
        };
            names = new string[]{
                "KLEBER"
            };
            colors = new Color[]{
                Color.red
            };

            StartDialogue();
        }
        if (GlobalVariables.dialogueCounter == 2 && sentences == null)
        {
            dialogueHappened = false;
            sentences = new string[] {
            "DID YOU MISS ME, HANDSOME~? AHUHUHUHUHU~!"
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Garance psycho")
        };
            names = new string[]{
                "GARANCE"
            };
            colors = new Color[]{
                Color.red
            };

            StartDialogue();
        }
        if (GlobalVariables.bossCounter == 50 && sentences == null)
        {
            dialogueHappened = false;
            sentences = new string[] {
            "FINALLY TOOK ALL OF YOU DOWN, HUH?",//1
            "ARGH! I CAN'T BELIEVE I JUST LOST TO THAT SHRIMP",//2
            "TSK... I SHOULD'VE HAD MY AIRSHIP FIXED",//3
            "...",//4
            "HEH. YOU ARE NOT RUNNING AWAY THIS TI--",//5
            "WHAT IN THE ACTUAL--?",//6
            "THE DAMN AIRSHIP IS EMPTY!?",//7
            "DIDN'T YOU NOTICE IT YET, LIL' HUMAN SHRIMP?",//8
            "OUR BOSS WAS NEVER HERE TO BEGIN WITH...",//9
            "THIS WAS ALL ACCORDING TO PLAN, HANDSOME~",//10
            "MAV? CAN YOU HEAR ME? ANSWER ME, YOU DORK!",//11
            "YEAH, THOSE RASCALS' BOSS IS AS SLY AS YOU TOLD ME",//12
            "GREAT! SO, I'VE FINALLY RESTORED THE CONNECTION",//13
            "I HAVE BAD NEWS THO... I KNOW I'M TOO LATE, BUT...",//14
            "THE ACTUAL BOSS, THE *REAL ONE*, IS APPROACHING WITH HIS AIRSHIP",//15
            "HE WILL TRY TO TAKE YOU DOWN NOW THAT YOU ARE TIRED",//16
            "I AIN'T GIVING IT, SAM. I'M STILL STANDING AND I WILL FIGHT",//17
            "IF HE IS COMING RIGHT AT ME, THIS MAKES THINGS WAY EASIER!",//18
            "DON'T GET TOO COCKY YET, WEAKO",//19
            "HIS WAR MACHINE IS WAAAAAAY STRONGER THAN OURS",//20
            "AHUHUHUHU~ MORE POWERFUL THAN *ANYTHING* YOU'VE FOUGHT SO FAR",//21
            "AND HE'LL BRING *ALL OF PHANTOM OMERTA* ALONG",//22
            "SWEET!",//23
            "NOW I CAN SWEEP THE WHOLE MAFIA IN ONE GO",//24
            "HE IS GETTING CLOSER, MAV...",//25
            "GET READY FOR THE BATTLE, YOU DORK! IT'S NOW OR NEVER!"//26
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Maverick worried talkin'"),//1
            Resources.Load<Sprite>("sprites/Kleber annoyed"),//2
            Resources.Load<Sprite>("sprites/Garance angry"),//3
            Resources.Load<Sprite>("sprites/OM3"),//4
            Resources.Load<Sprite>("sprites/Maverick smug talkin'"),//5
            Resources.Load<Sprite>("sprites/Maverick furious talkin'"),//6
            Resources.Load<Sprite>("sprites/Maverick angry talkin'"),//7
            Resources.Load<Sprite>("sprites/Kleber cheeky talkin'"),//8
            Resources.Load<Sprite>("sprites/Garance default"),//9
            Resources.Load<Sprite>("sprites/Garance convinced"),//10
            Resources.Load<Sprite>("sprites/Samira worried"),//11
            Resources.Load<Sprite>("sprites/Maverick worried"),//12
            Resources.Load<Sprite>("sprites/Samira thinking"),//13
            Resources.Load<Sprite>("sprites/Samira worried"),//14
            Resources.Load<Sprite>("sprites/Samira reluctant"),//15
            Resources.Load<Sprite>("sprites/Samira worried"),//16
            Resources.Load<Sprite>("sprites/Maverick smug"),//17
            Resources.Load<Sprite>("sprites/Maverick determined talkin'"),//18
            Resources.Load<Sprite>("sprites/Kleber irreverent"),//19
            Resources.Load<Sprite>("sprites/Kleber convinced talkin'"),//20
            Resources.Load<Sprite>("sprites/Garance psycho"),//21
            Resources.Load<Sprite>("sprites/Garance cheeky"),//22
            Resources.Load<Sprite>("sprites/Maverick determined"),//23
            Resources.Load<Sprite>("sprites/Maverick determined talkin'"),//24
            Resources.Load<Sprite>("sprites/Samira worried"),//25
            Resources.Load<Sprite>("sprites/Samira pissed-off")//26
        };
            names = new string[]{
            "MAVERICK",//1
            "KLEBER",//2
            "GARANCE",//3
            "???",//4
            "MAVERICK",//5
            "MAVERICK",//6
            "MAVERICK",//7
            "KLEBER",//8
            "GARANCE",//9
            "GARANCE",//10
            "SAMIRA",//11
            "MAVERICK",//12
            "SAMIRA",//13
            "SAMIRA",//14
            "SAMIRA",//15
            "SAMIRA",//16
            "MAVERICK",//17
            "MAVERICK",//18
            "KLEBER",//19
            "KLEBER",//20
            "GARANCE",//21
            "GARANCE",//22
            "MAVERICK",//23
            "MAVERICK",//24
            "SAMIRA",//25
            "SAMIRA"//26
            };
            colors = new Color[]{
            lightBlue,//1
            Color.red,//2
            Color.red,//3
            Color.red,//4
            lightBlue,//5
            lightBlue,//6
            lightBlue,//7
            Color.red,//8
            Color.red,//9
            Color.red,//10
            lightBlue,//11
            lightBlue,//12
            lightBlue,//13
            lightBlue,//14
            lightBlue,//15
            lightBlue,//16
            lightBlue,//17
            lightBlue,//18
            Color.red,//19
            Color.red,//20
            Color.red,//21
            Color.red,//22
            lightBlue,//23
            lightBlue,//24
            lightBlue,//25
            lightBlue//26
            };

            StartDialogue();
        }

        if (dialoguePanel.activeInHierarchy &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L) || Input.GetKey(KeyCode.C)))
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
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        sentences = null;
        if (GlobalVariables.dialogueCounter == 1||GlobalVariables.dialogueCounter==2)
        {
            GlobalVariables.dialogueCounter = 0;
            Time.timeScale = 1;
        }else if (GlobalVariables.bossCounter == 50)
        {
            SceneManager.LoadScene("Victory");
        }
    }
}