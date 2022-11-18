using System.Runtime.CompilerServices;
using Xunit;

namespace EuromonBooks.TestHelpers
{
    /// <summary>
    ///     Attribute that is applied to a method to indicate that it is a fact that
    ///     should be run by the test runner. The name of the method will be used as
    ///     the test method's <see cref="Xunit.FactAttribute.DisplayName"/> after
    ///     being reformatted by replacing specific characters in the method's name
    ///     with other characters.
    /// </summary>
    //[ExcludeFromCodeCoverage]
    public class FactAutoDisplayNameAttribute : FactAttribute
    {
        /// <summary>
        ///     Attribute that is applied to a method to indicate that it is a fact that
        ///     should be run by the test runner. The name of the method will be used as
        ///     the test method's <see cref="Xunit.FactAttribute.DisplayName"/> after
        ///     being reformatted by replacing specific characters in the method's name
        ///     with other characters.
        /// </summary>
        /// <param name="charsToReplace">
        ///     A <see cref="string"/> containing the characters
        ///     to replace in the test method's name (e.g. "_").
        /// </param>
        /// <param name="replacementChars">
        ///     A <see cref="string"/> containing the characters (e.g. " ") that will
        ///     replace those specified by the <paramref name="charsToReplace"/> parameter
        ///     that are found in the test method's name.
        /// </param>
        /// <param name="testMethodName">
        ///     This is automatically set to the name of the current method;
        ///     there's no need to set a value for this parameter.
        /// </param>
        /// <param name="callerFilePath">
        ///     This is automatically set to the path of the current file;
        ///     there's no need to set a value for this parameter.
        /// </param>
        public FactAutoDisplayNameAttribute(string charsToReplace = "_",
                                                     string replacementChars = " ",
                                                     [CallerMemberName] string testMethodName = "",
                                                     [CallerFilePath] string callerFilePath = "")
        {
            if (charsToReplace != null)
            {
                string callerFile = Path.GetFileNameWithoutExtension(callerFilePath);
                if (!string.IsNullOrEmpty(callerFile) && !string.IsNullOrEmpty(testMethodName))
                    base.DisplayName = $"{ testMethodName.Replace(charsToReplace, replacementChars) }";
            }
        }
    }

    /// <summary>
    ///     <para>
    ///         Marks a test method as being a data theory. Data theories are tests which
    ///         are fed various bits of data from a data source, mapping to parameters on
    ///         the test method.
    ///         If the data source contains multiple rows, then the test method is executed
    ///         multiple times (once with each data row). Data is provided by attributes which
    ///         derive from <see cref="Xunit.Sdk.DataAttribute"/> (notably,
    ///         <see cref="InlineDataAttribute"/> and <see cref="Xunit.MemberDataAttribute"/>).
    ///     </para>
    ///     <para>
    ///         The name of the method will be used as the test method's
    ///         <see cref="Xunit.FactAttribute.DisplayName"/> after being reformatted by
    ///         replacing specific characters in the method's name with other characters.
    ///     </para>
    /// </summary>
    //[ExcludeFromCodeCoverage]
    [Xunit.Sdk.XunitTestCaseDiscoverer("Xunit.Sdk.TheoryDiscoverer", "xunit.execution.{Platform}")]
    public class TheoryAutoDisplayNameAttribute : FactAutoDisplayNameAttribute
    {
        /// <summary>
        ///     <para>
        ///         Marks a test method as being a data theory. Data theories are tests which
        ///         are fed various bits of data from a data source, mapping to parameters on
        ///         the test method.
        ///         If the data source contains multiple rows, then the test method is executed
        ///         multiple times (once with each data row). Data is provided by attributes which
        ///         derive from <see cref="Xunit.Sdk.DataAttribute"/> (notably,
        ///         <see cref="InlineDataAttribute"/> and <see cref="Xunit.MemberDataAttribute"/>).
        ///     </para>
        ///     <para>
        ///         The name of the method will be used as the test method's
        ///         <see cref="Xunit.FactAttribute.DisplayName"/> after being reformatted by
        ///         replacing specific characters in the method's name with other characters.
        ///     </para>
        /// </summary>
        /// <param name="charsToReplace">
        ///     A <see cref="string"/> containing the characters
        ///     to replace in the test method's name (e.g. "_").
        /// </param>
        /// <param name="replacementChars">
        ///     A <see cref="string"/> containing the characters (e.g. " ") that will
        ///     replace those specified by the <paramref name="charsToReplace"/> parameter
        ///     that are found in the test method's name.
        /// </param>
        /// <param name="testMethodName">
        ///     This is automatically set to the name of the current method;
        ///     there's no need to set a value for this parameter.
        /// </param>
        /// /// <param name="callerFilePath">
        ///     This is automatically set to the path of the current file;
        ///     there's no need to set a value for this parameter.
        /// </param>
        public TheoryAutoDisplayNameAttribute(string charsToReplace = "_",
                                                       string replacementChars = " ",
                                                       [CallerMemberName] string testMethodName = "",
                                                       [CallerFilePath] string callerFilePath = "")
            : base(charsToReplace, replacementChars, testMethodName, callerFilePath) { }
    }
}