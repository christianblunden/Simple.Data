﻿using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Simple.Data.Commands
{
    class DeleteByCommand : ICommand
    {
        public bool IsCommandFor(string method)
        {
            return method.Equals("delete", StringComparison.InvariantCultureIgnoreCase) || method.StartsWith("deleteby", StringComparison.InvariantCultureIgnoreCase);
        }

        public object Execute(Database database, string tableName, InvokeMemberBinder binder, object[] args)
        {
            var criteria = binder.Name.Equals("delete", StringComparison.InvariantCultureIgnoreCase) ?
                binder.NamedArgumentsToDictionary(args)
                :
                MethodNameParser.ParseFromBinder(binder, args);

            var criteriaExpression = ExpressionHelper.CriteriaDictionaryToExpression(tableName, criteria);
            return database.Adapter.Delete(tableName, criteriaExpression);
        }
    }
}
