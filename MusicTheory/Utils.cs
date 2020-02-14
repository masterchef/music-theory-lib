using System;
using System.Collections.Generic;

namespace MusicTheory
{
    public class Utils
    {
        static Dictionary<string, int[]> pitchMetadata;
        static string[] pitchIndex;
        static Dictionary<string, int> accidentalSemitoneOffset;
        static Dictionary<int, string> semitoneOffsetAccidentals;
        static Dictionary<string, int> qualityAccidentalOffsets;

        
        static Utils()
        {
            pitchMetadata = new Dictionary<string, int[]>
            {
                { "C", new int[] {0, 0} },
                { "D", new int[] {1, 2} },
                { "E", new int[] {2, 4} },
                { "F", new int[] {3, 5} },
                { "G", new int[] {4, 7} },
                { "A", new int[] {5, 9} },
                { "B", new int[] {6, 11} },
                { "O", new int[] {7, 12} }
            };

            pitchIndex = new string[] { "C", "D", "E", "F", "G", "A", "B", "O" };

            accidentalSemitoneOffset = new Dictionary<string, int>
            {
                {"bb", -2 },
                {"b", -1 },
                {"", 0 },
                {"#", 1 },
                {"##", 2 }
            };

            semitoneOffsetAccidentals = new Dictionary<int, string>
            {
                {-2, "bb" },
                {-1, "b" },
                {0, "" },
                {1, "#" },
                {2, "##" }
            };

            qualityAccidentalOffsets = new Dictionary<string, int>
            {
                {"M", 0 },
                {"m", -1 },
                {"P", 0 },
                {"Dim", -1 },
                {"Aug", 1 },
                {"DD", -2 },
                {"AA", 2 }
            };
        }

        internal static string PitchFromIndex(int v)
        {
            return pitchIndex[v];
        }

        internal static int PitchIndex(string pitch)
        {
            return pitchMetadata[pitch][0];
        }

        internal static int SemitoneCount(Note start, Note end)
        {
            return SemitoneCount(start.pitch, start.accidental, start.octave, end.pitch, end.accidental, end.octave);
        }

        internal static int SemitoneCount(string startPitch, string startAccidental, int startOctave, string endPitch, string endAccidental, int endOctave)
        {
            return pitchMetadata[endPitch][1] + accidentalSemitoneOffset[endAccidental] + endOctave * 12
                - pitchMetadata[startPitch][1] - accidentalSemitoneOffset[startAccidental] - startOctave * 12;
        }

        internal static int OctaveFromOffset(string pitch, int pitchOctave, int pitchCount)
        {
            // Because pitchCount includes self we need to subtract 1 to get an accurate offset
            int pitchIndex = Utils.PitchIndex(pitch) +
                (7 * pitchOctave) +
                pitchCount - 1;
            return pitchIndex / 7;
        }

        /**
         * Calculates endPitch accidental given starting pitch, it's accidental
         */
        internal static string EndPitchAccidental(Note startNote, string endPitch, int endOctave, string intervalQuality)
        {
            int pitchCount = PitchCount(startNote.pitch, startNote.octave, endPitch, endOctave);
            // Get number of semitones for an interval with certain pitch count and quality offset
            int qualityAdjustedSemitoneCount = NaturalSemitoneCount((pitchCount - 1) % 7) + qualityAccidentalOffsets[intervalQuality];

            // Find number of semitones between startNote and endPitch without endNote accidental
            int pitchAdjustedSemitoneCount = SemitoneCount(startNote.pitch, startNote.accidental, startNote.octave, endPitch, "", endOctave) % 12;

            // Find accidental offset required to match expected semitone count
            int accidentalOffset = qualityAdjustedSemitoneCount - pitchAdjustedSemitoneCount;
            return semitoneOffsetAccidentals[accidentalOffset];
        }

        /**
         * Returns number of semitones between notes C and a given interval
         * index. For example:
         * C -> 0
         * D -> 2
         * ...
         * B -> 11
         */
        internal static int NaturalSemitoneCount(int intervalIndex)
        {
            return pitchMetadata[pitchIndex[intervalIndex]][1];
        }

        /**
         * Calculates number of pitches between two notes including self
         * for example:
         * C, C -> 1
         * C, B -> 7
         */
        internal static int PitchCount(Note startNote, Note endNote)
        {
            return PitchCount(startNote.pitch, startNote.octave, endNote.pitch, endNote.octave);
        }

        internal static int PitchCount(string startPitch, int startOctave, string endPitch, int endOctave)
        {
            return pitchMetadata[endPitch][0] + endOctave * 7
                - pitchMetadata[startPitch][0] - startOctave * 7 + 1;
        }

        
    }
}
