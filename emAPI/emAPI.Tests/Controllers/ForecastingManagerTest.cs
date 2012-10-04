// <copyright file="ForecastingManagerTest.cs">Copyright ©  2012</copyright>
using System;
using emAPI.Controllers;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using emAPI.ClassLibrary;
using System.Reflection;

namespace emAPI.Controllers
{
    /// <summary>This class contains parameterized unit tests for ForecastingManager</summary>
    [PexClass(typeof(ForecastingManager))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class ForecastingManagerTest
    {
        [PexMethod(MaxBranches = 20000)]
        [PexMethodUnderTest("forecastInvoice(Invoice)")]
        internal Invoice forecastInvoice([PexAssumeUnderTest]ForecastingManager target, Invoice lastInvoice)
        {
            object[] args = new object[1];
            args[0] = (object)lastInvoice;
            Type[] parameterTypes = new Type[1];
            parameterTypes[0] = typeof(Invoice);
            Invoice result0 = ((MethodBase)(typeof(ForecastingManager).GetMethod("forecastInvoice",
                                                                                 BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic, (Binder)null,
                                                                                 CallingConventions.HasThis, parameterTypes, (ParameterModifier[])null)))
                                  .Invoke((object)target, args) as Invoice;
            Invoice result = result0;
            return result;
            // TODO: add assertions to method ForecastingManagerTest.forecastInvoice(ForecastingManager, Invoice)
        }
    }
}
