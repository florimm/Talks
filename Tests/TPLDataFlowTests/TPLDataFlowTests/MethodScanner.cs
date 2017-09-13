﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TPLDataFlowTests
{
    internal class MethodScanner : IMethodInfoScanner
    {
        private readonly Func<MethodInfo, bool> _methodSelector;

        public MethodScanner(Func<MethodInfo, bool> methodSelector)
        {
            _methodSelector = methodSelector;
        }

        public MethodScanner(string methodName)
            : this(mi => mi.Name == methodName)
        {
        }

        public IEnumerable<MethodInfo> GetMethodInfos(object targetToAdapt)
        {
            if (targetToAdapt == null) throw new ArgumentNullException("targetToAdapt");
            var candidates = targetToAdapt.GetType().MethodCandidatesForSubscriptionBuilders(_methodSelector).ToList();
            return candidates;
        }
    }
}