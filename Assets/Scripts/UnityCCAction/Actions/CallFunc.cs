﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class CallFunc : ActionInstant
    {
        System.Action<object[]> func;
        object[] paras;


        public CallFunc(System.Action<object[]> func, params object[] paras)
        {
            if (func == null)
            {
                Debug.LogError("func can not be null");
            }
            this.func = func;
            this.paras = paras;
            this.SetActionName("CallFunc");
        }

        public void Call<T>(System.Action<T> func, T param)
        {

        }



        bool isPartialFinished = false;

        public override event EventHandler<ActionFinishedEventArgs> ActionFinished;

        public override void Run()
        {
            this.isDone = false;
            this.isPartialFinished = false;
            this.func(this.paras);
            this.isPartialFinished = true;

        }

        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override float GetDuration()
        {
            throw new System.NotImplementedException();
        }

        public override GameObject GetOriginalTarget()
        {
            throw new System.NotImplementedException();
        }

        public override int GetTag()
        {
            return this.tag;
        }

        public override GameObject GetTarget()
        {
            return this.target;
        }

        public override bool IsDone()
        {
            return this.isDone;
        }

        public override void Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override void SetDuration(float time)
        {
            throw new System.NotImplementedException();
        }

        public override FiniteTimeAction SetTag(int tag)
        {
            this.tag = tag;
            return this;

        }

        public override void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public override bool Update()
        {
            if (this.isPartialFinished)
            {
                this.OnPartialActionFinished();
            }
            return this.IsDone();
        }

        public override void Finish()
        {
            this.isDone = true;
            this.repeatedTimes = 0;
            if (this.completeCallback != null)
            {
                this.completeCallback(this.completeCallbackParams);
            }
            this.ActionFinished?.Invoke(this, new ActionFinishedEventArgs(this.GetTarget(), this));
        }


        public override int GetRepeatTimes()
        {
            return this.repeatTimes;
        }

        public override FiniteTimeAction SetRepeatTimes(int times)
        {
            this.repeatTimes = times;
            return this;
        }

        protected override void OnPartialActionFinished()
        {
            this.repeatedTimes++;
            if (this.repeatedTimes == this.repeatTimes)
            {
                this.Finish();
            }
            else
            {
                this.Run();
            }
        }

        public override FiniteTimeAction OnComplete(Action<object[]> callback, object[] param)
        {
            this.completeCallback = callback;
            this.completeCallbackParams = param;
            return this;
        }

        public override FiniteTimeAction Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }

        public override FiniteTimeAction SetActionName(string name)
        {
            this.actionName = name;
            return this;
        }

        public override string GetActionName()
        {
            return this.actionName;
        }

        public override bool IsPause()
        {
            return this.isPause;
        }

        public override void Pause()
        {
            Debug.LogWarning("CallFunc is ActionInstant, can not Pause");
        }

        public override void Resume()
        {
            Debug.LogWarning("CallFunc is ActionInstant, can not Resume");
        }

        public override float GetTotalPausedTime()
        {
            return this.totalPausedTime;
        }
    }
}
