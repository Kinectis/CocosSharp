﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CocosSharp
{
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    internal class CCCustomCommand : CCRenderCommand
    {
        public Action Action { get; internal set; }

        #region Constructors

        public CCCustomCommand(float globalZOrder, CCAffineTransform worldTransform, Action action) 
            : base(globalZOrder, worldTransform)
        {
            Action = action;
        }

        public CCCustomCommand(float globalZOrder, CCAffineTransform worldTransform) 
            : this(globalZOrder, worldTransform, null)
        {
        }

        public CCCustomCommand(float globalZOrder) 
            : this(globalZOrder, CCAffineTransform.Identity, null)
        {
        }

        #endregion Constructors


        internal override void RequestRenderCommand(CCRenderer renderer)
        {
            if(Action != null)
                renderer.ProcessCustomRenderCommand(this);
        }

        internal void RenderCustomCommand(CCDrawManager drawManager)
        {
            drawManager.PushMatrix();
            drawManager.SetIdentityMatrix();
            if (WorldTransform != CCAffineTransform.Identity)
            {
                var worldTrans = WorldTransform.XnaMatrix;
                drawManager.MultMatrix(ref worldTrans);
            }
            Action();

            drawManager.PopMatrix();
        }

        internal new string DebugDisplayString
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            return string.Concat("[CCCustomCommand: Group ", Group.ToString(), " Depth ", GlobalDepth.ToString(),"]");
        }
    }
}

