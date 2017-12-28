﻿namespace MADE.UnitTests.Common.Tests.Data.Caching.FileSystem
{
    using System;

    using MADE.Data.Caching.FileSystem;
    using MADE.UnitTests.Common.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FileDataCacheProviderTests
    {
        private TestModel model;

        private IFileDataCacheProvider provider;

        [TestInitialize]
        public void Setup()
        {
            this.provider = new FileDataCacheProvider();

            this.model = new TestModel
                             {
                                 Boolean = true,
                                 Byte = byte.MinValue,
                                 Char = char.MinValue,
                                 Decimal = decimal.One,
                                 Double = double.Epsilon,
                                 Float = float.Epsilon,
                                 Int = int.MinValue,
                                 Long = long.MinValue,
                                 Object = new TestModel(),
                                 SByte = sbyte.MinValue,
                                 Short = short.MinValue,
                                 String = "Hello, World!",
                                 UInt = uint.MinValue,
                                 ULong = ulong.MinValue,
                                 UShort = ushort.MinValue
                             };
        }

        [TestMethod]
        public void AddOrUpdate_AddModel_Exists()
        {
            string key = Guid.NewGuid().ToString();

            this.provider.AddOrUpdate(key, this.model);

            Assert.IsTrue(this.provider.Contains(key));
        }

	    [TestMethod]
	    public void AddOrUpdate_AddNull_DoesNotExist()
	    {
		    string key = Guid.NewGuid().ToString();

		    this.provider.AddOrUpdate<TestModel>(key, null);

		    Assert.IsFalse(this.provider.Contains(key));
	    }

	    [TestMethod]
	    public void Get_ExistingKey_ModelNotNull()
	    {
		    string key = Guid.NewGuid().ToString();

		    this.provider.AddOrUpdate(key, this.model);

		    Assert.IsTrue(this.provider.Contains(key));

			var item = this.provider.Get<TestModel>(key);

		    Assert.IsNotNull(item);
	    }

	    [TestMethod]
	    public void Get_NonExistingKey_ModelNull()
	    {
		    string key = Guid.NewGuid().ToString();

		    var item = this.provider.Get<TestModel>(key);

		    Assert.IsNull(item);
	    }

	    [TestMethod]
	    public void Remove_ExistingKey_ModelRemoved()
	    {
			string key = Guid.NewGuid().ToString();

		    this.provider.AddOrUpdate(key, this.model);

		    Assert.IsTrue(this.provider.Contains(key));

		    var item = this.provider.Get<TestModel>(key);

		    Assert.IsNotNull(item);

		    this.provider.Remove(key);

		    Assert.IsFalse(this.provider.Contains(key));
	    }
	}
}