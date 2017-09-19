using System;
namespace shall
{
    public class mythNeuron
    {
        private double[] _w;
        private double _φ;
        private double _output;
        private double _target;
        private bool _enable_target;
        private double _delta;
        private int _index;
        public mythNeuron(int index,double target)
        {
            _w = null;
            _φ = 0;
            _output = 0;
            _target = target;
            _enable_target = true;
            _delta = 0;
            _index = index;
        }

		public mythNeuron(int index)
		{
			_w = null;
			_φ = 0;
			_output = 0;
            _enable_target = false;
            _delta = 0;
            _index = index;
		}
        public double Delta
        {
            get
            {
                return _delta;    
            }
        }
		public double Target
        {
            get
            {
                return _target;
            }
        }

        public double Output
        {
            get
            {
                return _output;
            }
        }

        public double CalcE
        {
            get
            {
                return Math.Pow((_target - _output), 2) / 2;
            }
        }

        public double Activate(mythLayer layer)
        {
            if(_w == null)
            {
                _w = new double[layer.Length];
                initRandom(_w);
            }
            return Foward(layer);
        }

        public void Update(mythLayer layer,double rate)
        {
            for (int i = 0; i < layer.get().Length;i++)
            {
                var __delta = layer.get()[i].Output * this.Delta;
                _w[i] -= __delta* rate;
            }
        }

        public void CalcDelta(mythLayer layer = null)
        {
            double dedoutput = 0;
            if (_enable_target && layer == null)
            {
                dedoutput = Output - Target;
            }
            else
            {
                //dedoutput = de1/doutput1 + de2/doutput2
                foreach (var t in layer.get())
                {
                    dedoutput += t.Delta * t._w[_index];
                }
            }
            var doutputdnet = disigmoid(Output);
            _delta = dedoutput * doutputdnet;
        }
        
        private double Foward(mythLayer layer)
        {
            var _layerVal = layer.get();
            //if (_layerVal.Length != _w.Length)
            double ret = 0;
            for (int i = 0; i < _layerVal.Length; i++)
            {
                ret += _layerVal[i].Target * _w[i];
            }
            //ret += _φ;
            _output = sigmoid(ret);
            return _output;
        }

        private double disigmoid(double _in)
        {
            return _in * (1 - _in);    
        }

        private double sigmoid(double _in)
        {
            return 1 / (1 + Math.Exp(_in));
        }
        protected void initRandom(double[] __data)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < __data.Length; i++)
                __data[i] = random.NextDouble();
        }
    }
}
