// <copyright file="ApportionmentManagerTest.apportion.g.cs">Copyright �  2012</copyright>
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
[PexRaisedException(typeof(ArgumentNullException))]
public void apportionThrowsArgumentNullException907()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    apportionmentManager = new ApportionmentManager();
    list =
      this.apportion(apportionmentManager, (List<Chunk>)null, (List<Chunk>)null);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(InvalidOperationException))]
public void apportionThrowsInvalidOperationException285()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[0];
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException51()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException783()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate = default(DateTime);
    s0.EndDate = default(DateTime);
    s0.Amount = 2;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 1;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = default(DateTime);
    s2.EndDate = default(DateTime);
    s2.Amount = 3;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException14()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                                (DateTimeKind)(2305843009213693952uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 2;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = default(DateTime);
    s2.EndDate = default(DateTime);
    s2.Amount = 3;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException520()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 2306406027886592000L, 
                                (DateTimeKind)(2306406027886592000uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 2;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = new DateTime(4611686018427387903L & 563018672898049L, 
                                (DateTimeKind)(563018672898049uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = new DateTime(4611686018427387903L & 2305843009213693952L, 
                                (DateTimeKind)(2305843009213693952uL >> 62));
    s2.EndDate = default(DateTime);
    s2.Amount = 1;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException114()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[3];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 720575941453021185L, 
                                (DateTimeKind)(720575941453021185uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = new DateTime(4611686018427387903L & 648518346341351426L, 
                                (DateTimeKind)(648518346341351426uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = new DateTime(4611686018427387903L & 2954361355555045379L, 
                                (DateTimeKind)(2954361355555045379uL >> 62));
    s2.EndDate = default(DateTime);
    s2.Amount = 2;
    chunks[2] = s2;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException944()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[4];
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException203()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[4];
    Chunk s0 = default(Chunk);
    s0.StartDate = default(DateTime);
    s0.EndDate = default(DateTime);
    s0.Amount = 2;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 1;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = default(DateTime);
    s2.EndDate = default(DateTime);
    s2.Amount = 3;
    chunks[2] = s2;
    Chunk s3 = default(Chunk);
    s3.StartDate = default(DateTime);
    s3.EndDate = default(DateTime);
    s3.Amount = 3;
    chunks[3] = s3;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException451()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[5];
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void apportionThrowsNullReferenceException579()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[5];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & -6067993060854775810L, 
                                (DateTimeKind)(12378751012854775806uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 1;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = new DateTime(4611686018427387903L & 3155378975999999998L, 
                                (DateTimeKind)(3155378975999999998uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 4;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = new DateTime(4611686018427387903L & 3155378975999999998L, 
                                (DateTimeKind)(3155378975999999998uL >> 62));
    s2.EndDate = default(DateTime);
    s2.Amount = 3;
    chunks[2] = s2;
    Chunk s3 = default(Chunk);
    s3.StartDate = default(DateTime);
    s3.EndDate = default(DateTime);
    s3.Amount = 5;
    chunks[3] = s3;
    Chunk s4 = default(Chunk);
    s4.StartDate = default(DateTime);
    s4.EndDate = default(DateTime);
    s4.Amount = 2;
    chunks[4] = s4;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, (List<Chunk>)null, list);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportion715()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[5];
    Chunk s0 = default(Chunk);
    s0.StartDate = new DateTime(4611686018427387903L & 3155090124447490815L, 
                                (DateTimeKind)(3155090124447490815uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 2;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate = new DateTime(4611686018427387903L & 3155090124447490815L, 
                                (DateTimeKind)(3155090124447490815uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 3;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate = new DateTime(4611686018427387903L & 3155090124447490815L, 
                                (DateTimeKind)(3155090124447490815uL >> 62));
    s2.EndDate = default(DateTime);
    s2.Amount = 1;
    chunks[2] = s2;
    Chunk s3 = default(Chunk);
    s3.StartDate = new DateTime(4611686018427387903L & 3154913963112081406L, 
                                (DateTimeKind)(3154913963112081406uL >> 62));
    s3.EndDate = default(DateTime);
    s3.Amount = 5;
    chunks[3] = s3;
    Chunk s4 = default(Chunk);
    s4.StartDate = new DateTime(4611686018427387903L & 7766599981539469310L, 
                                (DateTimeKind)(7766599981539469310uL >> 62));
    s4.EndDate = default(DateTime);
    s4.Amount = 4;
    chunks[4] = s4;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, list, list);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(0, list1.Capacity);
    Assert.AreEqual<int>(0, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
[TestMethod]
[PexGeneratedBy(typeof(ApportionmentManagerTest))]
public void apportion450()
{
    ApportionmentManager apportionmentManager;
    List<Chunk> list;
    List<Chunk> list1;
    apportionmentManager = new ApportionmentManager();
    Chunk[] chunks = new Chunk[5];
    Chunk s0 = default(Chunk);
    s0.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s0.EndDate = default(DateTime);
    s0.Amount = 3;
    chunks[0] = s0;
    Chunk s1 = default(Chunk);
    s1.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s1.EndDate = default(DateTime);
    s1.Amount = 2;
    chunks[1] = s1;
    Chunk s2 = default(Chunk);
    s2.StartDate =
      new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s2.EndDate = default(DateTime);
    s2.Amount = 4;
    chunks[2] = s2;
    Chunk s3 = default(Chunk);
    s3.StartDate = default(DateTime);
    s3.EndDate = new DateTime(4611686018427387903L & 1L, (DateTimeKind)(1uL >> 62));
    s3.Amount = 5;
    chunks[3] = s3;
    Chunk s4 = default(Chunk);
    s4.StartDate = default(DateTime);
    s4.EndDate = default(DateTime);
    s4.Amount = 1;
    chunks[4] = s4;
    list = new List<Chunk>((IEnumerable<Chunk>)chunks);
    list1 = this.apportion(apportionmentManager, list, list);
    Assert.IsNotNull((object)list1);
    Assert.AreEqual<int>(8, list1.Capacity);
    Assert.AreEqual<int>(5, list1.Count);
    Assert.IsNotNull((object)apportionmentManager);
}
    }
}
