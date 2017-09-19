using System;
using System.Collections.Generic;
namespace shall
{
    public class mythSourcePair
    {
        public mythLayer _in;
        public mythLayer _out;
    }

    public class mythSource
    {
        private List<mythLayer> _in;
        private List<mythLayer> _out;
        private int _array_col = -1;
        public int Length
        {
            get
            {
                return _in.Count;
            }
        }

        public mythSource()
        {
            _in = new List<mythLayer>();
            _out = new List<mythLayer>();
        }

        public void Add(mythLayer __in, mythLayer __out)
        {
            if (_array_col < 0)
            {
                _array_col = __in.Length;
            }
            else
            {
                if (_array_col != __in.Length)
                {
                    return;
                }
            }
            _in.Add(__in);
            _out.Add(__out);
        }
        public mythSourcePair Get(int index)
        {
            var pair = new mythSourcePair();
            pair._in = _in[index];
            pair._out = _out[index];
            return pair;
        }
    }
}
