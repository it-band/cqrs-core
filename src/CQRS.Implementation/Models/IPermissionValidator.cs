using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Implementation.Models
{
    [Serializable]
    public class PermissionValidationResult
    {
        public virtual bool IsValid => Errors.Count == 0;

        public IList<string> Errors { get; set; }

        public PermissionValidationResult()
        {
            Errors = new List<string>();
        }

        public PermissionValidationResult(string failure) : this(new[] { failure })
        {

        }

        public PermissionValidationResult(IEnumerable<string> failures)
        {
            Errors = failures.Where(failure => !string.IsNullOrEmpty(failure)).ToList();
        }

        public override string ToString()
        {
            return ToString(Environment.NewLine);
        }

        public string ToString(string separator)
        {
            return string.Join(separator, Errors);
        }
    }

    public interface IPermissionValidator<in T>
    {
        Task<PermissionValidationResult> Validate(T input, CancellationToken cancellation = default(CancellationToken));
    }
}
