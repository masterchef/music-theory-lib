﻿using System;
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

        internal const string PERFECT = "P";
        internal const string MAJOR = "M";
        internal const string MINOR = "m";
        internal const string AUGMENTED = "Aug";
        internal const string AA = "AA";
        internal const string DIMINISHED = "Dim";
        internal const string DD = "DD";

        public Interval(Note startNote, Note endNote)
        {
            quality = Interval.Quality(startNote, endNote);
            pitchCount = Utils.PitchCount(startNote, endNote);
            validate();
        }

        public Interval(string intervalName)
        {
            Regex rx = new Regex(@"^(?<quality>(M|m|P|Dim|Aug|DD|AA)+)(?<pitchCount>\d+)?$",
                RegexOptions.IgnoreCase);

            Match match = rx.Match(intervalName);

            GroupCollection groups = match.Groups;
            pitchCount = int.Parse(groups["pitchCount"].Value);
            quality = groups["quality"].Value.ToUpper();
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
         * Calculates interval quality given two Notes
         */
        internal static string Quality(Note startNote, Note endNote)
        {
            var quality = MAJOR;
            int intervalIndex = (Utils.PitchCount(startNote, endNote) - 1) % 7;
            // Only unison, 4th and 5th note (0, 3, 4 indexed from 0) are perfect intervals
            if (Array.IndexOf(new int[] { 0, 3, 4 }, intervalIndex) >= 0)
            {
                quality = PERFECT;
            }

            // Find semitone count for a given interval index as reset to 0 or note C
            int naturalSemitoneCount = Utils.NaturalSemitoneCount(intervalIndex);
            // Actual semitone count needs to be reset to 0 or note C
            int actualSemitoneCount = Utils.SemitoneCount(startNote, endNote) % 12;
            int intervalOffset = actualSemitoneCount - naturalSemitoneCount;

            switch (intervalOffset)
            {
                case 0:
                    break;
                case 1:
                    quality = AUGMENTED;
                    break;
                case 2:
                    quality = AA;
                    break;
                case -1:
                    if (quality == PERFECT)
                    {
                        quality = DIMINISHED;
                    }
                    else
                    {
                        quality = MINOR;
                    }
                    break;
                case -2:
                    if (quality == MAJOR)
                    {
                        quality = DIMINISHED;
                    } else
                    {
                        quality = DD;
                    }
                    break;
                case -3:
                    if (quality == MAJOR)
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
    }
}
