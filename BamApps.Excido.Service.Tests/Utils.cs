using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BamApps.Excido.Service.Tests {

    [ExcludeFromCodeCoverage]
    public static class Utils {
        public static _T ExpectException<_T>(Action action) where _T : Exception {
            try { action(); }
            catch (_T ex) { return ex; }
            Assert.Fail("Expected " + typeof(_T));
            return null;
        }
    }
}
