using System;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MusicTheory
{
    public class Note
    {
        public string pitch { get; }
        public string accidental { get;}
        public int octave { get; }

        public Note(string input)
        {
            Regex rx = new Regex(@"^(?<note>[ABCDEFGabcdefg]{1})(?<accidental>(#|##|b|bb))?(?<octave>\d+)?$",
                RegexOptions.IgnoreCase);

            Match match = rx.Match(input);

            GroupCollection groups = match.Groups;
            pitch = groups["note"].Value.ToUpper();
            accidental = groups["accidental"].Value;
            octave = groups["octave"].Value == "" ? 4 : int.Parse(groups["octave"].Value);
        }

        
        public string ToString(bool showOctave=false)
        {
            if (showOctave)
            {
                return string.Format("{0}{1}{2}", pitch, accidental, octave);
            } else
            {
                return string.Format("{0}{1}", pitch, accidental);
            }
            
        }
    }
}
