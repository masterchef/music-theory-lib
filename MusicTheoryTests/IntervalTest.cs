using System;
using MusicTheory;
using NUnit.Framework;

namespace IntervalTests
{
    public class IntervalTests
    {
        [TestCase("C", "C", "P1")]
        [TestCase("C", "D", "M2")]
        [TestCase("C", "E", "M3")]
        [TestCase("C", "F", "P4")]
        [TestCase("C", "G", "P5")]
        [TestCase("C", "A", "M6")]
        [TestCase("C", "B", "M7")]
        [TestCase("D", "D", "P1")]
        [TestCase("D", "E", "M2")]
        [TestCase("D", "F#", "M3")]
        [TestCase("D", "G", "P4")]
        [TestCase("D", "A", "P5")]
        [TestCase("D", "B", "M6")]
        [TestCase("D", "C#5", "M7")]
        [TestCase("D", "D5", "P8")]
        public void TestIntervalFromNotes(string fromNote, string toNote, string result)
        {
            Note start = new Note(fromNote);
            Note end = new Note(toNote);
            Interval interval = new Interval(start, end);
            Assert.AreEqual(result, interval.ToString());
        }

        [TestCase("P1", 1, "P")]
        public void TestIntervalFromString(string intervalName, int pitchCount, string quality)
        {
            Interval interval = new Interval(intervalName);
            Assert.AreEqual(pitchCount, interval.pitchCount);
            Assert.AreEqual(quality, interval.quality);
        }

        [TestCase("C", "P1", "C4")]
        [TestCase("C", "M2", "D4")]
        [TestCase("C", "M3", "E4")]
        [TestCase("C", "P4", "F4")]
        [TestCase("C", "P5", "G4")]
        [TestCase("C", "M6", "A4")]
        [TestCase("C", "M7", "B4")]
        [TestCase("D", "P1", "D4")]
        [TestCase("D", "M2", "E4")]
        [TestCase("D", "M3", "F#4")]
        [TestCase("D", "P4", "G4")]
        [TestCase("D", "P5", "A4")]
        [TestCase("D", "M6", "B4")]
        [TestCase("D", "M7", "C#5")]
        [TestCase("D", "P8", "D5")]
        [TestCase("D", "P9", "E5")]
        [TestCase("D", "P10", "F#5")]
        [TestCase("D", "P11", "G5")]
        [TestCase("D", "P12", "A5")]
        [TestCase("D", "P13", "B5")]
        [TestCase("D", "P14", "C#6")]
        [TestCase("D", "P15", "D6")]
        [TestCase("D", "P16", "E6")]
        public void TestEndNoteFromInterval(string fromNote, string intervalName, string result)
        {
            Note note = new Note(fromNote);
            Interval interval = new Interval(intervalName);
            Note endNote = interval.EndNote(note);
            Assert.AreEqual(result, endNote.ToString(true));
        }


        [TestCase("C", "C", Interval.PERFECT)]
        [TestCase("C", "D", Interval.MAJOR)]
        [TestCase("C", "E", Interval.MAJOR)]
        [TestCase("C", "F", Interval.PERFECT)]
        [TestCase("C", "G", Interval.PERFECT)]
        [TestCase("C", "A", Interval.MAJOR)]
        [TestCase("C", "B", Interval.MAJOR)]
        [TestCase("C", "C5", Interval.PERFECT)]
        [TestCase("C", "C#", Interval.AUGMENTED)]
        [TestCase("C", "Db", Interval.MINOR)]
        [TestCase("C#", "D", Interval.MINOR)]
        [TestCase("C#", "Db", Interval.DIMINISHED)]
        [TestCase("Cb", "D", Interval.AUGMENTED)]
        [TestCase("C", "Gb", Interval.DIMINISHED)]
        [TestCase("C#", "G", Interval.DIMINISHED)]
        [TestCase("D", "D", Interval.PERFECT)]
        [TestCase("D", "Eb", Interval.MINOR)]
        [TestCase("D", "E", Interval.MAJOR)]
        [TestCase("D", "E#", Interval.AUGMENTED)]
        [TestCase("D", "Fb", Interval.DIMINISHED)]
        [TestCase("D", "F", Interval.MINOR)]
        [TestCase("D", "F#", Interval.MAJOR)]
        [TestCase("D", "Gb", Interval.DIMINISHED)]
        [TestCase("D", "G", Interval.PERFECT)]
        [TestCase("D", "G#", Interval.AUGMENTED)]
        [TestCase("D", "Abb", Interval.DD)]
        [TestCase("D", "Ab", Interval.DIMINISHED)]
        [TestCase("D", "A", Interval.PERFECT)]
        [TestCase("D", "A#", Interval.AUGMENTED)]
        [TestCase("D", "Bb", Interval.MINOR)]
        [TestCase("D", "B", Interval.MAJOR)]
        [TestCase("D", "B#", Interval.AUGMENTED)]
        [TestCase("D", "B##", Interval.AA)]
        [TestCase("D", "Cb5", Interval.DIMINISHED)]
        [TestCase("D", "C5", Interval.MINOR)]
        [TestCase("D", "C#5", Interval.MAJOR)]
        public void TestQualityName(string startNote, string endNote, string qualityName)
        {
            Note start = new Note(startNote);
            Note end = new Note(endNote);
            Assert.AreEqual(qualityName, Interval.Quality(start, end));
        }
    }
}
