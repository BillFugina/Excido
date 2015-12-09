using System;
using System.Linq;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Data.Model;
using BamApps.Excido.Interface.Service;
using BamApps.Excido.Interface.Data;

namespace BamApps.Excido.Service {
    public class SharedContentServiceValidator : IServiceValidator<SharedContentUnit>, ISharedContentServiceValidator<SharedContentUnit> {

        private readonly IReadSharedContentPredicate<SharedContentUnit> _readSharedContentPredicate;
        private readonly IWriteSharedContentPredicate<SharedContentUnit> _writeSharedContentPredicate;
        private readonly IGetSharedContentValidator<SharedContentUnit> _getSharedContentValidator;
        private readonly IAddSharedContentValidator<SharedContentUnit> _addSharedContentValidator;
        private readonly IUpdateSharedContentValidator<SharedContentUnit> _updateSharedContentValidator;
        private readonly IDeleteSharedContentValidator<SharedContentUnit> _deleteSharedContentValidator;
        public SharedContentServiceValidator(
                IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate,
                IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate,
                IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator, 
                IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator, 
                IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator, 
                IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator) {

            this._readSharedContentPredicate = readSharedContentPredicate;
            this._writeSharedContentPredicate = writeSharedContentPredicate;
            this._getSharedContentValidator = getSharedContentValidator;
            this._addSharedContentValidator = addSharedContentValidator;
            this._updateSharedContentValidator = updateSharedContentValidator;
            this._deleteSharedContentValidator = deleteSharedContentValidator;
        }



        public bool ValidateAdd(SharedContentUnit entity) {
            return _addSharedContentValidator.IsSatisfiedBy(entity);
        }

        public bool ValidateDelete(SharedContentUnit entity) {
            return _deleteSharedContentValidator.IsSatisfiedBy(entity);
        }

        public bool ValidateGetSlug(SharedContentUnit sharedContentUnit) {
            return _getSharedContentValidator.IsSatisfiedBy(sharedContentUnit);
        }

        public bool ValidateRead() {
            return _readSharedContentPredicate.Test();
        }

        public bool ValidateUpdate(SharedContentUnit entity) {
            return _updateSharedContentValidator.IsSatisfiedBy(entity);
        }

        public bool ValidateWrite() {
            return _writeSharedContentPredicate.Test();
        }

        public bool ValidateGet(SharedContentUnit entity) {
            return _getSharedContentValidator.IsSatisfiedBy(entity);
        }
    }


    public class ReadSharedContentPredicate : IReadSharedContentPredicate<SharedContentUnit> {
        public bool Test() {
            return true;
        }
    }

    public class WriteSharedContentPredicate : IWriteSharedContentPredicate<SharedContentUnit> {
        public bool Test() {
            return true;
        }
    }

    public class GetSharedContentValidator : IGetSharedContentValidator<SharedContentUnit> {
        public bool IsSatisfiedBy(SharedContentUnit entity) {
            var result = false;
            if (entity != null) {
                var expireDate = entity.ExpireDate?.Date ?? DateTime.MaxValue;
                result = DateTime.Now.Date <= expireDate;
            }
            return result;
        }
    }

    public class AddSharedContentValidator : IAddSharedContentValidator<SharedContentUnit> {
        ICreateStampValidator<SharedContentUnit> _createStampValidator;
        public AddSharedContentValidator(ICreateStampValidator<SharedContentUnit> createStampValidator) {
            this._createStampValidator = createStampValidator;
        }

        public bool IsSatisfiedBy(SharedContentUnit entity) {
            return _createStampValidator.IsSatisfiedBy(entity);
        }
    }

    public class UpdateSharedContentValidator : IUpdateSharedContentValidator<SharedContentUnit> {
        ICreateStampValidator<SharedContentUnit> _createStampValidator;
        public UpdateSharedContentValidator(ICreateStampValidator<SharedContentUnit> createStampValidator) {
            this._createStampValidator = createStampValidator;
        }

        public bool IsSatisfiedBy(SharedContentUnit entity) {
            return _createStampValidator.IsSatisfiedBy(entity);
        }
    }


    public class DeleteSharedContentValidator : IDeleteSharedContentValidator<SharedContentUnit> {
        public DeleteSharedContentValidator() {
        }

        public bool IsSatisfiedBy(SharedContentUnit entity) {
            return true;
        }
    }
}

