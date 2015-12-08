using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BamApps.Excido.Service.Tests")]

namespace BamApps.Excido.Service.Validation {
    internal class AndSpecification<T> : ISpecification<T> where T : IEntity {
        private readonly ISpecification<T> _spec1;
        private readonly ISpecification<T> _spec2;

        public ISpecification<T> Spec1 {
            get {
                return _spec1;
            }
        }

        public ISpecification<T> Spec2 {
            get {
                return _spec2;
            }
        }
        public AndSpecification(ISpecification<T> spec1, ISpecification<T> spec2) {
            if (spec1 == null) {
                throw new ArgumentNullException(nameof(spec1));
            }

            if (spec2 == null) {
                throw new ArgumentNullException(nameof(spec2));
            }

            _spec1 = spec1;
            _spec2 = spec2;
        }

        public bool IsSatisfiedBy(T entity) {
            return Spec1.IsSatisfiedBy(entity) && Spec2.IsSatisfiedBy(entity);
        }
    }

    internal class OrSpecification<T> : ISpecification<T> where T : IEntity {
        private readonly ISpecification<T> _spec1;
        private readonly ISpecification<T> _spec2;

        public ISpecification<T> Spec1 {
            get {
                return _spec1;
            }
        }

        public ISpecification<T> Spec2 {
            get {
                return _spec2;
            }
        }
        public OrSpecification(ISpecification<T> spec1, ISpecification<T> spec2) {
            if (spec1 == null) {
                throw new ArgumentNullException(nameof(spec1));
            }

            if (spec2 == null) {
                throw new ArgumentNullException(nameof(spec2));
            }

            _spec1 = spec1;
            _spec2 = spec2;
        }

        public bool IsSatisfiedBy(T entity) {
            return Spec1.IsSatisfiedBy(entity) || Spec2.IsSatisfiedBy(entity);
        }
    }

    internal class NotSpecification<T> : ISpecification<T> where T : IEntity {
        private readonly ISpecification<T> _wrapped;

        protected ISpecification<T> Wrapped {
            get {
                return _wrapped;
            }
        }

        internal NotSpecification(ISpecification<T> spec) {
            if (spec == null) {
                throw new ArgumentNullException(nameof(spec));
            }

            _wrapped = spec;
        }

        public bool IsSatisfiedBy(T candidate) {
            return !Wrapped.IsSatisfiedBy(candidate);
        }
    }

    public static class ExtensionMethods {
        public static ISpecification<T> And<T>(this ISpecification<T> spec1, ISpecification<T> spec2) where T : IEntity {
            return new AndSpecification<T>(spec1, spec2);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> spec1, ISpecification<T> spec2) where T : IEntity {
            return new OrSpecification<T>(spec1, spec2);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> spec) where T : IEntity {
            return new NotSpecification<T>(spec);
        }
    }
    public static class Specification<T> where T : IEntity {
        public static readonly TrueSpecification<T> True = new TrueSpecification<T>();
        public static readonly FalseSpecification<T> False = new FalseSpecification<T>();
    }

}
