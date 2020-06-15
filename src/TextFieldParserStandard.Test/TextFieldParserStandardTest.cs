using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextFieldParserStandard.Test
{
    [TestFixture]
    public class TextFieldParserStandardTest
    {
        [Test]
        [TestCase(",", "comma delimited")]
        [TestCase("\t", "tab delimited")]
        public void ParseDelimted(string delimiter, string message)
        {
            var parsedRows = GetParsedRows(
$@"Col1{delimiter}Col2{delimiter}Col3
A{delimiter}B{delimiter}C
D{delimiter}E{delimiter}F", delimiter).ToList();

            Assert.AreEqual(3, parsedRows.Count);
            var i = 0;
            var j = 0;
            var row = parsedRows[i];
            Assert.AreEqual(3, row.Length, $"{message}: parsedRows[{i}] length");
            Assert.AreEqual("Col1", row[j], $"{message}: parsedRows[{i}] col[{j}]");
            Assert.AreEqual("Col2", row[++j], $"{message}: parsedRows[{i}] col[{j}]");
            Assert.AreEqual("Col3", row[++j], $"{message}: parsedRows[{i}] col[{j}]");

            j = 0;
            row = parsedRows[++i];
            Assert.AreEqual(3, row.Length, $"{message}: parsedRows[{i}] length");
            Assert.AreEqual("A", row[j], $"{message}: parsedRows[{i}] col[{j}]");
            Assert.AreEqual("B", row[++j], $"{message}: parsedRows[{i}] col[{j}]");
            Assert.AreEqual("C", row[++j], $"{message}: parsedRows[{i}] col[{j}]");

            j = 0;
            row = parsedRows[++i];
            Assert.AreEqual(3, row.Length, $"{message}: parsedRows[{i}] length");
            Assert.AreEqual("D", row[j], $"{message}: parsedRows[{i}] col[{j}]");
            Assert.AreEqual("E", row[++j], $"{message}: parsedRows[{i}] col[{j}]");
            Assert.AreEqual("F", row[++j], $"{message}: parsedRows[{i}] col[{j}]");
        }

        private static IEnumerable<string[]> GetParsedRows(string content, string delimiter)
        {
            var parsedRows = new List<string[]>();
            using (var streamReader = new StreamReader(StringToStream(content)))
            {
                using (var parser = new TextFieldParser(streamReader))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(delimiter);

                    while (!parser.EndOfData)
                    {
                        parsedRows.Add(parser.ReadFields());
                    }
                }
            }

            return parsedRows;
        }

        /// <summary>
        /// http://www.csharp411.com/c-convert-string-to-stream-and-stream-to-string/
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static Stream StringToStream(string str)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(str));
        }
    }
}