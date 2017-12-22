using Fantasy.API.DataAccess.Tests;
// <copyright file="FantasyTestsFactory.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;

namespace Fantasy.API.DataAccess.Tests
{
    /// <summary>A factory for Fantasy.API.DataAccess.Tests.FantasyTests instances</summary>
    public static partial class FantasyTestsFactory
    {
        /// <summary>A factory for Fantasy.API.DataAccess.Tests.FantasyTests instances</summary>
        [PexFactoryMethod(typeof(FantasyTests))]
        public static FantasyTests Create()
        {
            FantasyTests fantasyTests = new FantasyTests();
            return fantasyTests;

            // TODO: Edit factory method of FantasyTests
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
