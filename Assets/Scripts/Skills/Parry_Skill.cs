using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    public bool parryUnlocked { get; private set; }

    [Header("Parry restore")]
    [SerializeField] private UI_SkillTreeSlot parryRestoreUnlockButton;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthAmount;
    public bool parryRestoreUnlocked { get; private set; }

    [Header("Parry with a mirage")]
    [SerializeField] private UI_SkillTreeSlot parryWithAMirageUnlockButton;
    public bool parryWithAMirageUnlocked { get; private set; }

    public override void UseSkill()
    {
        base.UseSkill();

        if (parryRestoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthAmount);
            player.stats.IncreaseHealthBy(restoreAmount);
        }
    }

    protected override void Start()
    {
        base.Start();

        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        parryRestoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithAMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithAMirage);
    }

    protected override void CheckUnlocked()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithAMirage();
    }

    private void UnlockParry()
    {
        if (parryUnlockButton.unlocked)
            parryUnlocked = true;
    }

    private void UnlockParryRestore()
    {
        if (parryRestoreUnlockButton.unlocked)
            parryRestoreUnlocked = true;
    }

    private void UnlockParryWithAMirage()
    {
        if (parryWithAMirageUnlockButton.unlocked)
            parryWithAMirageUnlocked = true;
    }

    public void MakeMirageOnParry(Transform _respawnTransform)
    {
        if (parryWithAMirageUnlocked)
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
    }
}
