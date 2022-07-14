using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Base
{
    public static class DbSetExtension
    {

        public static IQueryable<TEntity> TempFindById<TEntity>(this DbSet<TEntity> set, DbContext context, params object[] keyValues) where TEntity : class
        {
            /*var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();

            var i = 0;
            foreach (var property in key.Properties)
            {
                var keyValue = keyValues[i];
                entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValue);
                i++;
            }

            var entry = entries.FirstOrDefault();
            if (entry != null)
            {
                var list = new List<TEntity> { entry.Entity };
                return list.AsQueryable();
            }*/

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(keyValues[0])),
                    parameter));

            // Look in the database
            return query;
        }

        public static IQueryable<TEntity> TempFindByIdInclude<TEntity>(this DbSet<TEntity> set, DbContext context, int id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            /*var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();

            var i = 0;
            foreach (var property in key.Properties)
            {
                var keyValue = id;
                entries = entries.Where(e => (int)e.Property(property.Name).CurrentValue == keyValue);
                i++;
            }

            var entry = entries.FirstOrDefault();
            if (entry != null)
            {
                var list = new List<TEntity> { entry.Entity };
                return list.AsQueryable();
            }*/

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(id)),
                    parameter));

            foreach (var prop in includeProperties)
            {
                query = query.Include(prop);
            }

            // Look in the database
            return query;
        }

    }
}
