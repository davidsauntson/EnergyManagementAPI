// <copyright file="ApportionmentManagerTest.apportionToPeriod.g.cs">Copyright �  2012</copyright>
// <auto-generated>
// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
// </auto-generated>
using System;
using emAPI.ClassLibrary;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace emAPI.Controllers
{
    public partial class ApportionmentManagerTest
    {
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionToPeriodThrowsNullReferenceException478()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    apportionmentManager = new ApportionmentManager();
    list = this.apportionToPeriod(apportionmentManager, (List<Chunk>)null, 
                                  default(DateTime), default(DateTime), (AppotionmentPeriod)0);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod630()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[0];
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   default(DateTime), default(DateTime), (AppotionmentPeriod)0);
    Assert.IsNull((object)list1);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(ArgumentOutOfRangeException))]
public void apportionToPeriodThrowsArgumentOutOfRangeException539()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   default(DateTime), default(DateTime), (AppotionmentPeriod)0);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(ArgumentOutOfRangeException))]
public void apportionToPeriodThrowsArgumentOutOfRangeException910()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 3L, (DateTimeKind)(3uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                              (DateTimeKind)(2305843009213693952uL >> 62));
    s1.Amount = 2;
    chunks[1] = s1;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62)), 
                                   new DateTime(4611686018427387903L & 2305843009213693952L, 
                                                (DateTimeKind)(2305843009213693952uL >> 62)), (AppotionmentPeriod)0);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod768()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = default(DateTime);
    s1.Amount = 2;
    chunks[1] = s1;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62)), 
                                   new DateTime(4611686018427387903L & 2305843009213693952L, 
                                                (DateTimeKind)(2305843009213693952uL >> 62)), (AppotionmentPeriod)0);
    Assert.IsNull((object)list1);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(ArgumentOutOfRangeException))]
