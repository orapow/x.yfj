using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using X.Core.Utility;
using System.IO;
using System.Net;

namespace UnitTest
{
    /// <summary>
    /// UnitTest3 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTest3
    {
        public UnitTest3()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void PostZuFang()
        {
            var dict = new Dictionary<string, string>();
            Tools.PostHttpData("", dict);
        }

        [TestMethod]
        public void UpFileToZuFang()
        {
            var data = File.ReadAllBytes(@"d:\doc\desktop\临时\20140929\13012254164d8f1fc82577f.jpg");
            var txt = Tools.PostHttpFile("http://bj.zufang.com/index.php/rent_housing/upload/?get_err=1", data, "13012254164d8f1fc82577f.jpg");
            Console.WriteLine(txt);
        }

        [TestMethod]
        public void Test()
        {
            var c = Tools.GetHttpData("http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + Tools.GetClientIP()); //1 - 1 - 1  中国 上海  上海
            Console.WriteLine(c.Split('\t').LastOrDefault()); 
        }
    }
}
