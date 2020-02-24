using NUnit.Framework;
using MusicTheory;
using System.Collections.Generic;

namespace MusicTheory.ScaleTests
{
    public class ScaleTest
    {
        [TestCase("M1,M2,M3,P4,P5,M6,M7")]
        public void TestScaleCreation(string input)
        {
            string[] intervals = input.Split(',');
            Scale scale = new Scale(intervals);
            Assert.AreEqual(intervals.Length, scale.Intervals());
            for (int i=0; i< intervals.Length; i++)
            {
                Assert.AreEqual(intervals[i], scale.Intervals()[i].ToString());
            }
        }

        [TestCase("M1,M2,P4", "C", "C,D,F")]
        [TestCase("M1,M2,M3,P4,P5,M6,M7", "C", "C,D,E,F,G,A,B")]
        [TestCase("M1,M2,M3,P4,P5,M6,M7", "D", "D,E,F#,G,A,B,C#5")]
        [TestCase("M1,M2,M3,P4,P5,M6,M7", "B", "C#5,D#5,E5,F#5,G#5,A#5")]
        [TestCase("M1,M3,P5,M7,M9", "C", "C,E,G,B,D5")]
        public void TestScaleForKey(string input, string key, string notes)
        {
            string[] intervals = input.Split(',');
            Scale scale = new Scale(intervals);
            Note[] scaleNotes = scale.Notes(key);
            List<Note> expectedNotes = new List<Note>();
            foreach (string noteString in notes.Split(','))
            {
                expectedNotes.Add(new Note(noteString));
            }
            Assert.AreEqual(expectedNotes, scaleNotes);
        }

        [TestCase("Major", "M1,M2,M3,P4,P5,M6,M7")]
        [TestCase("Minor", "M1,M2,m3,P4,P5,m6,M7")]
        [TestCase("Minor", "M1,M2,m3,P4,P5,m6,m7")]
        public void TestPresetScales(string scaleName, string result)
        {
            string[] intervals = result.Split(',');
            Scale scale = new Scale(scaleName);
            Assert.AreEqual(intervals.Length, scale.Intervals());
            for (int i = 0; i < intervals.Length; i++)
            {
                Assert.AreEqual(intervals[i], scale.Intervals()[i].ToString());
            }
        }
    }
}
