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

        internal const int MAX_PITCHES = 7;
        internal const int MAX_SEMITONES = 12;

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
                {Note.DBL_FLAT, -2 },
                {Note.FLAT, -1 },
                {Note.NEUTRAL, 0 },
                {Note.SHARP, 1 },
                {Note.DBL_SHARP, 2 }
            };

            semitoneOffsetAccidentals = new Dictionary<int, string>
            {
                {-2, Note.DBL_FLAT },
                {-1, Note.FLAT },
                {0, Note.NEUTRAL },
                {1, Note.SHARP },
                {2, Note.DBL_SHARP }
            };

            qualityAccidentalOffsets = new Dictionary<string, int>
            {
                {Interval.MAJ, 0 },
                {Interval.MIN, -1 },
                {Interval.P, 0 },
                {Interval.DIM, -1 },
                {Interval.AUG, 1 },
                {Interval.DD, -2 },
                {Interval.AA, 2 }
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


        internal static int SemitoneCount(string startPitch, string startAccidental, int startOctave, string endPitch, string endAccidental, int endOctave)
        {
            return pitchMetadata[endPitch][1] + accidentalSemitoneOffset[endAccidental] + endOctave * MAX_SEMITONES
                - pitchMetadata[startPitch][1] - accidentalSemitoneOffset[startAccidental] - startOctave * MAX_SEMITONES;
        }

        internal static int OctaveFromOffset(string pitch, int pitchOctave, int pitchCount)
        {
            // Because pitchCount includes self we need to subtract 1 to get an accurate offset
            int pitchIndex = Utils.PitchIndex(pitch) +
                (MAX_PITCHES * pitchOctave) +
                pitchCount - 1;
            // Find an octave this pitch belongs to
            return pitchIndex / MAX_PITCHES;
        }

        /**
         * Calculates endPitch accidental given starting pitch, it's accidental
         */
        internal static string EndPitchAccidental(Note startNote, string endPitch, int endOctave, string intervalQuality)
        {
            int pitchCount = PitchCount(startNote.pitch, startNote.octave, endPitch, endOctave);
            // Get number of semitones for an interval with certain pitch count and quality offset
            int qualityAdjustedSemitoneCount = NaturalSemitoneCount((pitchCount - 1) % MAX_PITCHES) + qualityAccidentalOffsets[intervalQuality];

            // Find number of semitones between startNote and endPitch without endNote accidental
            int pitchAdjustedSemitoneCount = SemitoneCount(startNote.pitch,
                startNote.accidental,
                startNote.octave,
                endPitch,
                Note.NEUTRAL,
                endOctave) % MAX_SEMITONES;

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
         * Calculate number of pitches given starting pitch and octace, and end pitch and endOctave
         */
        internal static int PitchCount(string startPitch, int startOctave, string endPitch, int endOctave)
        {
            return pitchMetadata[endPitch][0] + endOctave * MAX_PITCHES
                - pitchMetadata[startPitch][0] - startOctave * MAX_PITCHES + 1;
        }

        
    }
}
