using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicTheory
{
    public class Interval
    {
        public Note startNote;
        public Note endNote;
        public int pitchCount;
        public string quality;

        internal const string P = "P";
        internal const string MAJ = "M";
        internal const string MIN = "m";
        internal const string AUG = "Aug";
        internal const string AA = "AA";
        internal const string DIM = "Dim";
        internal const string DD = "DD";

        public Interval(Note startNote, Note endNote)
        {
            quality = Interval.Quality(startNote, endNote);
            pitchCount = PitchCount(startNote, endNote);
            validate();
        }

        public Interval(string intervalName)
        {
            Regex rx = new Regex(@"^(?<quality>(M|m|P|Dim|Aug|DD|AA)+)(?<pitchCount>\d+)?$",
                RegexOptions.IgnoreCase);

            Match match = rx.Match(intervalName);

            GroupCollection groups = match.Groups;
            pitchCount = int.Parse(groups["pitchCount"].Value);
            quality = groups["quality"].Value;
            validate();
        }

        /**
         * Validates that given interval parameters are valid
         *
         * A valid interval must have a pitch count > 0 and quality must be
         * one of: M|m|P|Dim|Aug|DD|AA
         */
        private void validate()
        {
            if (pitchCount <= 0)
            {
                throw new InvalidOperationException(
                    "Intervals must be calculated from the lowest note.");
            }

            if (quality == "")
            {
                throw new InvalidOperationException(
                    "Interval quality is not specified.");
            }

            int intervalIndex = (pitchCount - 1) % Utils.MAX_PITCHES;

            // Validate that the interval quality has a valid quality
            if (Array.IndexOf(new int[] { 0, 3, 4 }, intervalIndex) >= 0)
            {
                if (Array.IndexOf(new string[] {P, AUG, DIM, DD, AA }, quality) < 0)
                {
                    throw new InvalidOperationException(
                    "Invalid quality for a PERFECT interval.");
                }
            } else
            {
                if (Array.IndexOf(new string[] { MAJ, AUG, MIN, DIM, DD, AA }, quality) < 0)
                {
                    throw new InvalidOperationException(
                    "Invalid quality for a MAJOR interval.");
                }
            }
        }

        /**
         * String version of the interval
         */
        public override string ToString()
        {
            return $"{quality}{pitchCount}";
        }

        /**
         * Returns end Note of a given interval and current Note as
         * a starting note.
         */
        public Note EndNote(Note startNote)
        {
            int pitchIndex = Utils.PitchIndex(startNote.pitch) + pitchCount - 1;
            string endPitch = Utils.PitchFromIndex(pitchIndex % 7);
            int octave = Utils.OctaveFromOffset(startNote.pitch, startNote.octave, pitchCount);
            string accidental = Utils.EndPitchAccidental(startNote, endPitch, octave, quality);
            return new Note($"{endPitch}{accidental}{octave}");
        }


        /**
         * Calculates interval quality given two Notes in the following way:
         * 1. Finds which interval it represents: 1, 2, 3 .. 7
         * 2. Sets QUALITY to PERFECT OR MAJOR given interval index
         * 3. Computes the actual number of semitones between the notes
         * 4. Computes the number of semitones expected for the expected interval
         * 5. Finds the semitone difference between the actual and expected number of semitones
         * 6. Updates the QUALITY based on a semitone offset.
         *
         * For example, qualitty for interval between C - E# Notes:
         * 1. Interval index is 2
         * 2. It's a MAJOR interval
         * 3. Actual number of semitones is 5
         * 4. For an unmodified MAJOR 3rd interval there should be 4 semitones
         * 5. The difference is 5 - 4 = 1
         * 6. MAJOR interval larger by 1 is AUGMENTED
         */
        internal static string Quality(Note startNote, Note endNote)
        {
            if (startNote.CompareTo(endNote) < 0)
            {
                throw new InvalidOperationException("End Note must be larger than start Note.");
            }

            var quality = MAJ;
            // Compute interval index rebased to C Note
            int intervalIndex = (PitchCount(startNote, endNote) - 1) % Utils.MAX_PITCHES;
            // Only unison, 4th and 5th note (0, 3, 4 indexed from 0) are perfect intervals
            if (Array.IndexOf(new int[] { 0, 3, 4 }, intervalIndex) >= 0)
            {
                quality = P;
            }

            // Find semitone count for a given interval index as reset to 0 or note C
            int naturalSemitoneCount = Utils.NaturalSemitoneCount(intervalIndex);
            // Compute semitone count rebased to C note
            int actualSemitoneCount = SemitoneCount(startNote, endNote) % Utils.MAX_SEMITONES;
            int intervalOffset = actualSemitoneCount - naturalSemitoneCount;

            switch (intervalOffset)
            {
                case 0:
                    break;
                case 1:
                    quality = AUG;
                    break;
                case 2:
                    quality = AA;
                    break;
                case -1:
                    if (quality == P)
                    {
                        quality = DIM;
                    }
                    else
                    {
                        quality = MIN;
                    }
                    break;
                case -2:
                    if (quality == MAJ)
                    {
                        quality = DIM;
                    } else
                    {
                        quality = DD;
                    }
                    break;
                case -3:
                    if (quality == MAJ)
                    {
                        quality = DD;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Invalid accidental offset of {intervalOffset} for a PERFECT interval.");
                    }
                    break;
                default:
                    throw new InvalidOperationException($"Invalid accidental offset of {intervalOffset} provided.");
            }
            return quality;
        }

        /**
         * Calculates number of pitches between two notes
         * for example:
         * C, C -> 1
         * C, B -> 7
         */
        internal static int PitchCount(Note startNote, Note endNote)
        {
            return Utils.PitchCount(startNote.pitch, startNote.octave, endNote.pitch, endNote.octave);
        }

        /**
         * Counts the number of semitones between two notes
         */
        internal static int SemitoneCount(Note start, Note end)
        {
            return Utils.SemitoneCount(start.pitch, start.accidental, start.octave, end.pitch, end.accidental, end.octave);
        }
    }
}
