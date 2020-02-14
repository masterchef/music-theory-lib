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

        [TestCase("C", "P1", "C")]
        [TestCase("C", "M2", "D")]
        [TestCase("C", "M3", "E")]
        [TestCase("C", "P4", "F")]
        [TestCase("C", "P5", "G")]
        [TestCase("C", "M6", "A")]
        [TestCase("C", "M7", "B")]
        [TestCase("D", "P1", "D")]
        [TestCase("D", "M2", "E")]
        [TestCase("D", "M3", "F#")]
        [TestCase("D", "P4", "G")]
        [TestCase("D", "P5", "A")]
        [TestCase("D", "M6", "B")]
        [TestCase("D", "M7", "C#")]
        [TestCase("D", "P8", "D")]
        public void TestEndNoteFromInterval(string fromNote, string intervalName, string result)
        {
            Note note = new Note(fromNote);
            Interval interval = new Interval(intervalName);
            Note endNote = interval.EndNote(note);
            Assert.AreEqual(result, endNote.ToString());
        }

        [TestCase("C", "P1", "C")]
        [TestCase("D", "M2", "C")]
        [TestCase("E", "M3", "C")]
        [TestCase("F", "P4", "C")]
        [TestCase("G", "P5", "C")]
        [TestCase("A", "M6", "C")]
        [TestCase("B", "M7", "C")]
        [TestCase("D", "P1", "D")]
        [TestCase("E", "M2", "D")]
        [TestCase("F#", "M3", "D")]
        [TestCase("G#", "P4", "D")]
        [TestCase("G", "P5", "D")]
        [TestCase("A", "M6", "D")]
        [TestCase("B", "M7", "D")]
        [TestCase("C#", "P8", "D")]
        public void TestStartNoteFromInterval(string endNote, string intervalName, string result)
        {
            Note note = new Note(endNote);
            Interval interval = new Interval(intervalName);
            Note startNote = interval.StartNote(note);
            Assert.AreEqual(result, startNote.ToString());
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
        [TestCase("C#", "G", Interval.DIMINISHED)]
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
        [TestCase("D", "Ab", Interval.DIMINISHED)]
        [TestCase("D", "A", Interval.PERFECT)]
        [TestCase("D", "A#", Interval.AUGMENTED)]
        [TestCase("D", "Bb", Interval.MINOR)]
        [TestCase("D", "B", Interval.MAJOR)]
        [TestCase("D", "B#", Interval.AUGMENTED)]
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
