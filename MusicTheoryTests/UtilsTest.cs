using MusicTheory;
using NUnit.Framework;

namespace UtilsTest
{
    public class UtilsTest
    {

        [TestCase("C", "C", 4, "P", "")]
        [TestCase("C#", "C", 4, "P", "#")]
        [TestCase("C#", "C", 4, "Dim", "")]
        [TestCase("C", "C", 4, "Aug", "#")]
        [TestCase("C", "C", 4, "DD", "bb")]
        [TestCase("C", "C", 4, "AA", "##")]
        [TestCase("C", "D", 4, "M", "")]
        [TestCase("C", "D", 4, "m", "b")]
        [TestCase("C#", "D", 4, "m", "")]
        [TestCase("E", "F", 4, "M", "#")]
        [TestCase("E", "F", 4, "Aug", "##")]
        [TestCase("E", "G", 4, "M", "#")]
        [TestCase("E", "C", 5, "M", "#")]
        [TestCase("E", "C", 5, "m", "")]
        [TestCase("E", "D", 5, "M", "#")]
        [TestCase("E", "D", 5, "m", "")]
        [TestCase("E", "E", 5, "P", "")]
        [TestCase("E", "E", 5, "Dim", "b")]
        [TestCase("E", "E", 5, "DD", "bb")]
        [TestCase("E", "E", 5, "Aug", "#")]
        [TestCase("E", "E", 5, "AA", "##")]
        [TestCase("E", "F", 5, "m", "")]
        [TestCase("E", "F", 5, "M", "#")]
        [TestCase("E", "F", 5, "Aug", "##")]
        public void TestEndPitchAccidental(string startNote, string endPitch, int endOctave, string intervalQuality, string result)
        {
            Note note = new Note(startNote);
            string pitchAccidental = Utils.EndPitchAccidental(note, endPitch, endOctave, intervalQuality);
            Assert.AreEqual(result, pitchAccidental);
        }

        [TestCase("C", 4, 1, 4)]
        [TestCase("C", 4, 2, 4)]
        [TestCase("C", 4, 7, 4)]
        [TestCase("C", 4, 8, 5)]
        [TestCase("C", 4, 9, 5)]
        [TestCase("B", 4, 1, 4)]
        [TestCase("B", 4, 2, 5)]
        [TestCase("B", 4, 4, 5)]
        [TestCase("B", 0, 7, 1)]
        [TestCase("B", 0, 8, 1)]
        [TestCase("B", 0, 9, 2)]
        [TestCase("B", 4, 9, 6)]
        public void TestOctaveFromPitchCount(string pitch, int octave, int pitchCount, int resultOctave)
        {
            Assert.AreEqual(resultOctave, Utils.OctaveFromOffset(pitch, octave, pitchCount));
        }
    }
}
