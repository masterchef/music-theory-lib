# music-theory-lib
Music theory library to work with Notes, Intervals, Scales, and Chords. 

## Description
There are four main classes built to support most of the functionality:
1. Note - the fundamental class of the whole library. It captures a musical note represented by a pitch, accidental and octave.
2. Interval - captures the musical interval between two notes and is defined by a pitch count and quality.
3. Scale - TBD
4. Chord - TBD.

## Examples
Below you'll find examples of how this library can be used to manipulate the classes described above.

### Notes
Note object can be instantiated with a string representation of a note. 
```
// The default Octave is 4
Note noteC = new Note("C");
noteC.ToString() -> C
noteC.ToString(showOctave=true) -> C4

// Standard accidental notation accepted [#, b, ##, bb]
Note noteC = new Note("C#");
noteC.ToString() -> C#

// Octave supported from 0 - 12
Note noteC = new Note("Cb5");
noteC.ToString(true) -> Cb5
```
