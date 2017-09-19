using System;
using System.Collections.Generic;
namespace shall
{
    public class mythBPNetworkWorker
    {
        private mythSource _source;
        private List<mythLayer> _hiddenlayer;
        public mythBPNetworkWorker(mythSource source)
        {
            _source = source;
            _hiddenlayer = new List<mythLayer>();
            //_hiddenlayer.Add();
        }

        public void AddHiddenLayers(int count, int num = -1)
        {
            for (int i = 0; i < count; i++)
                AddHiddenLayer(num);
        }

        public void AddHiddenLayer(int num = -1)
        {
            if (num < 0)
            {
                var _sourceVal = _source.Get(0);
                num = (int)Math.Floor(
                    Math.Sqrt(_sourceVal._in.Length + _sourceVal._out.Length) + 5.0
                );
            }
            _hiddenlayer.Add(new mythLayer(num));
        }

        public void StartLoop(int times)
        {
            if(_hiddenlayer.Count == 0){
                throw new Exception("No hidden layer has been found");
            }
            for (int i = 0; i < times; i++)
            {
                double _e_sum = 0;
                for (int j = 0; j < _source.Length; j++)
                {
                    var _sourceVal = _source.Get(i);
                    List<mythLayer> __currentlayers = new List<mythLayer>();
                    __currentlayers.Add(_sourceVal._in);
                    __currentlayers.AddRange(_hiddenlayer);
                    __currentlayers.Add(_sourceVal._out);

                    Forward(__currentlayers);
                    Backward(__currentlayers, 0.5);

                    var _etotal = ETotal(_sourceVal._out);
                    _e_sum += _etotal;

                }
                Console.WriteLine("ETotal:{0}", _e_sum);
            }
        }

        private void Forward(List<mythLayer> currentlayers)
        {
            //start active
            for (int i = 0; i < currentlayers.Count - 1;i++)
            {
                var __currentlayer = currentlayers[i + 1];
                var __prevlayer = currentlayers[i];

                foreach(var t in __currentlayer.get())
                {
                    t.Activate(__prevlayer);
                }
            }
        }

        private double ETotal(mythLayer layer)
        {
            double _etotal = 0;
            foreach (var t in layer.get()) 
            {
                _etotal += t.CalcE;
            }
            return _etotal;
        }

        private void Backward(List<mythLayer> currentlayers,double rate)
        {
			//first,for output
			var _output = currentlayers[currentlayers.Count - 1];
			var _prevlayer = currentlayers[currentlayers.Count - 2];
            mythLayer _nextlayer;
            foreach(var t in _output.get())
            {
                t.CalcDelta();
                t.Update(_prevlayer, rate);
            }

            //second,for hidden layer
            for (int i = currentlayers.Count - 2; i >= 1; i--)
            {
                _prevlayer = currentlayers[i - 1];
                _nextlayer = currentlayers[i + 1];
                foreach (var t in _output.get())
                {
                    t.CalcDelta(_nextlayer);
                    t.Update(_prevlayer, rate);
                }
            }
        }
    }
}
