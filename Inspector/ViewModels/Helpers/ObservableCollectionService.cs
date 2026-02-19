using System.Collections.ObjectModel;

namespace Inspector.ViewModels.Helpers
{
    internal static class ObservableCollectionService
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            var c = new ObservableCollection<T>();
            try
            {
                foreach (T e in collection)
                    c.Add(e);
            }
            catch { }
            return c;
        }

        public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> inputCol, ObservableCollection<T> addCol)
        {
            foreach (T obj in addCol)
                inputCol.Add(obj);
            return inputCol;
        }
    }
}