public void apportionToPeriodThrowsArgumentOutOfRangeException495()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 2;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                              (DateTimeKind)(2305843009213693952uL >> 62));
    s1.Amount = 1;
    chunks[1] = s1;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2305843009213693952L, 
                                                (DateTimeKind)(2305843009213693952uL >> 62)), 
                                   new DateTime(4611686018427387903L & 2305843009213693953L, 
                                                (DateTimeKind)(2305843009213693953uL >> 62)), (AppotionmentPeriod)0);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod599()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62)), 
                                   default(DateTime), (AppotionmentPeriod)0);
    Assert.IsNull((object)list1);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod817()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 3L, (DateTimeKind)(3uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = new DateTime(4611686018427387903L & 35184372088832L, 
                              (DateTimeKind)(35184372088832uL >> 62));
    s1.Amount = 2;
    chunks[1] = s1;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2L, (DateTimeKind)(2uL >> 62)), 
                                   new DateTime(4611686018427387903L & 35184372088833L, 
                                                (DateTimeKind)(35184372088833uL >> 62)), AppotionmentPeriod.Annually);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod61()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 3L, (DateTimeKind)(3uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = default(DateTime);
    s2.EndDate = new DateTime(4611686018427387903L & 35184372088832L, 
                              (DateTimeKind)(35184372088832uL >> 62));
    s2.Amount = 2;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2L, (DateTimeKind)(2uL >> 62)), 
                                   new DateTime(4611686018427387903L & 35184372088833L, 
                                                (DateTimeKind)(35184372088833uL >> 62)), AppotionmentPeriod.Annually);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod490()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 2305843009213693956L, 
                                (DateTimeKind)(2305843009213693956uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 2;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate =
      new DateTime(4611686018427387903L & 5L, (DateTimeKind)(5uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = default(DateTime);
    s2.EndDate = new DateTime(4611686018427387903L & 2305878193585782784L, 
                              (DateTimeKind)(2305878193585782784uL >> 62));
    s2.Amount = 1;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2L, (DateTimeKind)(2uL >> 62)), 
                                   new DateTime(4611686018427387903L & 35184372088833L, 
                                                (DateTimeKind)(35184372088833uL >> 62)), AppotionmentPeriod.Annually);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod374()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 2305843009213693956L, 
                                (DateTimeKind)(2305843009213693956uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 2;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate =
      new DateTime(4611686018427387903L & 5L, (DateTimeKind)(5uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                                (DateTimeKind)(2305843009213693952uL >> 62));
    s2.EndDate = new DateTime(4611686018427387903L & 2305878193585782784L, 
                              (DateTimeKind)(2305878193585782784uL >> 62));
    s2.Amount = 1;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2L, (DateTimeKind)(2uL >> 62)), 
                                   new DateTime(4611686018427387903L & 35184372088833L, 
                                                (DateTimeKind)(35184372088833uL >> 62)), AppotionmentPeriod.Annually);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod547()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 3L, (DateTimeKind)(3uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 2;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                              (DateTimeKind)(2305843009213693952uL >> 62));
    s1.Amount = 1;
    chunks[1] = s1;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2305843009213693954L, 
                                                (DateTimeKind)(2305843009213693954uL >> 62)), 
                                   new DateTime(4611686018427387903L & 2305843009213693954L, 
                                                (DateTimeKind)(2305843009213693954uL >> 62)), AppotionmentPeriod.Daily);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod674()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 1537223721734442314L, 
                                (DateTimeKind)(1537223721734442314uL >> 62));
    s0.EndDate =
      new DateTime(4611686018427387903L & 262144L, (DateTimeKind)(262144uL >> 62));
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = new DateTime(4611686018427387903L & 44259974186992L, 
                                (DateTimeKind)(44259974186992uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = new DateTime(4611686018427387903L & 576505012279189504L, 
                                (DateTimeKind)(576505012279189504uL >> 62));
    s2.EndDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                              (DateTimeKind)(2305843009213693952uL >> 62));
    s2.Amount = 2;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2L, (DateTimeKind)(2uL >> 62)), 
                                   new DateTime(4611686018427387903L & 274877906945L, 
                                                (DateTimeKind)(274877906945uL >> 62)), AppotionmentPeriod.Annually);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod222()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 3074457345618214912L, 
                                (DateTimeKind)(3074457345618214912uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = new DateTime(4611686018427387903L & 2305843009213706240L, 
                                (DateTimeKind)(2305843009213706240uL >> 62));
    s1.EndDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                              (DateTimeKind)(2305843009213693952uL >> 62));
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = new DateTime(4611686018427387903L & 2305843009213707265L, 
                                (DateTimeKind)(2305843009213707265uL >> 62));
    s2.EndDate = new DateTime(4611686018427387903L & 3080462145121419265L, 
                              (DateTimeKind)(3080462145121419265uL >> 62));
    s2.Amount = 2;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, default(DateTime), 
                                                               new DateTime(4611686018427387903L & 32L, (DateTimeKind)(32uL >> 62)), 
                                                               AppotionmentPeriod.Annually);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod352()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = new DateTime(4611686018427387903L & 4611686018427387904L, 
                                (DateTimeKind)(4611686018427387904uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = default(DateTime);
    s2.EndDate = new DateTime(4611686018427387903L & 562949953421312L, 
                              (DateTimeKind)(562949953421312uL >> 62));
    s2.Amount = 2;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, default(DateTime), 
                                                               new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62)), 
                                                               AppotionmentPeriod.Annually);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(4, list1.Capacity);
    Assert.AreEqual<int>(1, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod730()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[4];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 3L, (DateTimeKind)(3uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = default(DateTime);
    s2.EndDate = default(DateTime);
    s2.Amount = 3;
    chunks[2] = s2;
    Chunk s3 = default(Chunk);
    s3.StartDate = default(DateTime);
    s3.EndDate = new DateTime(4611686018427387903L & 35184372088832L, 
                              (DateTimeKind)(35184372088832uL >> 62));
    s3.Amount = 2;
    chunks[3] = s3;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2L, (DateTimeKind)(2uL >> 62)), 
                                   new DateTime(4611686018427387903L & 35184372088833L, 
                                                (DateTimeKind)(35184372088833uL >> 62)), AppotionmentPeriod.Annually);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod604()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 576460752303423491L, 
                                (DateTimeKind)(576460752303423491uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = new DateTime(4611686018427387903L & 2882303761517117440L, 
                              (DateTimeKind)(2882303761517117440uL >> 62));
    s1.Amount = 2;
    chunks[1] = s1;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2305843009213693954L, 
                                                (DateTimeKind)(2305843009213693954uL >> 62)), 
                                   new DateTime(4611686018427387903L & 2305860601399738369L, 
                                                (DateTimeKind)(2305860601399738369uL >> 62)), AppotionmentPeriod.Weekly);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod428()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 3L, (DateTimeKind)(3uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = new DateTime(4611686018427387903L & 8796093022208L, 
                              (DateTimeKind)(8796093022208uL >> 62));
    s1.Amount = 2;
    chunks[1] = s1;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 2L, (DateTimeKind)(2uL >> 62)), 
                                   new DateTime(4611686018427387903L & 8796093022209L, 
                                                (DateTimeKind)(8796093022209uL >> 62)), AppotionmentPeriod.Monthly);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportionToPeriod533()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[2];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = default(DateTime);
    s1.EndDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                              (DateTimeKind)(2305843009213693952uL >> 62));
    s1.Amount = 2;
    chunks[1] = s1;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportionToPeriod(apportionmentManager, list, 
                                   new DateTime(4611686018427387903L & 576460752303423488L, 
                                                (DateTimeKind)(576460752303423488uL >> 62)), 
                                   new DateTime(4611686018427387903L & 576460752303423489L, 
                                                (DateTimeKind)(576460752303423489uL >> 62)), AppotionmentPeriod.Quarterly);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
    }
}
