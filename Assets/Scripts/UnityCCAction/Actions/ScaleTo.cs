using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 坐标系统为：基于父节点
    /// </summary>
    public class ScaleTo : ActionInterval
    {
        public Vector3 startScale = Vector3.zero;
        public Vector3 targetScale;

        public override event EventHandler<ActionFinishedEventArgs> ActionFinished;

        public ScaleTo(float duration, Vector3 targetPos)
        {
            if (duration <= 0)
            {
                Debug.LogError("error, duration should >0");
                return;
            }
            this.SetDuration(duration);
            this.targetScale = targetPos;
            this.SetActionName("ScaleTo");
        }
        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override FiniteTimeAction Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }

        public override ActionInterval Easing(Ease ease)
        {
            this.easeFunc = EaseTool.Get(ease);
            return this;
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

        public override float GetDuration()
        {
            throw new System.NotImplementedException();
        }

        public override GameObject GetOriginalTarget()
        {
            throw new System.NotImplementedException();
        }

        public override int GetRepeatTimes()
        {
            return this.repeatTimes;
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

        public override FiniteTimeAction OnComplete(Action<object[]> callback, object[] param)
        {
            this.completeCallback = callback;
            this.completeCallbackParams = param;
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

        public override void Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override void Run()
        {
            this.isDone = false;

            if (this.startScale == Vector3.zero)
            {
                this.startScale = this.target.transform.localScale;
            }
            this.startTime = Time.time- this.GetTotalPausedTime();
        }

        public override void SetDuration(float time)
        {
            this.duration = time;
        }

        public override FiniteTimeAction SetRepeatTimes(int times)
        {
            this.repeatTimes = times;
            return this;
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
            if (this.IsDone())
            {
                return true;
            }
            if (this.IsPause())
            {
                return false;
            }

            if (Time.time - startTime - this.GetTotalPausedTime() > this.duration)
            {
                this.OnPartialActionFinished();
            }

            this.doScaleTo(Time.time);

            return this.IsDone();
        }

        private void doScaleTo(float time)
        {
            if (this.IsDone())
            {
                return;
            }

            var dir = this.targetScale - this.startScale;
            float t = (time - startTime - this.GetTotalPausedTime()) / this.duration;
            t = t > 1 ? 1 : t;
            var desScale = this.startScale + dir * (this.easeFunc(t));
            this.target.transform.localScale = desScale;
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
            if (this.isPause)
            {
                return;
            }

            this.isPause = true;
            this.lastPausedTime = Time.time;
        }

        public override void Resume()
        {
            if (this.isPause == false)
            {
                return;
            }
            this.isPause = false;
            this.totalPausedTime += (Time.time - this.lastPausedTime);
        }

        public override float GetTotalPausedTime()
        {
            return this.totalPausedTime;
        }
    }
}
