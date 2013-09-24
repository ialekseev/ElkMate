using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartElk.ElkMate.Common
{
    public class On<T>
    {
        public Func<IEnumerable<T>> GetItems { get; set; }
        public List<Action<T>> ToApply { get; set; }
        public List<Action> ToPerform { get; set; }
                
        private On()
        {
            ToApply = new List<Action<T>>();
            ToPerform = new List<Action>();            
        }

        public static On<T> Items(Func<IEnumerable<T>> getItems)
        {            
            return new On<T>(){GetItems = getItems};
        }

        public On<T> Apply(Action<T> apply)
        {
            this.ToApply.Add(apply);
            return this;
        }

        public On<T> Perform(Action perform)
        {
            this.ToPerform.Add(perform);
            return this;
        }

        public IList<T> Execute()
        {           
            var result = GetItems().ToList();
            
            foreach (var item in result)
            {                
                foreach (var apply in ToApply)
                {
                    apply(item);
                }                
            }

            foreach (var perform in ToPerform)
            {
                perform();
            }
                        
            return result;
        }        
    }
}
