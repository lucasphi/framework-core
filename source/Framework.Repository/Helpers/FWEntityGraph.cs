using Framework.Core;
using Framework.Repository.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Repository.Helpers
{
    internal class FWEntityGraph
    {
        public FWEntityGraph(DbContext context)
        {
            _context = context;
        }

        public void TrackChange(IFWEntity entity)
        {
            _context.ChangeTracker.TrackGraph(entity, e =>
            {
                var innerEntity = e.Entry.Entity as FWEntity;

                if (!innerEntity.IsRemoved())
                {
                    _context.Entry(innerEntity).State = (e.Entry.IsKeySet) ?
                                                            EntityState.Modified :
                                                            EntityState.Added;
                }
                else
                {
                    _context.Entry(innerEntity).State = EntityState.Deleted;
                }
            });
        }

        public void TrackDelete(IFWEntity entity)
        {
            _context.ChangeTracker.TrackGraph(entity, e =>
            {
                _context.Entry(e.Entry.Entity).State = EntityState.Deleted;
            });
        }

        private DbContext _context;
    }
}
