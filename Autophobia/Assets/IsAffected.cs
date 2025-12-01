using UnityEngine;
using System.Collections;

public class IsAffected : MonoBehaviour
{
    public bool contactDMG;
    private Collider2D      playercollider;
    private Collider2D      thiscollider;
    private healthBar       HB;
    private ModularAudioHandler MAH;
    private SpriteRenderer SR;
    private float S1dmg = 0.005f;
    private float S2dmg = 0.01f;
    private float S3dmg = 0.02f;
    private float S4dmg = 0.03f;

    private GameObject spikes;

    private bool coroRunning;

    private float dmgval;
    private Color Astage1;
    private Color Astage2;
    private Color Astage3;
    private Color Astage4;
    private Color ogColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contactDMG      = false;
        playercollider  = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
        thiscollider    = GetComponent<Collider2D>();
        HB              = GameObject.FindWithTag("HealthBar").GetComponent<healthBar>();
        MAH             = GameObject.FindWithTag("AudioParser").GetComponent<ModularAudioHandler>();
        SR              = GetComponent<SpriteRenderer>();
        Astage1         = new Color (0.3f, 0.2f, 0.2f);
        Astage2         = new Color (0.518f, 0.137f, 0.137f);
        // Astage3         = GameObject.FindWithTag("ColorSource").GetComponent<SpriteRenderer>().color;
        Astage3         = new Color (0.720f, 0.05f, 0.05f);
        Astage4         = new Color(0.859f, 0f, 0f);
        ogColor         = SR.color;

        spikes          = transform.Find("Spikes").gameObject;
        spikes.SetActive (false);

        coroRunning     = false;

        dmgval          = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (contactDMG)
        {
            if (thiscollider.IsTouching(playercollider))
            {
                HB.takeDamage (dmgval);
            }
        }
    }

    public void StartDMG()
    {
        if (coroRunning)
        {
            StopCoroutine(DamageTime());
        }
        StartCoroutine(DamageTime());
    }

    private IEnumerator DamageTime()
    {
        Affect(true);
        yield return new WaitForSeconds((float)(MAH.beat2int));
        SR.color = Astage3; 
        dmgval = S3dmg;
        yield return new WaitForSeconds((float)(MAH.beat2int));
        SR.color = Astage2; 
        dmgval = S2dmg;
        yield return new WaitForSeconds((float)(MAH.beat2int));
        SR.color = Astage1;
        dmgval = S1dmg;
        yield return new WaitForSeconds((float)(MAH.beat2int));
        Affect(false);
    }

    private void Affect(bool startdmg)
    {
        if (startdmg)
        {
            coroRunning = true;
            spikes.SetActive (true);
            contactDMG = true;
            SR.color = Astage4;
            dmgval = S3dmg;
        }
        else
        {
            coroRunning = false;
            spikes.SetActive (false);
            contactDMG = false;
            SR.color = ogColor;
            dmgval = 0f;
        }
    }
}
