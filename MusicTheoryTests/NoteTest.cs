using NUnit.Framework;
using MusicTheory;

namespace NoteTests
{
    public class NoteTests
    {
        
        [TestCase("A", "A4")]
        [TestCase("B", "B4")]
        [TestCase("C", "C4")]
        [TestCase("D", "D4")]
        [TestCase("E", "E4")]
        [TestCase("F", "F4")]
        [TestCase("G", "G4")]
        public void TestNoteCreationJustName(string input, string expected)
        {
            Note note = new Note(input);
            Assert.AreEqual(expected, note.ToString(showOctave: true));
        }

        [TestCase("A#", "A#4")]
        [TestCase("Bb", "Bb4")]
        public void TestNoteCreationWithAccent(string input, string expected)
        {
            Note note = new Note(input);
            Assert.AreEqual(expected, note.ToString(showOctave: true));
        }

        [TestCase("A1", "A1")]
        [TestCase("B2", "B2")]
        [TestCase("C3", "C3")]
        [TestCase("D12", "D12")]
        public void TestNoteCreationWithOctave(string input, string expected)
        {
            Note note = new Note(input);
            Assert.AreEqual(expected, note.ToString(showOctave: true));
        }

        [TestCase("A#1", "A#1")]
        [TestCase("A##1", "A##1")]
        [TestCase("Bb3", "Bb3")]
        [TestCase("Bbb3", "Bbb3")]
        [TestCase("C#10", "C#10")]
        public void TestNoteCreationFull(string input, string expected)
        {
            Note note = new Note(input);
            Assert.AreEqual(expected, note.ToString(showOctave: true));
        }

        [TestCase("a#1", "A", "#", 1, "A#")]
        [TestCase("bb3", "B", "b", 3, "Bb")]
        [TestCase("c#10", "C", "#", 10, "C#")]
        [TestCase("c#", "C", "#", 4, "C#")]
        public void TestNoteCreationFullNoOctave(string input, string tone, string accidental, int octave, string expected) {
            Note note = new Note(input);
            Assert.AreEqual(tone, note.pitch);
            Assert.AreEqual(accidental, note.accidental);
            Assert.AreEqual(octave, note.octave);
            Assert.AreEqual(expected, note.ToString());
        }
    }
}