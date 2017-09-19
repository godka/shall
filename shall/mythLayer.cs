using System;
namespace shall
{
    public class mythLayer
    {
        protected mythNeuron[] _data;
        public int Length
        {
            get
            {
                return _data.Length;   
            }
        }

        public mythLayer(double[] data)
        {
            _data = new mythNeuron[data.Length];
            for (int i = 0; i < _data.Length;i++)
            {
                _data[i] = new mythNeuron(i,data[i]);
            }
            //_data = data;
        }

        public mythLayer(int row)
        {
            _data = new mythNeuron[row];
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = new mythNeuron(i);
            }
        }

        public virtual mythNeuron[] get()
        {
            return _data;
        }

    }
}
