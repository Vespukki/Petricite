using System;

namespace Petricite
{
    public interface IFilterable<T>
    {
        public void OnFilter(Filter<T> filter);


        public Func< bool> validTest { get; set; }
    }
}
