using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Utility.owo
{
    internal static partial class Utility
    {
        internal static IEnumerable<T> InterleaveArrays<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            var arr = new List<T>();
            var observed = a.ToList();
            var other = b.ToList();

            while (observed.Any())
            {
                arr.Add(observed[0]);
                observed.RemoveAt(0);
                (observed, other) = (other, observed);
            }

            if (other.Count > 0)
                arr = arr.Concat(other).ToList();
            return arr;
        }
        internal static IEnumerable<Func<Word, Word>> SpecificWordMappingList => new List<Func<Word, Word>>
            {
                MapFucToFwuc, MapMomToMwom, MapTimeToTim, MapMeToMwe,
                MapNVowelToNy, MapOverToOwor, MapOveToUv, MapHahaToHehexD,
                MapTheToTeh, MapYouToU, MapReadToWead, MapWorseToWose
            };

        internal static IEnumerable<Func<Word, Word>> UvuMappingList => new List<Func<Word, Word>>
            {
                MapOToOwO, MapEwToUwU, MapHeyToHay, MapDeadToDed,
                MapNVowelTToNd
            };

        internal static IEnumerable<Func<Word, Word>> UwuMappingList => new List<Func<Word, Word>>
            {
                MapBracketsToStarTrails, MapPeriodCommaExclamationSemicolonToKaomojis,
                MapThatToDat, MapThToF, MapLeToWal, MapVeToWe, MapRyToWwy,
                MapROrLToW
            };

        internal static IEnumerable<Func<Word, Word>> OwoMappingList => new List<Func<Word, Word>>
            {
                MapLlToWw, MapVowelOrRExceptOlToWl, MapOldToOwld,
                MapOlToOwl, MapLOrRoToWo, MapSpecificConsonantsOToLetterAndWo,
                MapVOrWLeToWal, MapFiToFwi, MapVerToWer, MapPoiToPwoi,
                MapSpecificConsonantsLeToLetterAndWal,
                MapConsonantRToConsonantW,
                MapLyToWy, MapPleToPwe, MapNrToNw
            };
    }
}
