using System;
namespace MusicTheory
{
    public class Scale
    {
        private string[] intervals;
        private string scaleName;

        public Scale()
        {
        }

        public Scale(string[] intervals)
        {
            this.intervals = intervals;
        }

        public Scale(string scaleName)
        {
            this.scaleName = scaleName;
        }

        internal Interval[] Intervals()
        {
            throw new NotImplementedException();
        }

        internal Note[] Notes(string key)
        {
            throw new NotImplementedException();
        }
    }
}
