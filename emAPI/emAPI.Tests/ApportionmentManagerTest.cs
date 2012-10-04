// <copyright file="ApportionmentManagerTest.cs">Copyright ©  2012</copyright>

using System;
using emAPI.Controllers;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using emAPI.ClassLibrary;
using System.Collections.Generic;

namespace emAPI.Controllers
{
    [TestClass]
    [PexClass(typeof(ApportionmentManager))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ApportionmentManagerTest
    {
        [PexMethod(MaxBranches = 20000)]
        public List<Chunk> setupDatedChunksForApportionToPeriod(
            [PexAssumeUnderTest]ApportionmentManager target,
            DateTime startDate,
            DateTime endDate,
            AppotionmentPeriod interval
        )
        {
            List<Chunk> result = target.setupDatedChunksForApportionToPeriod(startDate, endDate, interval);
            return result;
            // TODO: add assertions to method ApportionmentManagerTest.setupDatedChunksForApportionToPeriod(ApportionmentManager, DateTime, DateTime, AppotionmentPeriod)
        }
        [PexMethod(MaxBranches = 20000, MaxConstraintSolverTime = 8, Timeout = 240)]
        public List<Chunk> apportion(
            [PexAssumeUnderTest]ApportionmentManager target,
            List<Chunk> datedChunks,
            List<Chunk> dataIn
        )
        {
            List<Chunk> result = target.apportion(datedChunks, dataIn);
            return result;
            // TODO: add assertions to method ApportionmentManagerTest.apportion(ApportionmentManager, List`1<Chunk>, List`1<Chunk>)
        }
        [PexMethod(MaxBranches = 20000)]
        public List<Chunk> apportionToPeriod(
            [PexAssumeUnderTest]ApportionmentManager target,
            List<Chunk> dataIn,
            DateTime startDate,
            DateTime endDate,
            AppotionmentPeriod interval
        )
        {
            List<Chunk> result = target.apportionToPeriod(dataIn, startDate, endDate, interval);
            return result;
            // TODO: add assertions to method ApportionmentManagerTest.apportionToPeriod(ApportionmentManager, List`1<Chunk>, DateTime, DateTime, AppotionmentPeriod)
        }
    }
}
