using System.Collections.Generic;

namespace ExportToPDF
{
    public class Model
    {
        private IEnumerable<Model> _rows;
        public IEnumerable<Model> Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                OnPropertyChanged("Rows");
            }
        }

        private void OnPropertyChanged(string v)
        {

        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Instagram { get; set; }
    }
}
