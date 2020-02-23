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


        [TestCase("C", "C", Interval.P)]
        [TestCase("C", "D", Interval.MAJ)]
        [TestCase("C", "E", Interval.MAJ)]
        [TestCase("C", "F", Interval.P)]
        [TestCase("C", "G", Interval.P)]
        [TestCase("C", "A", Interval.MAJ)]
        [TestCase("C", "B", Interval.MAJ)]
        [TestCase("C", "C5", Interval.P)]
        [TestCase("C", "C#", Interval.AUG)]
        [TestCase("C", "Db", Interval.MIN)]
        [TestCase("C#", "D", Interval.MIN)]
        [TestCase("C#", "Db", Interval.DIM)]
        [TestCase("Cb", "D", Interval.AUG)]
        [TestCase("C", "Gb", Interval.DIM)]
        [TestCase("C#", "G", Interval.DIM)]
        [TestCase("D", "D", Interval.P)]
        [TestCase("D", "Eb", Interval.MIN)]
        [TestCase("D", "E", Interval.MAJ)]
        [TestCase("D", "E#", Interval.AUG)]
        [TestCase("D", "Fb", Interval.DIM)]
        [TestCase("D", "F", Interval.MIN)]
        [TestCase("D", "F#", Interval.MAJ)]
        [TestCase("D", "Gb", Interval.DIM)]
        [TestCase("D", "G", Interval.P)]
        [TestCase("D", "G#", Interval.AUG)]
        [TestCase("D", "Abb", Interval.DD)]
        [TestCase("D", "Ab", Interval.DIM)]
        [TestCase("D", "A", Interval.P)]
        [TestCase("D", "A#", Interval.AUG)]
        [TestCase("D", "Bb", Interval.MIN)]
        [TestCase("D", "B", Interval.MAJ)]
        [TestCase("D", "B#", Interval.AUG)]
        [TestCase("D", "B##", Interval.AA)]
        [TestCase("D", "Cb5", Interval.DIM)]
        [TestCase("D", "C5", Interval.MIN)]
        [TestCase("D", "C#5", Interval.MAJ)]
        public void TestQualityName(string startNote, string endNote, string qualityName)
        {
            Note start = new Note(startNote);
            Note end = new Note(endNote);
            Assert.AreEqual(qualityName, Interval.Quality(start, end));
        }

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
            Assert.AreEqual(pitchCount, Interval.PitchCount(start, end));
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
            Assert.AreEqual(semitoneCount, Interval.SemitoneCount(start, end));
        }
    }
}
