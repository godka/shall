using System;
using System.IO;
namespace shall
{
    public class mythFileReader
    {
        private string _filename = string.Empty;
        public mythFileReader(string filename)
        {
            _filename = filename;
        }
        public mythSource StartLoop()
        {
            var strings = File.ReadAllLines(_filename);
            if (strings.Length > 1)
            {
                var str = strings[0];
                var spstr = str.Split(' ');
                int _in_count = int.Parse(spstr[0]);
                int _out_count = int.Parse(spstr[1]);
                mythSource source = new mythSource();
                for (int i = 1; i < strings.Length; i++)
                {
                    spstr = strings[i].Split(' ');
                    double[] _data = new double[_in_count];
                    int k = 0;
                    for (int j = 0; j < _in_count; j++)
                    {
                        _data[j] = double.Parse(spstr[k++]);
                    }
                    double[] _out = new double[_out_count];
                    for (int j = 0; j < _out_count; j++)
                    {
                        _out[j] = double.Parse(spstr[k++]);
                    }
                    mythLayer _inlayer = new mythLayer(_data);
                    mythLayer _outlayer = new mythLayer(_out);
                    source.Add(_inlayer, _outlayer);
                }
                return source;
            }
            return null;
        }
    }
}
