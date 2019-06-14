using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZGame.cc
{
    public class Rotate : InfiniteTimeAction
    {
        float xValue;
        float yValue;
        float zValue;
        Space relativeSpace;

        /// <summary>
        /// Applies a rotation of zAngle degrees around the z axis, xAngle degrees around
        /// the x axis, and yAngle degrees around the y axis (in that order) each seconds.        
        /// </summary>
        /// <param name="xValue"></param>
        /// <param name="yValue"></param>
        /// <param name="zValue"></param>
        /// <param name="relativeSpace"></param>
        public Rotate(float xValue, float yValue, float zValue, Space relativeSpace)
        {
            this.xValue = xValue;
            this.yValue = yValue;
            this.zValue = zValue;

            this.relativeSpace = relativeSpace;
            this.SetActionName("Rotate");
        }

        public override event EventHandler<ActionFinishedEventArgs> ActionFinished;

        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override void Finish()
        {
            this.isDone = true;
            this.ActionFinished?.Invoke(this, new ActionFinishedEventArgs(this.GetTarget(), this));
        }

        public override string GetActionName()
        {
            return this.actionName;
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

        public override void Run()
        {

        }

        public override InfiniteTimeAction SetActionName(string name)
        {
            this.actionName = name;
            return this;
        }

        public override InfiniteTimeAction SetTag(int tag)
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

            this.doRotate();
            return this.IsDone();
        }

        private void doRotate()
        {
            if (this.IsDone())
            {
                return;
            }
            this.target.transform.Rotate(xValue * Time.deltaTime, yValue * Time.deltaTime, zValue * Time.deltaTime, this.relativeSpace);

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