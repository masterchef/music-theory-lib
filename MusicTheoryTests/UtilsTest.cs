using MusicTheory;
using NUnit.Framework;

namespace UtilsTest
{
    public class UtilsTest
    {
        [TestCase("C", "C", 1)]
        [TestCase("C", "D", 2)]
        [TestCase("C", "E", 3)]
        [TestCase("C", "F", 4)]
        [TestCase("C", "G", 5)]
        [TestCase("C", "A", 6)]
        [TestCase("C", "B", 7)]
        [TestCase("C", "C5", 8)]
        [TestCase("C", "C6", 15)]
        public void TestPitchCount(string startNote, string endNote, int pitchCount)
        {
            Note start = new Note(startNote);
            Note end = new Note(endNote);
            Assert.AreEqual(pitchCount, Utils.PitchCount(start, end));
        }

        [TestCase("C", "C", 0)]
        [TestCase("C", "C#", 1)]
        [TestCase("C", "Db", 1)]
        [TestCase("C", "D", 2)]
        [TestCase("C", "D#", 3)]
        [TestCase("C", "Eb", 3)]
        [TestCase("C", "E", 4)]
        [TestCase("C", "E#", 5)]
        [TestCase("C", "Fb", 4)]
        [TestCase("C", "F", 5)]
        [TestCase("C", "F#", 6)]
        [TestCase("C", "Gb", 6)]
        [TestCase("C", "G", 7)]
        [TestCase("C", "G#", 8)]
        [TestCase("C", "Ab", 8)]
        [TestCase("C", "A", 9)]
        [TestCase("C", "A#", 10)]
        [TestCase("C", "Bb", 10)]
        [TestCase("C", "B", 11)]
        [TestCase("C", "B#", 12)]
        [TestCase("C", "C5", 12)]
        [TestCase("C", "D5", 14)]
        [TestCase("C", "E5", 16)]
        [TestCase("C", "F5", 17)]
        [TestCase("C", "G5", 19)]
        [TestCase("C", "A5", 21)]
        [TestCase("C", "B5", 23)]
        [TestCase("C", "C6", 24)]
        public void TestSemitoneCount(string startNote, string endNote, int semitoneCount)
        {
            Note start = new Note(startNote);
            Note end = new Note(endNote);
            Assert.AreEqual(semitoneCount, Utils.SemitoneCount(start, end));
        }

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
