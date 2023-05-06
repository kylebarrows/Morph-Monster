using System;
using UnityEngine;

public class FormSwapHandler : MonoBehaviour, IMonsterReceiver
{
    private MonsterCharacter Character;

    public FormInfo monsterForm = new MonsterFormInfo();

    public FormInfo activeForm;

    public bool isTransformed;

    [SerializeField] public RuntimeAnimatorController monsterAnimController;
    [SerializeField] public RuntimeAnimatorController meleeAnimController;
    [SerializeField] public RuntimeAnimatorController rangerAnimController;
    [SerializeField] public RuntimeAnimatorController heavyAnimController;

    [SerializeField] private GameObject OnTransformEffect;
    [SerializeField] private GameObject currentCorpse;
    [SerializeField] private GameObject MeleeStopTransformEffect;
    [SerializeField] private GameObject RangerStopTransformEffect;
    [SerializeField] private GameObject BruteStopTransformEffect;

    public static event Action<FormInfo> OnFormChange;

    private FormInfo[] Forms = new FormInfo[4] { new MonsterFormInfo(), new MeleeFormInfo(), new BruteFormInfo(), new RangerFormInfo() };

    public void Start()
    {
        activeForm = new MonsterFormInfo();
    }

    public void ResetForm()
    {
        ChangeForm(0, false);
    }

    public void ChangeForm(int formID, bool load)
    {
        FormInfo newForm = Forms[formID];
        ChangeFormPrivate(newForm, load);
    }

    private void ChangeFormPrivate(FormInfo newForm, bool load)
    {
        //Debug.Log("Changing Form!");
        if(newForm.Form == activeForm.Form)
        {
            return;
        }

        switch (newForm.Form)
        {
            case FormNames.Monster:
                isTransformed = false;
                Character.Controller.animator.runtimeAnimatorController = monsterAnimController as RuntimeAnimatorController;
                break;
            case FormNames.Melee:
                isTransformed = true;
                currentCorpse = MeleeStopTransformEffect;
                Character.Controller.animator.runtimeAnimatorController = meleeAnimController as RuntimeAnimatorController;
                break;
            case FormNames.Ranger:
                isTransformed = true;
                currentCorpse = RangerStopTransformEffect;
                Character.Controller.animator.runtimeAnimatorController = rangerAnimController as RuntimeAnimatorController;
                break;
            case FormNames.Brute:
                isTransformed = true;
                currentCorpse = BruteStopTransformEffect;
                Character.Controller.animator.runtimeAnimatorController = heavyAnimController as RuntimeAnimatorController;
                break;
            default:
                isTransformed = false;
                Character.Controller.animator.runtimeAnimatorController = monsterAnimController as RuntimeAnimatorController;
                break;
        }

        activeForm = newForm;
        Character.Controller.SetCharacterScale(newForm.scale);
        Character.Controller.SetColliderBounds(newForm.ColliderSize, newForm.ColliderOffset);
        OnFormChange.Invoke(newForm);

        // Is the player loading from a checkpoint?
        if(load)
        {
            return;
        }

        if(activeForm.Form != FormNames.Monster )
        {
            Instantiate(OnTransformEffect, Character.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(currentCorpse, Character.transform.position, Quaternion.Euler(0, 0, 90));
            Instantiate(OnTransformEffect, Character.transform.position, Quaternion.identity);
        }

        

    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Character = character;
        Character.SwapHandler = this;
    }
}
