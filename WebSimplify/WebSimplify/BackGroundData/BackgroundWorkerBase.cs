using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public abstract class BackgroundWorkerBase
    {
        /// <summary>
        /// do cycle in every supplied miliseconds
        /// </summary>
        public abstract int RepeatEvery { get; }
        public abstract void DoWork();
    }
}