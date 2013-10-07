using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartElk.ElkMate.Common
{
    public class Apply<T>
    {
        public Action<T> ToApply { get; private set; }
        public bool IsGrave { get; private set; }

        public Apply(Action<T> toApply, bool isGrave)
        {
            ToApply = toApply;
            IsGrave = isGrave;
        }
    }
    
    public class On<T>
    {
        public Func<IEnumerable<T>> GetItems { get; private set; }
        public List<Apply<T>> ToApply { get; private set; }        
        public List<Action<IEnumerable<T>>> ToPerformBeforeApply { get; private set; }
        public List<Action<IEnumerable<T>>> ToPerformAfterApply { get; private set; }
                
        private On()
        {
            ToApply = new List<Apply<T>>();            
            ToPerformBeforeApply = new List<Action<IEnumerable<T>>>();            
            ToPerformAfterApply = new List<Action<IEnumerable<T>>>();            
        }

        public static On<T> Items(Func<IEnumerable<T>> getItems)
        {            
            return new On<T>(){GetItems = getItems};
        }

        public On<T> Apply(Action<T> apply)
        {
            this.ToApply.Add(new Apply<T>(apply, false));
            return this;
        }

        public On<T> ApplyGrave(Action<T> graveApply)
        {
            this.ToApply.Add(new Apply<T>(graveApply, true));
            return this;
        }

        public On<T> PerformBeforeApply(Action<IEnumerable<T>> perform)
        {
            this.ToPerformBeforeApply.Add(perform);
            return this;
        }

        public On<T> PerformAfterApply(Action<IEnumerable<T>> perform)
        {
            this.ToPerformAfterApply.Add(perform);
            return this;
        }

        public IList<T> Execute(bool forceGraveApplyExecution)
        {
            var result = GetItems().ToList();            
            
            foreach (var perform in ToPerformBeforeApply)
            {
                perform(result.AsReadOnly());
            }
                        
            foreach (var item in result)
            {                
                foreach (var apply in ToApply)
                {
                    if (!apply.IsGrave || apply.IsGrave && forceGraveApplyExecution)
                      apply.ToApply(item);
                }                
            }

            foreach (var perform in ToPerformAfterApply)
            {
                perform(result.AsReadOnly());
            }
                        
            return result;
        }

        public IList<T> Execute()
        {
            return Execute(false);
        }
    }
}
