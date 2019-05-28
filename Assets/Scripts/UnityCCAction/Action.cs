﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 所有动作类型的基类
    /// </summary>
    public abstract class Action
    {
         public bool isDone = false;
        public GameObject target = null;


        public abstract void Run();
        /// <summary>
        /// 返回值指示是否完成动作
        /// </summary>
        /// <returns></returns>
        public abstract bool Update();
        public abstract void Finish();

        /// <summary>
        /// 返回一个克隆的对象
        /// </summary>
        /// <returns></returns>
        public abstract Action Clone();
        /// <summary>
        /// 动作是否完成，true 是， false 否
        /// </summary>
        /// <returns></returns>
        public abstract bool IsDone();

        /// <summary>
        /// 获得执行当前动作的目标节点
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GetTarget();

        /// <summary>
        /// 为动作设置执行的目标节点
        /// </summary>
        /// <param name="target"></param>
        public abstract void SetTarget(GameObject target);

        /// <summary>
        /// 获得原始目标节点
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GetOriginalTarget();

        /// <summary>
        /// 获得动作的Tag
        /// </summary>
        /// <returns></returns>
        public abstract int GetTag();

        /// <summary>
        /// 为动作设置标签，用于识别动作
        /// </summary>
        /// <param name="tag"></param>
        public abstract void SetTag(int tag);
    }
}
