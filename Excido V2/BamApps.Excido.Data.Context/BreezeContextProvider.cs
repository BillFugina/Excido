using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breeze.ContextProvider;
using BamApps.Excido.Interface.Service;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Data.Model;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace BamApps.Excido.Data.Context {
    public class BreezeContextProvider : ContextProvider, IBreezeContextProvider {
        private static readonly object __lock = new object();

        ISharedContentService<SharedContentUnit> _sharedContentService;
        IReadRepository<SharedContentUnit> _readRepository;
        IWriteRepository<SharedContentUnit> _writeRepository;

        private readonly List<KeyMapping> _keyMappings = new List<KeyMapping>();

        public BreezeContextProvider(ISharedContentService<SharedContentUnit> sharedContentService, IReadRepository<SharedContentUnit> readRepository, IWriteRepository<SharedContentUnit> writeRepository) {
            if (sharedContentService == null)
                throw new ArgumentNullException(nameof(sharedContentService), $"{nameof(sharedContentService)} is null.");
            if (readRepository == null)
                throw new ArgumentNullException(nameof(readRepository), $"{nameof(readRepository)} is null.");
            if (writeRepository == null)
                throw new ArgumentNullException(nameof(writeRepository), $"{nameof(writeRepository)} is null.");

            _sharedContentService = sharedContentService;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        protected override void SaveChangesCore(SaveWorkState saveWorkState) {
            lock (__lock) {
                _keyMappings.Clear();
                var saveMap = saveWorkState.SaveMap;

                saveSharedContentUnits(saveMap);

                // ToList effectively copies the _keyMappings so that an incoming SaveChanges call doesn't clear the 
                // keyMappings before the previous version has completed serializing. 
                saveWorkState.KeyMappings = _keyMappings.ToList();

                _writeRepository.SaveChanges();
            }
        }

        private void saveSharedContentUnits(Dictionary<Type, List<EntityInfo>> saveMap) {
            List<EntityInfo> infos;
            if (!saveMap.TryGetValue(typeof(SharedContentUnit), out infos)) {
                return;
            }

            foreach (var ei in infos) {
                var unit = ei.Entity as SharedContentUnit;

                if (unit != null) {

                    switch (ei.EntityState) {
                        case EntityState.Added:
                            addSharedContentUnit(unit);
                            break;
                        case EntityState.Modified:
                            updateSharedContentUnit(unit);
                            break;
                        case EntityState.Deleted:
                            deleteSharedContentUnit(unit);
                            break;
                    }
                }
            }
        }

        private void deleteSharedContentUnit(SharedContentUnit unit) {
            _writeRepository.DeleteEntity(unit);
        }

        private void updateSharedContentUnit(SharedContentUnit unit) {
            _writeRepository.UpdateEntity(unit);
        }

        private void addSharedContentUnit(SharedContentUnit unit) {
            _writeRepository.AddEntity(unit);
        }

        public IQueryable<SharedContentUnit> SharedContentUnits() {
            return _readRepository.GetAll();
        }

        #region unimplemented
        [ExcludeFromCodeCoverage]
        protected override string BuildJsonMetadata() {
            return null;
        }

        [ExcludeFromCodeCoverage]
        protected override void CloseDbConnection() {
        }

        [ExcludeFromCodeCoverage]
        protected override void OpenDbConnection() {
        }

        [ExcludeFromCodeCoverage]
        public override IDbConnection GetDbConnection() {
            return null;
        }
        #endregion unimplemented
    }
}
