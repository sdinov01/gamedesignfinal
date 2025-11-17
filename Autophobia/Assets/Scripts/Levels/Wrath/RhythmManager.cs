using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public float bpm = 118f;
    public KnifeController[] knives;

    private float beatInterval;
    private float timer;
    private int beatCount = 0;

    private int nextKnifeIndex = 0; //  当前轮到哪一把刀

    void Start()
    {
        beatInterval = 60f / bpm;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= beatInterval)
        {
            timer -= beatInterval;
            beatCount++;

            HandleBeat(beatCount);
        }
    }

    void HandleBeat(int beat)
    {
        int beatInBar = ((beat - 1) % 4) + 1; // 1~4 循环 拍号

        if (beat <= 4)
        {
            // 前半段：只有第4拍刺 1 次
            if (beatInBar == 4)
                TriggerNextKnife();
        }
        else
        {
            // 后半段：第 2、4 拍刺两次
            if (beatInBar == 2 || beatInBar == 4)
                TriggerNextKnife();
        }
    }

    void TriggerNextKnife()
    {
        if (knives.Length == 0) return;

        KnifeController k = knives[nextKnifeIndex];
        Debug.Log($"Trigger knife {nextKnifeIndex}");
        k.TriggerAttack();

        // 往下轮
        nextKnifeIndex++;
        if (nextKnifeIndex >= knives.Length)
            nextKnifeIndex = 0;
    }
}