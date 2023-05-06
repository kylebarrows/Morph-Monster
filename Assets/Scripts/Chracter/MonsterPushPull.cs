using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPushPull : MonsterFormAbility, IMonsterReceiver
{
    MonsterCharacter Character;

    MonsterController Controller;

    [SerializeField] private float TARGET_RANGE;
    [SerializeField] private LayerMask BOX_MASK;

    public bool isPulling;

    private PushPullObject mPushable;

    void Start()
    {
        //PushPullObject.OnDetachFromCharacter += StopPulling;
        FormSwapHandler.OnFormChange += OnFormChange;

    }

    private PushPullObject CheckForPushPullObject()
    {
        PushPullObject pushable = null;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 
            TARGET_RANGE, BOX_MASK);
        if (hit.collider != null && hit.collider.gameObject.tag=="PushPullObject")
        {
            Debug.Log("ObjectHit");
            pushable = hit.collider.gameObject.GetComponent<PushPullObject>();
        }

        return pushable;
    }

    public override void UpdateAbility()
    {
        if (!Active)
        {
            return;
        }

        if (isPulling)
        {
            if(!Character.MInput.pullObjectHeld || !IsObjectInRange() || !Controller.isOnGround)
            {
                ReleaseObject();
            }
            else
            {
                HandleMovingObject();
            }
        }
        else
        {
            if (Character.MInput.pullObjectHeld && Controller.isOnGround)
            { 
                PushPullObject pushable = CheckForPushPullObject();
                if(pushable != null)
                {
                    mPushable = pushable;
                    GrabObject();
                }
            }
        }
    }

    private void GrabObject()
    {
        if (!isPulling)
        {
            isPulling = true;
            mPushable.OnGrabbed();
        }
    }

    private void ReleaseObject()
    {
        Debug.Log("Event triggered!!!");
        mPushable.OnReleased(Controller.rb2d);
        isPulling = false;
        mPushable =  null;
    }

    private void HandleMovingObject()
    {
        mPushable.OnMove(Controller.rb2d);
    }

    private bool IsObjectInRange()
    {
        float dist = Vector2.Distance(transform.position, mPushable.rb2d.position);
        return dist <= mPushable.halfWidth + TARGET_RANGE + 0.1;
    }

    public void OnFormChange(FormInfo info)
    {
        MonsterFormAbility.Activate(this, info.hasPushPull);
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Character = character;
        Controller = Character.Controller;
        Character.Abilities.PushPull = this;
    }
}
